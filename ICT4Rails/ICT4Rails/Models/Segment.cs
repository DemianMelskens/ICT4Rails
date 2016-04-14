using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICT4Rails.Models
{
    class Segment
    {
        public bool Blocked { get; set; }
        public Track Track { get; set; }
        public TextBox Textbox { get; set; }

        public Segment(bool blocked, Track track, TextBox textbox)
        {
            this.Track = track;
            this.Blocked = blocked;
            this.Textbox = textbox;
        }
    }
}
