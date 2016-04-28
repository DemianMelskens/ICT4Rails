using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICT4Rails.Models
{
    public class Segment : IComparable<Segment>
    {
        public string Name { get; set; }
        public bool Blocked { get; set; }
        public Track Track { get; set; }
        public TextBox Textbox { get; set; }
        public Tram Tram { get; set; }

        public Segment(string name, bool blocked, Track track, Tram tram)
        {
            this.Name = name;
            this.Track = track;
            this.Blocked = blocked;
            this.Tram = tram;
        }

        public Segment(string name)
        {
            this.Name = name;
        }

        public int CompareTo(Segment other)
        {
            return Convert.ToInt32(this.Name) - Convert.ToInt32(other.Name);
        }
    }
}
