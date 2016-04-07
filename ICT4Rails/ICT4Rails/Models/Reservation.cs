using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models
{
    class Reservation
    {
        public int BeginDate { get; set; }
        public int EndDate { get; set; }
        public Tram Tram { get; set; }
        public Segment Segment { get; set; }

        public Reservation(int begindate, int enddate, Tram tram, Segment segment)
        {
            this.BeginDate = begindate;
            this.EndDate = enddate;
            this.Tram = tram;
            this.Segment = segment;
        }
    }
}
