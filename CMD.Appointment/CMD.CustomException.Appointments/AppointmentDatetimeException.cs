using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.CustomException.Appointments
{
    public class AppointmentDatetimeException : ApplicationException
    {
        public AppointmentDatetimeException(string message = "Cannot book appointment for previous date") : base(message)
        {

        }
    }
}
