using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wMediaPlayer
{
    class PlayListItem
    {
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

        //public TimeSpan DurationString()
        //{
            
        //}
    }
}
