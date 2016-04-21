using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Data
{
    public class CacheData
    {
        private static TramQueries tramqueries = new TramQueries();
        private static UserQueries userqueries = new UserQueries();
        private static SegmentQueries segmentqueries = new SegmentQueries();
        private static ReservationQueries reservationqueries = new ReservationQueries();

        public List<Tram> trams { get; set; }
        public List<User> users { get; set; }
        public List<Segment> segments { get; set; }
        public List<Reservation> reservations { get; set; }
        //added username property
        public string username { get; set; }
        //added password property
        public string password { get; set; }

        public void LoadData()
        {
            trams = tramqueries.GetTrams();
            users = userqueries.GetUsers();
            segments = segmentqueries.GetSegments(trams);
            reservations = reservationqueries.GetReservations(trams, segments);

            //added username
            username = userqueries.GetUserName();
            //added password
            password = userqueries.GetPassword();
        }
    }
}
