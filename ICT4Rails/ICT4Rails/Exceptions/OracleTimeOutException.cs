using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Exceptions
{
    public class OracleTimeOutException : Exception
    {
        public static readonly string TimeOutErrorCode = "ORA-12170";
        public static bool CheckIfTimeOutException(Exception exception)
        {
            return exception.Message.IndexOf(TimeOutErrorCode) > -1;
        }

        public OracleTimeOutException() : base()
        {

        }
    }
}