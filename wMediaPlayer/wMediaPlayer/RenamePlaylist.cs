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
    public partial class RenamePlaylist : Form
    {
        public RenamePlaylist()
        {
            InitializeComponent();
        }

        private Form1 otherForm;
        private PlayListManager manager;
        private DataSet1 dataset;
        private AxWindowsMediaPlayer mp;

        public RenamePlaylist(Form1 form, DataSet1 dataset, AxWindowsMediaPlayer mp)
        {
            InitializeComponent();

            this.otherForm = form;
            this.dataset = dataset;
            manager = new PlayListManager(dataset);
            this.mp = mp;
        }

        //public RenamePlaylist(Form1 form, DataSet1 dataset, AxWindowsMediaPlayer mp, PlayListManager plm)
        //{
        //    InitializeComponent();

        //    manager = plm;
        //    this.otherForm = form;
        //    this.dataset = dataset;
        //    //manager = new PlayListManager(dataset);
        //    this.mp = mp;
        //}

        private void RenamePlaylist_Load(object sender, EventArgs e)
        {
            List<string> playlists = manager.LoadPlaylists();
            foreach (var item in playlists)
            {
                comboBox_playlists.Items.Add(item);
            }
        }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox_playlists.Text))
            {
                MessageBox.Show("Choose playlist!");
            }
            else
            {
                if (!String.IsNullOrEmpty(comboBox_playlists.Text))
                {
                    if (!comboBox_playlists.Items.Contains(comboBox_playlists.Text))
                    {
                        MessageBox.Show("This playlist doesn't exist.");
                        comboBox_playlists.Text = "";
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure you want to rename this playlist?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            
                            manager.RenamePlaylist(comboBox_playlists.SelectedItem.ToString(), txtBox_newPlaylistName.Text);

                            comboBox_playlists.Items.Clear();
                            List<string> playlists = manager.LoadPlaylists();
                            foreach (var item in playlists)
                            {
                                comboBox_playlists.Items.Add(item);
                            }

                            //comboBox_playlists.Items.Remove(comboBox_playlists.SelectedItem);
                            //comboBox_playlists.Items.Add(txtBox_newPlaylistName.Text);
                            comboBox_playlists.Text = "";
                            txtBox_newPlaylistName.Text = "";
                        }
                    }
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
