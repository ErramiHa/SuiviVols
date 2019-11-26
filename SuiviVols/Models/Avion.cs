using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Models
{
    public class Avion
    {
        public int IdAvion { get; set; }
        public string Designation { get; set; }
        public double Consommation { get; set; }
        public double EffortDecollage { get; set; }
    }
}
