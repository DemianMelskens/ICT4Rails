using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Exceptions
{
    class OracleConnectIdentifierException : Exception
    {
        public static readonly string TimeOutErrorCode = "ORA-12154";
        public static bool CheckIfIdentifierExeption(Exception exception)
        {
            return exception.Message.IndexOf(TimeOutErrorCode) > -1;
        }
    }
}
