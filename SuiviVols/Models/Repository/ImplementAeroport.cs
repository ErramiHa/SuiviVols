using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Models.Repository
{
    public class ImplementAeroport : IAeroportRepository
    {
        private List<Aeroport> _aeroportList;

        public ImplementAeroport()
        {
            _aeroportList = new List<Aeroport>()
            {
                new Aeroport(){CodeAeroport="CMN",NomAeroport="Aéroport Mohamed V",Latitude=33.3699704,Longitude=-7.5857231},
                new Aeroport(){CodeAeroport="ORY",NomAeroport="Paris ORLY",Latitude=48.7277,Longitude=2.36708},
                new Aeroport(){CodeAeroport="LHR",NomAeroport="Londres Heathrow",Latitude=51.4700256,Longitude=-0.4564842},
                new Aeroport(){CodeAeroport="RAK",NomAeroport="Marrakech",Latitude=32.9262536,Longitude=-8.4107989},
                new Aeroport(){CodeAeroport="MAD",NomAeroport="Madrid",Latitude=40.4935,Longitude=-3.56629},
                new Aeroport(){CodeAeroport="CDG",NomAeroport="Charles de gaulle",Latitude=49.007,Longitude=2.55979},
            };
        }

        public Aeroport GetAeroport(string code)
        {
            return _aeroportList.FirstOrDefault(x=> x.CodeAeroport == code);
        }

        public List<Aeroport> GetAllAeroport()
        {
            return _aeroportList.ToList();
        }
    }
}
