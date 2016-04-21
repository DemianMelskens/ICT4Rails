using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models
{
    public class Reservation
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public Tram Tram { get; set; }
        public Segment Segment { get; set; }

        public Reservation(DateTime begindate, DateTime enddate, Tram tram, Segment segment)
        {
            this.BeginDate = begindate;
            this.EndDate = enddate;
            this.Tram = tram;
            this.Segment = segment;
        }
    }
}
