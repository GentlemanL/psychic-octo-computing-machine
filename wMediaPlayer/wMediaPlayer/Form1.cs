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
        public bool newPlaylistCreated { get; set; }
        private PlayListManager manager;

        private string currentPlayList;

        public string CurrentPlayList
        {
            get { return currentPlayList; }
            set 
            { 
                currentPlayList = value;
                lbl_playlistName.Text = currentPlayList;
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
            newPlaylistCreated = false;
        }

        private ToolStripMenuItem CreateOpenMenuItem(string playlist)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem(playlist);
            menuItem.Text = playlist;
            menuItem.Tag = playlist;
            menuItem.Click += (sender, args) =>
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
                LoadPlayList((string)tsmi.Tag);
            };
            return menuItem;
        }

        public void addItemToListView(string name, string duration)
        {
            listView1.Items.Add(name).SubItems.Add(duration);
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

                    listView1.Items.Clear();
                    addItemToListView(songName, lbl_totalDuration.Text = media.durationString);
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

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void playlistToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !playlistToolStripMenuItem.Checked;
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
            if (xWMP.settings.mute == true)
            {
                xWMP.settings.mute = false;
            }
            else
            {
                xWMP.settings.mute = true;
            }
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPlaylist np = new NewPlaylist(this, dataset, xWMP);
            np.ShowDialog();

            if (newPlaylistCreated)
            {
                //dorobit myslienku
                ToolStripMenuItem tsmi = CreateOpenMenuItem(CurrentPlayList);
                openToolStripMenuItem.DropDownItems.Add(tsmi);
                listView1.Items.Clear();
                newPlaylistCreated = false;
            }
        }
        private void updateOpenTSMI()
        {
            List<string> playLists = manager.LoadPlaylists();

            openToolStripMenuItem.DropDownItems.Clear();
            foreach (var playlist in playLists.OrderBy(playListName => playListName))
            {
                ToolStripMenuItem tsmi = CreateOpenMenuItem(playlist);
                openToolStripMenuItem.DropDownItems.Add(tsmi);
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeletePlaylist dp = new DeletePlaylist(this, dataset, xWMP);
            dp.ShowDialog();

            updateOpenTSMI();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenamePlaylist rp = new RenamePlaylist(this, dataset, xWMP);
            rp.ShowDialog();

            updateOpenTSMI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //odstranenie povodnych buttonov a stuff
            //xWMP.uiMode = "None";

            manager = new PlayListManager(dataset);
            List<string> playlists = manager.LoadPlaylists();
            openToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in playlists.OrderBy(playlistName => playlistName))
            {
                ToolStripMenuItem tsmi = CreateOpenMenuItem(item);
                openToolStripMenuItem.DropDownItems.Add(tsmi);
            }
        }
        private void LoadPlayList(string playlistName)
        {
            IWMPPlaylist pl = xWMP.playlistCollection.newPlaylist(playlistName);
            CurrentPlayList = playlistName;

            List<PlayListItem> playlistItems = manager.LoadPlaylist(CurrentPlayList);
            listView1.Items.Clear();

            TotalDuration = new TimeSpan(0, 0, 0);

            foreach (PlayListItem item in playlistItems)
            {
                addItemToListView(item.Name, item.Duration.ToString());
                IWMPMedia media = xWMP.newMedia(item.Path);
                pl.appendItem(media);
                TotalDuration += item.Duration;
            }

            xWMP.currentPlaylist = pl;
            xWMP.Ctlcontrols.stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataset.WriteXml("Playlists.xml");
        }
    }
}
