using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Data
{
    interface IUserContext
    {
        List<string> GetUsers();
    }
}
