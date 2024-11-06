using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

namespace FleshClone
{
    public partial class FlashClone : Form
    {
        protected string cfg = "cfg.txt";
        protected string registred = "registred.toml";
        public delegate void RecursedDirectoryEnum(string OriginalPath, TomlTable toml);
        RecursedDirectoryEnum Registration = FilesRegistration; 
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
                string[] lines = {
                    "FID: ND",
                    "Name: ND",
                    "OriginalPath: ND",
                    "ReservPath: ND"
                };
                File.WriteAllLines(cfg, lines);
            }
        }

        private void buttonBackUp_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                folderBrowserDialog1.Description = "Select folder for saving back ups";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    CfgUpdater("ReservPath", $"{folderBrowserDialog1.SelectedPath}");
                }
            }
            ShowCfg();
        }
        private void ButtonOriginal_Click(object sender, EventArgs e)
        {
            string originalPath = string.Empty;
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                folderBrowserDialog1.Description = "Select a folder on Flash-card to saving it";
               
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    originalPath = folderBrowserDialog1.SelectedPath;
                    CfgUpdater("OriginalPath", $"{originalPath}");
                    string driverLetter = originalPath[0].ToString();
                    CfgUpdater("FID", GetVolumeSerialNumber(driverLetter));
                    CfgUpdater("Name", GetDeviceName(driverLetter));
                }
            }
            if (!string.IsNullOrEmpty(originalPath)) 
            {
                ShowCfg();
                var toml = new TomlTable();
                RDE_Method(originalPath, toml, Registration);
                var tomlOut = Toml.FromModel(toml);
                File.WriteAllText(registred, tomlOut);
            }
            else
            {
                MessageBox.Show("Путь не был выбран.");
            }
        }
        private void RDE_Method(string OriginalPath, TomlTable toml, RecursedDirectoryEnum RM) //call FilesAppend for all of directorys
        {

            RM(OriginalPath, toml);
            string[] originalSubDirectories = Directory.GetDirectories(OriginalPath);
            foreach (string directory in originalSubDirectories)
            {
                RDE_Method(directory, toml, RM);
            }
        }
        static void FilesRegistration(string OriginalPath, TomlTable toml)//registred all files
        {
            string[] originalFiles = Directory.GetFiles(OriginalPath);//what if no files??

            foreach (string file in originalFiles)
            {

                string fullPath = Path.GetFullPath(file);
                DateTime lastEditTime = File.GetLastWriteTime(file);

                var fileTable = new TomlTable
                {
                    ["lastModified"] = lastEditTime.ToString("o") // ISO формат
                };

                
                toml[fullPath] = fileTable; 
            }
        }
        //to do
        //FilesCopying
        //подписка флэшки на копирование
        //Parallel.ForEach

        private void ShowCfg()
        {
            FIDLabel.Text = GetCfg("FID");
            FlashName.Text = GetCfg("Name");
            Opath.Text = GetCfg("OriginalPath");
            Spath.Text = GetCfg("ReservPath");
        }
        private string GetCfg(string keyName)
        {
            string[] lines = File.ReadAllLines(cfg);
            string pattern = $@"^{keyName}:.*";
            foreach (string line in lines)
            {
                if(Regex.IsMatch(line, pattern))
                {
                    string respons = "";
                    for(int i = keyName.Length + 2; i < line.Length; i++)
                    {
                        respons += line[i].ToString();
                    }
                    return respons;
                }
            }
            return null;
        }
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

        private void CfgUpdater(string keyName, string newData)
        {
            string[] lines = File.ReadAllLines(cfg);
            string pattern = $@"^{keyName}:.*";

            for (int i = 0; i < lines.Length; i++)

            {
                if (Regex.IsMatch(lines[i], pattern))
                {

                    lines[i] = $"{keyName}: " + newData;
                    break;
                }
            }
            File.WriteAllLines(cfg, lines);

        }


    }
}
