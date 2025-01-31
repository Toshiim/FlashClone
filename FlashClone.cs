using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Windows.Forms.LinkLabel;
using System.Management;
using Tomlyn;
using Tomlyn.Model;
using System.Diagnostics;
using Newtonsoft.Json;

namespace FleshClone
{
    public partial class FlashClone : Form
    {
        protected string cfg = "cfg.toml";
        protected string registred = "registred.json";
        public delegate void RecursedDirectoryEnum(string OriginalPath, Dictionary<string, object> jsonTree);
        RecursedDirectoryEnum Registration = FilesRegistration;

        string GlobOriginalPath = string.Empty;
        string GlobReservPath = string.Empty;
        Stopwatch stopwatch = new Stopwatch();
        public FlashClone()
        {
            InitializeComponent();
        }
        private void FleshClone_Load(object sender, EventArgs e)
        {
            if (File.Exists(cfg))
            {
                ShowCfg();
            }
            else
            {
                File.Create(cfg).Dispose();
                var toml = new TomlTable
                {
                    ["FID"] = FIDLabel.Text,
                    ["Name"] = FlashName.Text,
                    ["OriginalPath"] = Opath.Text,
                    ["ReservPath"] = Spath.Text,
                };
                File.WriteAllText(cfg, Toml.FromModel(toml));
            }
        }

        private void buttonBackUp_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                folderBrowserDialog1.Description = "Select folder for saving back ups";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    GlobReservPath = folderBrowserDialog1.SelectedPath;
                    CfgUpdater("ReservPath", $"{GlobReservPath}");
                }
            }
            ShowCfg();
        }
        private void ButtonOriginal_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                folderBrowserDialog1.Description = "Select a folder on Flash-card to save";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    GlobOriginalPath = folderBrowserDialog1.SelectedPath;
                    CfgUpdater("OriginalPath", $"{GlobOriginalPath}");
                    string driverLetter = GlobOriginalPath[0].ToString();
                    CfgUpdater("FID", GetVolumeSerialNumber(driverLetter));
                    CfgUpdater("Name", GetDeviceName(driverLetter));
                }
            }
            if (!string.IsNullOrEmpty(GlobOriginalPath)) //Registration
            {
                ShowCfg();
                var jsonTree = new Dictionary<string, object>(); //RI?
                RecursionIsolator(GlobOriginalPath, jsonTree, Registration);
                var resulTree = new Dictionary<string, object>();
                resulTree[Path.GetFileName(GlobOriginalPath)] = jsonTree;
                File.WriteAllText(registred, JsonConvert.SerializeObject(resulTree, Formatting.Indented)); //RI?
            }
            else
            {
                MessageBox.Show("Путь не был выбран.");
            }
        }
        private void buttonForCopy_Click(object sender, EventArgs e)
        {
            if (File.Exists(registred))
            {
                string json = File.ReadAllText(registred);
                var jsonTree = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                RecursionIsolator(GlobOriginalPath, jsonTree, FilesCopying);
                var resulTree = new Dictionary<string, object>();
                resulTree[Path.GetFileName(GlobOriginalPath)] = jsonTree;
                File.WriteAllText(registred, JsonConvert.SerializeObject(resulTree, Formatting.Indented));
            }
            else
            {
                MessageBox.Show("Файл не выбран или не зарегестрирован");
            }

            //var tomlOut = Toml.FromModel(toml);
            //File.WriteAllText(registred, tomlOut);
        }
        static void FilesCopying(string OriginalPath, Dictionary<string, object> jsonTree)
        {
            string[] originalFiles = Directory.GetFiles(OriginalPath);//what if no files??

            foreach (string file in originalFiles)
            {

                string fullPath = Path.GetFullPath(file);
                DateTime lastEditTime = File.GetLastWriteTime(file);

                var fileTable = new Dictionary<string, string>
                {
                    ["lastModified"] = lastEditTime.ToString("o") // ISO формат
                };


                jsonTree[fullPath] = fileTable;
            }
        }
        private void RecursionIsolator(string path, Dictionary<string, object> jsonTree, RecursedDirectoryEnum RM)
        {
            stopwatch.Start();
            RDE_Method(path, jsonTree, RM);
            stopwatch.Stop();
            TimeLable.Text = stopwatch.Elapsed.TotalSeconds.ToString() + " sec";
            stopwatch.Reset();
        }
        private void RDE_Method(string path, Dictionary<string, object> jsonTree, RecursedDirectoryEnum RM) //call RM for all of directorys
        {

            RM(path, jsonTree);

            string[] originalSubDirectories = Directory.GetDirectories(path);
            foreach (string directory in originalSubDirectories)
            {
                var subDirData = new Dictionary<string, object>();
                jsonTree[Path.GetFileName(directory)] = subDirData;
                RDE_Method(directory, subDirData, RM);
            }

        }

        static void FilesRegistration(string OriginalPath, Dictionary<string, object> jsonTree)//registred all files
        {
            jsonTree["LDM"] = Directory.GetLastWriteTime(OriginalPath).ToString("yyyy-MM-ddTHH:mm:sszzz"); //LastDirModified
            string[] originalFiles = Directory.GetFiles(OriginalPath);//what if no files??

            var filesData = new Dictionary<string, object>();
            foreach (string file in originalFiles)
            {

                DateTime lastEditTime = File.GetLastWriteTime(file);
                filesData[Path.GetFileName(file)] = new Dictionary<string, string>
                {
                    ["LFM"] = lastEditTime.ToString("yyyy-MM-ddTHH:mm:sszzz") // LastFileModified 
                };
            }
            jsonTree["files"] = filesData;
        }

        //
        //to do
        //sign RecursionIsolater -> json | RM ???
        //Разобратся с resulTree & jsonTree
        // Разобратся с глобальными и локальными путями(избыточны)
        //FilesCopying
        //подписка флэшки на копирование
        //Parallel.ForEach
        //порционное сохранение данных(записывать в файл после обработки каждой папки)
        //Древовидная структура
        //Загрузка переменных путей из cfg
        //Проверка при копировании выбранны ли  пути
        //

        static string GetVolumeSerialNumber(string driveLetter)
        {
            try
            {
                string query = $"SELECT * FROM Win32_LogicalDisk WHERE DeviceID = '{driveLetter}:'";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    ManagementObject disk = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                    return disk?["VolumeSerialNumber"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return null;
        }
        static string GetDeviceName(string driveLetter)
        {
            try
            {
                // Шаг 1: Получаем PartitionID, соответствующий букве диска
                string partitionQuery = $"ASSOCIATORS OF {{Win32_LogicalDisk.DeviceID='{driveLetter}:'}} WHERE AssocClass=Win32_LogicalDiskToPartition";
                using (ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(partitionQuery))
                {
                    foreach (ManagementObject partition in partitionSearcher.Get())
                    {
                        // Шаг 2: Получаем физический диск, связанный с этим разделом
                        string diskQuery = $"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partition["DeviceID"]}'}} WHERE AssocClass=Win32_DiskDriveToDiskPartition";
                        using (ManagementObjectSearcher diskSearcher = new ManagementObjectSearcher(diskQuery))
                        {
                            foreach (ManagementObject disk in diskSearcher.Get())
                            {
                                // Возвращаем название устройства
                                return disk["Model"]?.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return null;
        }
        private void ShowCfg()
        {
            var toml = Toml.ToModel(File.ReadAllText(cfg)) as TomlTable;
            FIDLabel.Text = toml["FID"]?.ToString();
            FlashName.Text = toml["Name"]?.ToString();
            Opath.Text = toml["OriginalPath"]?.ToString();
            Spath.Text = toml["ReservPath"]?.ToString();
        }

        private void CfgUpdater(string keyName, string newData)
        {
            var toml = Toml.ToModel(File.ReadAllText(cfg)) as TomlTable;
            toml[keyName] = newData;
            File.WriteAllText(cfg, Toml.FromModel(toml));
        }
        

    }
}