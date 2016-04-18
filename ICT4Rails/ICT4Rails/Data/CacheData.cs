using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Data
{
    public class CacheData
    {
        private static TramQueries tramqueries = new TramQueries();
        private static UserQueries userqueries = new UserQueries();
        private static SegmentQueries segmentqueries = new SegmentQueries();

        public List<string> trams { get; set; }
        public List<string> users { get; set; }
        public List<string> segments { get; set; }
        //added username property
        public string username { get; set; }
        //added password property
        public string password { get; set; }

        public void LoadData()
        {
            trams = tramqueries.GetTrams();
            users = userqueries.GetUsers();
            segments = segmentqueries.GetSegments();
            //added username
            username = userqueries.GetUserName();
            //added password
            password = userqueries.GetPassword();
        }
    }
}
