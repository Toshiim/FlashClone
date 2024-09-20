using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace FleshClone
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);

        }
        public partial class UsbMonitorService : ServiceBase
        {
            private ManagementEventWatcher _watcher;



            private void USBDeviceInserted(object sender, EventArrivedEventArgs e)
            {
                // Здесь логика копирования данных или бэкапа
            }

            protected override void OnStop()
            {
                if (_watcher != null)
                {
                    _watcher.Stop();
                    _watcher.Dispose();
                }
            }
        }

    }
}
