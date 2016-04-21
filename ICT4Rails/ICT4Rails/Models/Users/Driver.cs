﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Models.Users
{
    class Driver : User
    {
        public override string UserName { get; set; }
        public override string Password { get; set; }
        public override int Age { get; set; }
        public override string FirstName { get; set; }
        public override string SurName { get; set; }
        public override string SurNamePrefix { get; set; }
        public override string Email { get; set; }

        public Driver(string name, string password, int age, string firstname, string surname, string surnameprefix, string email) : base(name, password, age, firstname, surname, surnameprefix, email)
        {
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
