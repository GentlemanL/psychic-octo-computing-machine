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
            //newPlaylistCreated = false;
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

            //**
            //if (DialogResult.OK == openFileDialog2.ShowDialog())
            //{
            //    IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.getByName(CurrentPlayListName).Item(0);
            //    List<PlayListItem> addedItems = new List<PlayListItem>();
            //    foreach (string url in openFileDialog2.FileNames)
            //    {
            //        IWMPMedia media = axWindowsMediaPlayer1.newMedia(url);
            //        playlist.appendItem(media);

            //        PlayListItem item = PlaylistWorker.CreateItem(Path.GetFileName(url), url);
            //        addedItems.Add(item);
            //        TotalDuration += item.Duration;
            //        string[] row = new string[3] {
            //                Path.GetFileName(url),
            //                item.ReturnDurationFormat(),
            //                url
            //            };
            //        ListViewItem items = new ListViewItem(row);
            //        listView1.Items.Add(items);

            //        //listBoxURLS.Items.Add(item);
            //    }
            //    lblTotalDuration.Text = string.Format(@"{0}", TotalDuration.Value.ToString(TotalDuration.Value.TotalMinutes >= 60 ? @"h\:mm\:ss" : @"m\:ss"));

            //    Dictionary<string, string> nameUrlPair = addedItems.ToDictionary(t => t.Url, t => t.ItemName);
            //    worker.AddSongsToPlayList(nameUrlPair, CurrentPlayListName);

            //    if (IsEmptyListView)
            //    {
            //        axWindowsMediaPlayer1.currentPlaylist = playlist;
            //    }
            //}
            //***************************
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
                xWMP.settings.volume = trackBar1.Value;
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
                //newPlaylistCreated = false;
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

            //defaultPlaylist = xWMP.playlistCollection.newPlaylist("Default playlist");
            //CurrentPlayList = defaultPlaylist.name;
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
            ListViewItem item = listView1.SelectedItems[0];

            PlayListItem plItem = new PlayListItem(item.SubItems[0].Text,
                                                    item.SubItems[2].Text,
                                                    TimeSpan.Parse(item.SubItems[1].Text));
            if (plItem == null) return;

            try
            {
                //IWMPPlaylist playlist = xWMP.playlistCollection.getByName(CurrentPlayList).Item(0);

                //for (int i = 0; i < playlist.count; i++)
                //{
                //    bool isSameUrl = string.Equals(playlist.Item[i].sourceURL, plItem.Path, StringComparison.InvariantCultureIgnoreCase);
                //    if (!isSameUrl)
                //        continue;

                //    xWMP.Ctlcontrols.playItem(playlist.Item[i]);
                //}
                IWMPPlaylist playlist = xWMP.playlistCollection.getByName(CurrentPlayList).Item(0);
                IWMPMedia media = xWMP.newMedia(item.SubItems[2].Text);
                while (!xWMP.Ctlcontrols.currentItem.sourceURL.Equals(item.SubItems[2].Text))
                {
                    xWMP.Ctlcontrols.next();
                }
                xWMP.Ctlcontrols.play();



                //xWMP.Ctlcontrols.playItem(playlist.Item[i]);
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

                listView1.Items.Clear();
                addItemToListView(Path.GetFileName(URL), lbl_totalDuration.Text = media.durationString, URL);
            }
            //NewPlaylist np = new NewPlaylist(this, dataset, xWMP);
            //np.ShowDialog();

            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    string songName = Path.GetFileName(ofd.FileName);
            //    path = ofd.FileName;
            //    IWMPMedia media = xWMP.newMedia(path);
            //    xWMP.URL = path;

            //    listView1.Items.Clear();
            //    addItemToListView(songName, lbl_totalDuration.Text = media.durationString, path);
            //}
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void trckBar_sound_Scroll(object sender, EventArgs e)
        {
            xWMP.settings.volume = trackBar1.Value;
        }
    }
}
