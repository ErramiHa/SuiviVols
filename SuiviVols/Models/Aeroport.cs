using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Aeroport
    {
        public string CodeAeroport { get; set; }
        public string NomAeroport { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
