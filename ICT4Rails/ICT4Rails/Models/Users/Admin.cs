using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models.Users
{
    class Admin : User
    {
        public override string Name { get; set; }
        public override string Password { get; set; }

        public Admin(string name, string password): base(name, password)
        {
            this.Name = name;
            this.Password = password;
        }
    }
}
