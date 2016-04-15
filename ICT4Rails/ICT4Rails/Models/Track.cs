using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models
{
    public class Track
    {
        private int BeginsOn;
        public int LineID { get; set; }

        public Track(int lineid, int beginson)
        {
            this.LineID = lineid;
            this.BeginsOn = beginson;
        }
    }
}
