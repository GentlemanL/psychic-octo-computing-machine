using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;

namespace wMediaPlayer
{
    public partial class Form1 : Form
    {
        public DataSet1 dataset;
        public TimeSpan? TotalDuration;
        private string path;
        private PlayListManager manager;

        private string currentPlayList;

        public string CurrentPlayList
        {
            get { return currentPlayList; }
            set 
            { 
                currentPlayList = value;

            }
        }


        public Form1()
        {
            InitializeComponent();

            dataset = new DataSet1();

            try
            {
                dataset.ReadXml("Playlists.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            TotalDuration = new TimeSpan(0, 0, 0);

        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;" + "*.wav;*.mp2;*.mp3|All Files|*.*";
            try
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string songName = Path.GetFileName(ofd.FileName);
                    path = ofd.FileName;
                    IWMPMedia media = xWMP.newMedia(path);
                    xWMP.URL = path;
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.play();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.next();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.previous();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.stop();
        }

        private void btn_mute_Click(object sender, EventArgs e)
        {
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            manager = new PlayListManager(dataset);
            CurrentPlayList = "";

            //List<string> playlists = manager
        }


    }
}
