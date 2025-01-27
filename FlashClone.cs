﻿using System;
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
using System.Diagnostics;

namespace FleshClone
{
    public partial class FlashClone : Form
    {
        protected string cfg = "cfg.toml";
        protected string registred = "registred.toml";
        public delegate void RecursedDirectoryEnum(string OriginalPath, TomlTable toml);
        RecursedDirectoryEnum Registration = FilesRegistration;
        public FlashClone()
        {
            InitializeComponent();
        }
        string GlobOriginalPath = string.Empty;
        string GlobReservPath = string.Empty;
        Stopwatch stopwatch = new Stopwatch();
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
                    ["FID"] = "ND",
                    ["Name"] = "ND",
                    ["OriginalPath"] = "ND",
                    ["ReservPath"] = "ND"
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
                folderBrowserDialog1.Description = "Select a folder on Flash-card to saving it";

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
                var toml = new TomlTable();
                RecursionIsolator(GlobOriginalPath, toml, Registration);
                var tomlOut = Toml.FromModel(toml);
                File.WriteAllText(registred, tomlOut);
            }
            else
            {
                MessageBox.Show("Путь не был выбран.");
            }
        }
        private void buttonForCopy_Click(object sender, EventArgs e)
        {
            var toml = new TomlTable();
            RecursionIsolator(GlobOriginalPath, toml, Registration);
            var tomlOut = Toml.FromModel(toml);
            File.WriteAllText(registred, tomlOut);
        }

        private void RecursionIsolator(string Path, TomlTable toml, RecursedDirectoryEnum RM)
        {
            stopwatch.Start();
            RDE_Method(Path, toml, RM);
            stopwatch.Stop();
            TimeLable.Text = stopwatch.Elapsed.TotalSeconds.ToString() + " sec";
            stopwatch.Reset();
        }
        private void RDE_Method(string Path, TomlTable toml, RecursedDirectoryEnum RM) //call RM for all of directorys
        {
            RM(Path, toml);
            string[] originalSubDirectories = Directory.GetDirectories(Path);
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
        static void FilesCopying(string OriginalPath, TomlTable toml)
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