using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Models.Repository
{
    public class ImplementAvion : IAvionRepository
    {

        private List<Avion> _avionList;

        public ImplementAvion()
        {
            _avionList = new List<Avion>()
            {
                new Avion(){IdAvion=1,Consommation=5,Designation="Boeing",EffortDecollage=80},
                new Avion(){IdAvion=2,Consommation=4.6,Designation="Pilatus",EffortDecollage=70},
                new Avion(){IdAvion=3,Consommation=4.2,Designation="Embraer",EffortDecollage=35},
                new Avion(){IdAvion=4,Consommation=4.4,Designation="Cessna",EffortDecollage=400},
                new Avion(){IdAvion=5,Consommation=4,Designation="Beechcraft",EffortDecollage=63},
                new Avion(){IdAvion=6,Consommation=5,Designation="Airbus",EffortDecollage=120},
                new Avion(){IdAvion=7,Consommation=6,Designation="Bristol",EffortDecollage=230},                         
            };
        }

        public List<Avion> GetAllAvions()
        {
            return _avionList.ToList();
        }
    }
}
