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
        public string Name { get; set; }
        public bool Blocked { get; set; }
        public Track Track { get; set; }
        public TextBox Textbox { get; set; }

        public Segment(string name, bool blocked, Track track, TextBox textbox)
        {
            this.Name = name;
            this.Track = track;
            this.Blocked = blocked;
            this.Textbox = textbox;
        }
    }
}
