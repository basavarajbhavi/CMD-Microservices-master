using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.CustomException.Appointments
{
    public class MissingDetailException : ApplicationException
    {
        public MissingDetailException(string message = "Missing appointment detail") : base(message)
        {

        }
    }
}
