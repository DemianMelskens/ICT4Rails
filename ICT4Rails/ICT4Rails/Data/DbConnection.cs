﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ICT4Rails.Data
{
    public class DbConnection
    {
        private static readonly Dictionary<string, string> ConnectionStringVariables = new Dictionary<string, string>
        {
            {"Data Source", "fhictora01.fhict.local:1521/fhictora"},
            {"User Id", "dbi306801"},
            {"Password", "kARHC2VPYo"}
        };

        private static string ConnectionString
        {
            get
            {
                var connectionstring = "";
                foreach (var variable in ConnectionStringVariables)
                {
                    connectionstring += string.Format("{0}={1};", variable.Key, variable.Value);
                }
                return connectionstring;
            }
        }

        public static OracleConnection Connection
        {
            get
            {
                try
                {
                    var connection = new OracleConnection(ConnectionString);
                    connection.Open();
                    return connection;
                }
                catch (Exception e)
                {
                    if (Exceptions.OracleTimeOutException.CheckIfTimeOutException(e))
                    {
                        throw new Exceptions.OracleTimeOutException();
                    }
                    else if (Exceptions.OracleConnectIdentifierException.CheckIfIdentifierExeption(e))
                    {
                        MessageBox.Show("Connect to vpn");
                        throw new Exceptions.OracleConnectIdentifierException(); //als deze exeption word gethrowed betekent dat je niet bent verbonden met vpn (vdi.fhict.nl(SSL))
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }


    }
}
