using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models.Enums;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Models
{
    public class Maintenance
    {
        public int MaintenanceID { get; set; }
        public Tram Tram { get; set; }
        public MaintenanceType Type { get; set; }
        public string Specification { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public List<User> Workers { get; set; }
        

        public Maintenance(int maintenanceID, Tram tram, MaintenanceType type, string specification, DateTime date, int duration, List<User> workers)
        {
            this.MaintenanceID = maintenanceID;
            this.Tram = tram;
            this.Type = type;
            this.Specification = specification;
            this.Date = date;
            this.Duration = duration;
            this.Workers = workers;
            
        }

        public Maintenance(MaintenanceType type)
        {
            this.Type = type;
        }

       
    }
}
