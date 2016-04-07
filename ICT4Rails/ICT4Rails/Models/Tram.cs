using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models.Enums;

namespace ICT4Rails.Models
{
    class Tram
    {
        private List<Reservation> Reservations = new List<Reservation>();
        private List<Maintenance> Maintenances = new List<Maintenance>();
        public int TramID { get; set; }
        public Status Status { get; set; }
        public int Length { get; set; }

        public Tram(int tramid, Status status, int lenght)
        {
            this.TramID = tramid;
            this.Status = status;
            this.Length = lenght;
        }
    }
}
