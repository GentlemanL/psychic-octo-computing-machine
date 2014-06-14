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
    public partial class DeletePlaylist : Form
    {
        public DeletePlaylist()
        {
            InitializeComponent();
        }

        private Form1 otherForm;
        private PlayListManager manager;
        private DataSet1 dataset;
        private AxWindowsMediaPlayer mp;
        private string currentPlaylist;

        public DeletePlaylist(Form1 form, DataSet1 dataset, AxWindowsMediaPlayer mp)
        {
            InitializeComponent();

            this.otherForm = form;
            this.dataset = dataset;
            manager = new PlayListManager(dataset);
            this.mp = mp;
        }

        public DeletePlaylist(Form1 form, DataSet1 dataset, AxWindowsMediaPlayer mp, string currentPlaylist)
            : this(form, dataset, mp)
        {
            this.currentPlaylist = currentPlaylist;
        }

        private void DeletePlaylist_Load(object sender, EventArgs e)
        {
            List<string> playlists = manager.LoadPlaylists();
            foreach (var item in playlists)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void btn_np_delete_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Choose playlist!");
            }
            else
            {
                if (!String.IsNullOrEmpty(comboBox1.Text))
                {
                    if (!comboBox1.Items.Contains(comboBox1.Text))
                    {
                        MessageBox.Show("This playlist doesn't exist.");
                        comboBox1.Text = "";
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure you want to delete this playlist?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            currentPlaylist = comboBox1.SelectedIndex + 1.ToString();
                            manager.DeletePlaylist(comboBox1.Text);
                            comboBox1.Items.Remove(comboBox1.SelectedItem);
                            comboBox1.Text = "";
                        }
                    }
                }
            }
        }

        private void btn_np_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
