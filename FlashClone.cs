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

namespace FleshClone
{
    public partial class FlashClone : Form
    {
        protected string cfg = "cfg.txt";
        public FlashClone()
        {
            InitializeComponent();
        }

        private void FleshClone_Load(object sender, EventArgs e)
        {
            if (File.Exists(cfg))
            {
                //Загрузить и отобразить
            }
            else
            {
                File.Create(cfg).Dispose();
                string[] lines = {
                    "FID: ND",
                    "OriginalPath: ND",
                    "ReservPath: ND"
                };
                File.WriteAllLines(cfg, lines);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                folderBrowserDialog1.Description = "Select folder for saving back ups";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    CfgUpdater("ReservPath", $"{folderBrowserDialog1.SelectedPath}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                folderBrowserDialog1.Description = "Select a folder on Flash-card to saving it";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    CfgUpdater("OriginalPath", $"{folderBrowserDialog1.SelectedPath}");
                }
            }
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
