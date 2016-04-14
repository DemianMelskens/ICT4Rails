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
        public List<string> trams { get; set; }
        public List<string> users { get; set; }

        public void LoadData()
        {
            trams = tramqueries.GetTrams();
            users = userqueries.GetUsers();
        }
    }
}
