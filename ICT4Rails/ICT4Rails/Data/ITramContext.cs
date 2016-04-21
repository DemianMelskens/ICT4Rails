using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Data
{
    interface ITramContext
    {
        List<Tram> GetTrams();
    }
}
