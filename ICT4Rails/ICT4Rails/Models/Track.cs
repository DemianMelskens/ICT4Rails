using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models
{
    class Track
    {
        private Segment BeginsOn;
        public int LineID { get; set; }

        public Track(int lineid)
        {
            this.LineID = lineid;
        }
    }
}
