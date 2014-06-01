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
    class PlayListManager
    {
        private DataSet1 dataset;
        AxWMPLib.AxWindowsMediaPlayer Player;

        public PlayListManager(DataSet1 dataset)
        {
            this.dataset = dataset;
        }

        public void createPlaylist(string name)
        {
            DataSet1.PlaylistRow plRow = dataset.Playlist.NewPlaylistRow();
            plRow.PlaylistName = name;
            dataset.Playlist.AddPlaylistRow(plRow);
        }

        //public List<palylistitem>
    }
}
