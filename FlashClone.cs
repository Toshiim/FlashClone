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

namespace FleshClone
{
    public partial class FlashClone : Form
    {
        public FlashClone()
        {
            InitializeComponent();
        }

        private void FleshClone_Load(object sender, EventArgs e)
        {
            if (File.Exists("cfg.txt"))
            {
                //Загрузить и отобразить
            }
            else
            {
                File.Create("cfg.txt").Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
