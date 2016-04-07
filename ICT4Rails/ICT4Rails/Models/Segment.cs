using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models
{
    class Segment
    {
        public bool Blocked { get; set; }
        public Track Track { get; set; }

        public Segment(bool blocked, Track track)
        {
            this.Track = track;
            this.Blocked = blocked;
        }
    }
}
