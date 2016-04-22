using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Data
{
    interface IUserContext
    {
        List<User> GetUsers();
        void AddUser(int userid, string username, string password, int age, string profession, string firstname, string surname, string surnameprefix, string email);
        void DeleteUser(string firstname, string surname, string email);
    }
}
