using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Data
{
    public class UserQueries : IUserContext
    {
        public List<User> GetUsers()
        {
            User user = null;
            List<User> users = new List<User>();
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "SELECT * " +
                                      "FROM " + '"' + "User" + '"';

                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if(Convert.ToString(reader["Profession"]) == "Wagenparkbeheerder")
                                {
                                    user = new Admin(Convert.ToString(reader["Username"]), Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]), Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]), Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Schoonmaker")
                                {
                                    user = new Cleaner(Convert.ToString(reader["Username"]), Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]), Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]), Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Technicus")
                                {
                                    user = new Technician(Convert.ToString(reader["Username"]), Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]), Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]), Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Bestuurder")
                                {
                                    user = new Driver(Convert.ToString(reader["Username"]), Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]), Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]), Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }

                                users.Add(user);
                            }
                        }
                        
                        return users;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        //added GetUserName
        public string GetUserName()
        {
            string username = "";
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "SELECT Username " +
                                      "FROM " + '"' + "User" + '"';
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                username = Convert.ToString(reader["Username"]);
                            }
                        }
                        return username;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        //added GetPassword
        public string GetPassword()
        {
            string password = "";
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "SELECT Password " +
                                      "FROM " + '"' + "User" + '"';
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                password = Convert.ToString(reader["Password"]);
                            }
                        }
                        return password;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}