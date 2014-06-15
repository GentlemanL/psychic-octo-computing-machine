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
        public DataSet1 dataset;
        //AxWMPLib.AxWindowsMediaPlayer Player;

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

        public static PlayListItem CreateItem(string songName, string path)
        {
            TimeSpan? duration = null;
            if (!string.IsNullOrWhiteSpace(path) && System.IO.File.Exists(path))
            {
                // mozno pouzit TagLib
                WindowsMediaPlayerClass wmpc = new WindowsMediaPlayerClass();
                IWMPMedia newFile = wmpc.newMedia(path);
                duration = TimeSpan.Parse(newFile.durationString.Length > 5 ? newFile.durationString : "00:" + newFile.durationString);

            }
            return new PlayListItem(songName, path, duration);
        }

        public List<PlayListItem> LoadPlaylist(string name)
        {
            var result =
                dataset.Song.Select(@"PlaylistName = '" + name + "'")
                       .Select(row =>
                       {
                           return CreateItem(row.Field<string>("SongName"), row.Field<string>("Path"));
                       });
            return result.ToList();

            //List<PlayListItem> result =
            //    dataset.Song.Select(@"PlaylistName = '" + name + "'")
            //           .Select(row =>
            //           {
            //               return CreateItem(row.Field<string>("SongName"), row.Field<string>("Path"));
            //           }).ToList(); ;
            //return result;
        }

        public List<string> LoadPlaylists()
        {
            return dataset.Playlist
                          .Cast<DataSet1.PlaylistRow>()
                          .Select(t => t.PlaylistName)
                          .ToList();
        }

        public void AddSongsToPlayList(Dictionary<string, string> nameUrlPair, string playlistName)
        {
            foreach (string song in nameUrlPair.Keys)
            {
                dataset.Song.AddSongRow(nameUrlPair[song], song, playlistName);
            }
        }

        public void DeleteSong(string name)
        {
            for (int i = dataset.Song.Rows.Count - 1; i >= 0; i--)
            {
                if (dataset.Song.Rows[i]["SongName"].ToString() == name)
                {
                    dataset.Song.Rows[i].Delete();
                    break;
                }
            }
        }

        public void DeletePlaylist(string playlistName)
        {
            DataRow[] rows = dataset.Song.Select("PlaylistName = '" + playlistName + "'");

            foreach (DataRow row in rows)
            {
                row.Delete();
            }

            for (int i = dataset.Playlist.Rows.Count - 1; i >= 0; i--)
            {
                if (dataset.Playlist.Rows[i]["PlaylistName"].ToString() == playlistName)
                {
                    dataset.Playlist.Rows[i].Delete();
                }
            }
        }

        public void RenamePlaylist(string oldPlaylist, string newPlaylist)
        {
            DataRow[] rows = dataset.Song.Select("PlaylistName = '" + oldPlaylist + "'");

            foreach (DataRow row in rows)
            {
                row["PlaylistName"] = newPlaylist;
            }

            for (int i = dataset.Playlist.Rows.Count - 1; i >= 0; i--)
            {
                if (dataset.Playlist.Rows[i]["PlaylistName"].ToString() == oldPlaylist)
                {
                    dataset.Playlist.Rows[i]["PlaylistName"] = newPlaylist;
                }
            }
        }
    }
}
