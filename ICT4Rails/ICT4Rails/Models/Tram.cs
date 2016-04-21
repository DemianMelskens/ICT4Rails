using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models.Enums;

namespace ICT4Rails.Models
{
    public class Tram
    {
        private List<Reservation> Reservations = new List<Reservation>();
        private List<Maintenance> Maintenances = new List<Maintenance>();

        public string TramID { get; set; }
        public Tramtype TramType { get; set; }
        public Status Status { get; set; }
        public int Length { get; set; }
        public DateTime lastClean { get; set; }
        public DateTime lastReperation { get; set; }
       

        public Tram(string tramid, Tramtype tramtype, Status status, int lenght, DateTime lastclean, DateTime lastreperation)
        {
            this.TramID = tramid;
            this.TramType = tramtype;
            this.Status = status;
            this.Length = lenght;
            this.lastClean = lastClean;
            this.lastReperation = lastReperation;
        }

        public Tram(string tramid)
        {
            this.TramID = tramid;
        }


    }
}
