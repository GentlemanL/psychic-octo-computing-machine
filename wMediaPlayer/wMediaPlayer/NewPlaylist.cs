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
    public partial class NewPlaylist : Form
    {
        public NewPlaylist()
        {
            InitializeComponent();
        }

        private Form1 otherForm;
        private PlayListManager manager;
        private DataSet1 dataset;
        private AxWindowsMediaPlayer mp; 
        public NewPlaylist(Form1 form, DataSet1 dataset, AxWindowsMediaPlayer mp)
        {
            InitializeComponent();

            this.otherForm = form;
            this.dataset = dataset;
            manager = new PlayListManager(dataset);
            this.mp = mp;
        }

        private void btn_np_ok_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBox_np_name.Text))
            {
                MessageBox.Show("Name can't be empty!");
            }
            else
            {
                otherForm.CurrentPlayList = txtBox_np_name.Text;
                manager.createPlaylist(txtBox_np_name.Text);
                mp.playlistCollection.newPlaylist(txtBox_np_name.Text);
                otherForm.newPlaylistCreated = true;
                Close();
            }
        }

        private void btn_np_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
