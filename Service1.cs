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

namespace FleshClone
{
    public partial class Service1 : ServiceBase
    {
        private ManagementEventWatcher watcher;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
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

            }
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
