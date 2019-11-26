using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Models.Repository
{
    public interface IAeroportRepository
    {
        Aeroport GetAeroport(string code);
        List<Aeroport> GetAllAeroport();
    }
}
