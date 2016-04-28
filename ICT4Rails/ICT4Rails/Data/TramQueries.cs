using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models;
using ICT4Rails.Models.Enums;

namespace ICT4Rails.Data
{
    public class TramQueries : ITramContext
    {
        public List<Tram> GetTrams()
        {
            var trams = new List<Tram>();
            Tramtype tramtype = Tramtype.Combinos;
            Status status = Status.Defect;
            DateTime lastclean;
            DateTime lastreparation;
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "SELECT t.TramID, t.TYPE, s.Name, t.Lenght, t.Lastclean, t.Lastreparation " +
                                      "FROM TRAM t left outer join Status_has_Tram st " +
                                      "on(t.TramID = st.TramID) " +
                                      "left outer join STATUS s " +
                                      "on(st.StatusID = s.StatusID) " +
                                      "order by t.TramID";

                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToString(reader["Type"]) == "Combinos")
                                {
                                    tramtype = Tramtype.Combinos;
                                }
                                else if (Convert.ToString(reader["Type"]) == "11G")
                                {
                                    tramtype = Tramtype.elevenG;
                                }
                                else if (Convert.ToString(reader["Type"]) == "Dubbele kop Combinos")
                                {
                                    tramtype = Tramtype.DubbelekopCombinos;
                                }
                                else if (Convert.ToString(reader["Type"]) == "12G")
                                {
                                    tramtype = Tramtype.twelfG;
                                }
                                else if (Convert.ToString(reader["Type"]) == "Opleidingstram")
                                {
                                    tramtype = Tramtype.Opleidingstram;
                                }

                                if(Convert.ToString(reader["Name"]) == "")
                                {
                                    status = Status.GeenStatusBekent;
                                }
                                else if (Convert.ToString(reader["Name"]) == "Defect")
                                {
                                    status = Status.Defect;
                                }
                                else if (Convert.ToString(reader["Name"]) == "Schoonmaak")
                                {
                                    status = Status.NeedsCleaning;
                                }
                                else if (Convert.ToString(reader["Name"]) == "Dienst")
                                {
                                    status = Status.ReadyForUse;
                                }
                                else if (Convert.ToString(reader["Name"]) == "Remise")
                                {
                                    status = Status.InRemise;
                                }
                                else if (Convert.ToString(reader["Name"]) == "Onderhoudsbeurt")
                                {
                                    status = Status.NeedsReperation;
                                }

                                string value = Convert.ToString(reader["LastClean"]);
                                value = value.Substring(0, 9);
                                string[] values = value.Split('-');
                                lastclean = new DateTime(Convert.ToInt32(values[2]), Convert.ToInt32(values[1]), Convert.ToInt32(values[0]));

                                value = Convert.ToString(reader["Lastreparation"]);
                                value = value.Substring(0, 9);
                                values = value.Split('-');
                                lastreparation = new DateTime(Convert.ToInt32(values[2]), Convert.ToInt32(values[1]), Convert.ToInt32(values[0]));

                                Tram tram = new Tram(Convert.ToString(reader["TramID"]), tramtype, status, Convert.ToInt32(reader["Lenght"]), lastclean, lastreparation);
                                trams.Add(tram);
                            }
                        }
                        return trams;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void ChangeTramStatus(int status, int tramid)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                if (CheckIfTramHasStatus(tramid))
                {
                    command.CommandText = "UPDATE Status_has_Tram " +
                                          "SET StatusID=" + @status +
                                          "WHERE TramID=" + @tramid;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    command.CommandText = "INSERT INTO Status_has_Tram (StatusID, TramID)" +
                                          "VALUES (" + @status + ", " + @tramid + ")";

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

        public bool CheckIfTramHasStatus(int tramid)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "SELECT st.TramID " +
                                      "FROM Status_has_Tram st " +
                                      "WHERE st.TramID =" + @tramid;

                try
                {
                    if (command.ExecuteScalar() == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AddTram(string tramid, string tramtype, DateTime lastclean, DateTime lastreparation, int lenght)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "INSERT INTO Tram (TramID, Type, Lastclean, lastreparation, Lenght) " +
                                      "VALUES (" + @tramid + ", '" + @tramtype + "', to_date('" + @lastclean.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24:mi:ss')" + ", to_date('" + @lastreparation.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24:mi:ss'), " + @lenght + ")";

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ChangeTram(string tramid, string tramtype, DateTime lastclean, DateTime lastreparation, int lenght)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "UPDATE Tram " +
                                      "SET TramID=" + @tramid + ", Type= '" + @tramtype + "', Lastclean=" + "to_date('" + @lastclean.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24:mi:ss')" + ", Lastreparation =" + "to_date('" + @lastreparation.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24:mi:ss')" + ", Lenght =" + @lenght +
                                      "WHERE TramID=" + @tramid;

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DeleteTram(string tramid)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "DELETE FROM Tram " +
                                      "WHERE TramID = " + @tramid;

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
