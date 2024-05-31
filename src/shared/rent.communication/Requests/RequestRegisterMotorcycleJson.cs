using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.communication.Requests
{
    public class RequestRegisterMotorcycleJson
    {
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
    }
}
