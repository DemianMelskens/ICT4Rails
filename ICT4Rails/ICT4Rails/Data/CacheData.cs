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
        public List<string> trams { get; set; }

        public void LoadData()
        {
            trams = tramqueries.GetTrams();
        }
    }
}
