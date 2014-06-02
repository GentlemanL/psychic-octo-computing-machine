
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

        private string Name { get; set; }
        private string Path { get; set; }
        private TimeSpan? Duration { get; set; }

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
