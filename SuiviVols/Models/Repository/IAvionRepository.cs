using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Models.Repository
{
    public interface IAvionRepository
    {
        List<Avion> GetAllAvions();
    }
}
