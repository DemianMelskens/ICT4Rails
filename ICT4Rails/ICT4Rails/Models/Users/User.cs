using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models.Users
{
    public abstract class User
    {
        public abstract string Name { get; set; }
        public abstract string Password { get; set; }

        public User(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }
    }
}
