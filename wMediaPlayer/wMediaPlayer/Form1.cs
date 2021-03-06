﻿using System;
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
        private bool isPaused = false;
        public bool newPlaylistCreated { get; set; }
        private PlayListManager manager;
        private Timer myTimer;
        private int trackTime = 0;
        //public IWMPPlaylist defaultPlaylist;

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
            myTimer = new Timer();

            myTimer.Tick += new EventHandler(timer1_Tick); // Everytime timer ticks, timer_Tick will be called
            myTimer.Interval = (1) * (1000);              // Timer will tick evert second
            //myTimer.Enabled = true;                       // Enable the timer
            //myTimer.Start();                              // Start the timer
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

        public void addItemToListView(string name, string duration, string path)
        {
            string[] sub = new string[3] { name, duration, path };
            ListViewItem lvi = new ListViewItem(sub);
            listView1.Items.Add(lvi);
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
                    addItemToListView(songName, lbl_totalDuration.Text = media.durationString, path);
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


        string totalD;
        //add song to current playlist
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CurrentPlayList))
            {
                MessageBox.Show("Select playlist first!");
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;" + "*.wav;*.mp2;*.mp3|All Files|*.*";
                ofd.Multiselect = true;

                try
                {
                    //List<PlayListItem> items = new List<PlayListItem>();
                    Dictionary<string, string> dsfsd = new Dictionary<string, string>();

                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //string songName = Path.GetFileName(ofd.FileName);
                        //path = ofd.FileName;
                        //IWMPMedia media = xWMP.newMedia(path);

                        //xWMP.URL = path;

                        ////defaultPlaylist.appendItem(media);
                        //PlayListItem item = PlayListManager.CreateItem(songName, path);
                        //items.Add(item);
                        //totalD = media.durationString;
                        //if (totalD.Length < 6)
                        //{
                        //    totalD = "00:" + totalD;
                        //}
                        //TotalDuration += TimeSpan.Parse(totalD);
                        //lbl_totalDuration.Text = TotalDuration.ToString();
                        //addItemToListView(songName, totalD, path);

                        //dsfsd.Add(path, songName);
                        //manager.AddSongsToPlayList(dsfsd, CurrentPlayList);

                        List<PlayListItem> items = new List<PlayListItem>();
                        IWMPPlaylist playlist = xWMP.playlistCollection.getByName(CurrentPlayList).Item(0);

                        foreach (string path in ofd.FileNames)
                        {
                            IWMPMedia media = xWMP.newMedia(path);
                            //xWMP.URL = path;
                            PlayListItem item = PlayListManager.CreateItem(Path.GetFileName(path), path);
                            items.Add(item);
                            playlist.appendItem(media);
                            totalD = media.durationString;
                            if (totalD.Length < 6)
                            {
                                totalD = "00:" + totalD;
                            }
                            TotalDuration += TimeSpan.Parse(totalD);
                            lbl_totalDuration.Text = TotalDuration.ToString();

                            addItemToListView(Path.GetFileName(path), totalD, path);

                            //dsfsd.Add(path, Path.GetFileName(path));
                        }
                        Dictionary<string, string> nameUrlPair = items.ToDictionary(t => t.Path, t => t.Name);
                        manager.AddSongsToPlayList(nameUrlPair, CurrentPlayList);
                        LoadPlayList(CurrentPlayList);
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
            if (isPaused)
            {
                isPaused = false;
                xWMP.Ctlcontrols.play();
                myTimer.Start();
            }
            else
            {
                xWMP.Ctlcontrols.play();
                adjustTimeBar();
                myTimer.Start();
            }
        }
        private void btn_pause_Click(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.pause();
            myTimer.Stop();
            isPaused = true;
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.next();
            adjustTimeBar();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.previous();
            adjustTimeBar();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.stop();
            myTimer.Stop();
            trackTime = 0;
        }

        private void btn_mute_Click(object sender, EventArgs e)
        {
            if (xWMP.settings.mute == true)
            {
                xWMP.settings.mute = false;
                xWMP.settings.volume = trackBar_sound.Value;
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
                addItemToListView(item.Name, item.Duration.ToString(), item.Path);
                IWMPMedia media = xWMP.newMedia(item.Path);
                pl.appendItem(media);
                TotalDuration += item.Duration;
                lbl_totalDuration.Text = TotalDuration.ToString();
            }

            xWMP.currentPlaylist = pl;
            xWMP.Ctlcontrols.stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataset.WriteXml("Playlists.xml");
        }

        private void deleteSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the song?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IWMPMedia media = xWMP.newMedia(listView1.SelectedItems[0].SubItems[2].Text);
                manager.DeleteSong(listView1.SelectedItems[0].SubItems[0].Text);

                totalD = media.durationString;
                if (totalD.Length < 6)
                {
                    totalD = "00:" + totalD;
                }
                TotalDuration -= TimeSpan.Parse(totalD);
                lbl_totalDuration.Text = TotalDuration.ToString();

                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            isPaused = false;
            ListViewItem item = listView1.SelectedItems[0];
            PlayListItem plItem = new PlayListItem(item.SubItems[0].Text,
                                                    item.SubItems[2].Text,
                                                    TimeSpan.Parse(item.SubItems[1].Text));
            if (plItem == null) return;

            try
            {
                if (xWMP.Ctlcontrols.currentItem.sourceURL.Equals(item.SubItems[2].Text))
                {
                    xWMP.Ctlcontrols.stop();
                    xWMP.Ctlcontrols.play();
                }
                else
                {
                    IWMPPlaylist playlist = xWMP.playlistCollection.getByName(CurrentPlayList).Item(0);
                    IWMPMedia media = xWMP.newMedia(item.SubItems[2].Text);
                    while (!xWMP.Ctlcontrols.currentItem.sourceURL.Equals(item.SubItems[2].Text))
                    {
                        xWMP.Ctlcontrols.next();
                    }
                    xWMP.Ctlcontrols.play();
                }

                adjustTimeBar();
                tickAction();
                myTimer.Start();

            }
            catch (Exception)
            {
                MessageBox.Show("File not found! Delete song and add it from its new location!");
            }
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string URL = Microsoft.VisualBasic.Interaction.InputBox("Prompt", "Title", "Default", -1, -1);
            if (URL != "")
            {
                IWMPMedia media = xWMP.newMedia(URL);
                xWMP.URL = URL;

                lbl_playlistName.Text = "Playlist name";
                listView1.Items.Clear();
                addItemToListView(Path.GetFileName(URL), lbl_totalDuration.Text = media.durationString, URL);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //xWMP.Ctlcontrols.currentPosition = Convert.ToDouble(trackBar1.Value);
        }

        private void trckBar_sound_Scroll(object sender, EventArgs e)
        {
            xWMP.settings.volume = trackBar_sound.Value;
        }

        private string songTime(int tmp)
        {
            string time = String.Format("{0}:{1}:{2}", tmp / 3600, tmp / 60, tmp % 60);
            return time;
        }

        private void tickAction()
        {
            if (isPaused)
            {

            }
            else
            {
                //txtBox_currentTime.Text = trackTime.ToString();
                txtBox_currentTime.Text = songTime(trackBar1.Value);
                trackBar1.Value = trackTime;
                trackTime++;

                if (trackBar1.Value == trackBar1.Maximum)
                {
                    xWMP.Ctlcontrols.next();
                    adjustTimeBar();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tickAction();
        }

        private void adjustTimeBar()
        {
            IWMPMedia media = xWMP.newMedia(xWMP.Ctlcontrols.currentItem.sourceURL);
            double time = media.duration;
            trackTime = 0;
            trackBar1.Value = 0;
            trackBar1.Maximum = Convert.ToInt32(time);
        }

        private void xWMP_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {

        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            double dblValue;

            dblValue = ((double)e.X / (double)trackBar1.Width) * (trackBar1.Maximum - trackBar1.Minimum);
            trackBar1.Value = Convert.ToInt32(dblValue);
            xWMP.Ctlcontrols.currentPosition = dblValue;
            trackTime = trackBar1.Value;
        }

        private void trackBar1_Move(object sender, EventArgs e)
        {
            xWMP.Ctlcontrols.currentPosition = Convert.ToDouble(trackBar1.Value);
            trackTime = trackBar1.Value;
        }

    }
}
