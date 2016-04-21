using System;
using System.Collections.Generic;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Data
{
    public class UserQueries : IUserContext
    {
        public List<User> GetUsers()
        {
            User user = null;
            var users = new List<User>();
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
                                var profession = Convert.ToString(reader["Profession"]);
                                if (Convert.ToString(reader["Profession"]) == "Wagenparkbeheerder")
                                {
                                    user = new Admin(Convert.ToString(reader["Username"]),
                                        Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]),
                                        Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]),
                                        Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Schoonmaker")
                                {
                                    user = new Cleaner(Convert.ToString(reader["Username"]),
                                        Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]),
                                        Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]),
                                        Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Technicus")
                                {
                                    user = new Technician(Convert.ToString(reader["Username"]),
                                        Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]),
                                        Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]),
                                        Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Bestuurder")
                                {
                                    user = new Driver(Convert.ToString(reader["Username"]),
                                        Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]),
                                        Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]),
                                        Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
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

        public void AddUser(int userid, string username, string password, int age, string profession, string firstname,
            string surname, string surnameprefix, string email)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "INSERT INTO " + '"' + "User" + '"' +
                                      "(UserID, Username, Password, Age, Profession, Firstname, Surname, Surnameprefix, Email) " +
                                      "VALUES (" + @userid + ", '" + @username + "', '" + @password + "', " + @age +
                                      ", '" + @profession + "', '" + @firstname + "', '" + @surname + "', '" +
                                      @surnameprefix + "', '" + @email + "')";

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
        }

        public void DeleteUser(string firstname, string surname, string email)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "DELETE FROM " + '"' + "User" + '"' +
                                      " WHERE Firstname = " + "'" +
                                      firstname + "'" +
                                      " AND Surname = " + "'" +
                                      surname + "'" +
                                      " AND Email = " + "'" +
                                      email + "'";

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}