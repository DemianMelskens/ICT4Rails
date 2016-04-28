using System;
using System.Collections.Generic;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Data
{
    public class UserQueries : IUserContext
    {
        /// <summary>
        /// Retrieves a list of Users from the database.
        /// </summary>
        /// <returns>A list of User objects</returns>
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
                                    int userid = Convert.ToInt32(reader["UserID"]);
                                    user = new Admin(userid, Convert.ToString(reader["Username"]),
                                        Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]),
                                        Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]),
                                        Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Schoonmaker")
                                {
                                    user = new Cleaner(Convert.ToInt32(reader["UserID"]), Convert.ToString(reader["Username"]),
                                        Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]),
                                        Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]),
                                        Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Technicus")
                                {
                                    user = new Technician(Convert.ToInt32(reader["UserID"]), Convert.ToString(reader["Username"]),
                                        Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Age"]),
                                        Convert.ToString(reader["Firstname"]), Convert.ToString(reader["Surname"]),
                                        Convert.ToString(reader["Surnameprefix"]), Convert.ToString(reader["Email"]));
                                }
                                else if (Convert.ToString(reader["Profession"]) == "Bestuurder")
                                {
                                    user = new Driver(Convert.ToInt32(reader["UserID"]), Convert.ToString(reader["Username"]),
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

        /// <summary>
        /// Method adds a new user to the database
        /// </summary>
        /// <param name="userid">The user ID of the new user</param>
        /// <param name="username">The username of the new user</param>
        /// <param name="password">The password of the new user</param>
        /// <param name="age">The age of the new user</param>
        /// <param name="profession">Profession of the newly created user: technician, cleaner, driver etc.</param>
        /// <param name="firstname">The first name of the user</param>
        /// <param name="surname">The surname of the user</param>
        /// <param name="surnameprefix">Surname prefix (e.g.: 'van der')</param>
        /// <param name="email">E mail address of the user</param>
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

      

        /// <summary>
        /// Removes a user from the database. Requires first name, last name and email address to validate
        /// </summary>
        /// <param name="firstname">First name of the user to be deleted</param>
        /// <param name="surname">The last name of the user to be deleted</param>
        /// <param name="email">Email of the user to be deleted</param>
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