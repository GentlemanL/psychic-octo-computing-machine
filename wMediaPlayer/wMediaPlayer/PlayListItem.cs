using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wMediaPlayer
{
    class PlayListItem
    {
        private string songName;

        public PlayListItem() { }

        public string Name { get; set; }
        public string Path { get; set; }
        public TimeSpan? Duration { get; set; }

        public PlayListItem(string name, string path, TimeSpan duration)
        {
            Name = name;
            Path = path;
            Duration = duration;
        }

        public PlayListItem(string songName, string path, TimeSpan? duration)
        {
            this.songName = songName;
            this.Path = path;
            this.Duration = duration;
        }

        //public TimeSpan DurationString()
        //{
            
        //}
    }
}
