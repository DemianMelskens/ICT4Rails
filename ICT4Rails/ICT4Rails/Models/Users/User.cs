using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models.Users
{
    public abstract class User
    {
        public abstract int UserID { get; set; }
        public abstract string UserName { get; set; }
        public abstract string Password { get; set; }
        public abstract int Age { get; set; }
        public abstract string FirstName { get; set; }
        public abstract string SurName { get; set; }
        public abstract string SurNamePrefix { get; set; }
        public abstract string Email { get; set; }

        public User(int userid, string name, string password, int age, string firstname, string surname, string surnameprefix, string email)
        {
            this.UserID = userid;
            this.UserName = name;
            this.Password = password;
            this.Age = age;
            this.FirstName = firstname;
            this.SurName = surname;
            this.SurNamePrefix = surnameprefix;
            this.Email = email;
        }
    }
}
