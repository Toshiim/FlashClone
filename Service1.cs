using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using System.Text.RegularExpressions;
using Tomlyn;
using Tomlyn.Model;


namespace FleshClone
{
    public partial class Service1 : ServiceBase
    {
        private ManagementEventWatcher watcher;

        public Service1()
        {
            InitializeComponent();
        }
        protected string cfg = "cfg.txt";
        string FID = "";
        string Name = "";
        string OriginalPath = "";
        string ReservPath = "";

        Dictionary<string, DateTime> EditTimeOfFiles; 
        protected override void OnStart(string[] args)
        {

            if (File.Exists(cfg))
            {
                LoadCFG();
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
            watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            watcher.EventArrived += new EventArrivedEventHandler(USBDeviceInserted);
            watcher.Query = query;
            watcher.Start();
        }

        private void USBDeviceInserted(object sender, EventArrivedEventArgs e)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");

            foreach (ManagementObject device in searcher.Get())
            {
                if (device?["VolumeSerialNumber"]?.ToString() == FID)
                {
                    USBReservCopyFiles();
                }
            }
        }

        private void USBReservCopyFiles()
        {
            //https://learn.microsoft.com/en-us/dotnet/api/system.io.directory?view=net-8.0
            GetTimeOfOriginal();
        }

        private void GetTimeOfOriginal()
        {
            string[] originalFiles = Directory.GetFileSystemEntries(OriginalPath);
            foreach (string file in originalFiles)
            {
                string path = Path.Combine(OriginalPath, file);
                DateTime lastEditTime = File.GetLastWriteTime(path);
                EditTimeOfFiles[path] = lastEditTime;
            }
        }

        private void LoadCFG()
        {
            string[] buffer;
            string[] lines = File.ReadAllLines(cfg);
            buffer = lines[0].Split(':');
            FID = buffer[1];
            buffer = lines[1].Split(':');
            Name = buffer[1];
            buffer = lines[2].Split(':');
            OriginalPath = buffer[1];
            buffer = lines[3].Split(':');
            ReservPath = buffer[1];
        }
        //подумать о реализации через split
        //private string FarmatingF(string fstr)
        //{
        //    string pattern = "w+$";
        //    string respons = "";
        //    for (int i = fstr.Length + 2; i < line.Length; i++)
        //    {
        //        respons += line[i].ToString();
        //    }
        //}
        //private void LoadCFG()
        //{
        //    string[] lines = File.ReadAllLines(cfg);
        //    string[] keys = new string[] { "FID", "Name", "OriginalPath", "ReservPath" };
        //    foreach (string line in lines)
        //    {
        //        foreach (string key in keys)
        //        {
        //            string pattern = $@"^{key}:.*";
        //            if (Regex.IsMatch(line, pattern))
        //            {

        //                for (int i = key.Length + 2; i < line.Length; i++)
        //                {
        //                    respons += line[i].ToString();
        //                }
        //                return respons;
        //            }
        //        }
        //    }
        //    return null;
        //}
        private void LoadCFG(string key)
        {

        }
        protected override void OnStop()
        {
            if (watcher != null)
            {
                watcher.Stop();
                watcher.Dispose();
            }
        }
    }
}
