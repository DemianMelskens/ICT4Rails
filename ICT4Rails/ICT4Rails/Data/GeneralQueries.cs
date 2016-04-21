using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ICT4Rails.Data
{
    public static class GeneralQueries
    {
        public static int GetPrimairyKey(string tableName, string columnName)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "SELECT MAX(" + @columnName + ") " +
                                            "FROM " + '"' + @tableName + '"' + " " +
                                            "ORDER BY " + @columnName + " ASC";

                try
                {
                    if (command.ExecuteScalar() == DBNull.Value)
                    {
                        return 1;
                    }
                    else
                    {
                        int pk = Convert.ToInt32(command.ExecuteScalar());

                        return pk + 1;
                    }
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }
    }
}
