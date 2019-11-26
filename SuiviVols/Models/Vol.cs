using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Models
{
    public class Vol
    {
        public int IdVol { get; set; }

        public List<SelectListItem> AeroportsDepart { get; set; }
        [Required(ErrorMessage = "Le champs aéroport de départ est obligatoire")]
        public string SelectedDepart { get; set; }
        public List<SelectListItem> AeroportsArrivee { get; set; }
        [Required(ErrorMessage = "Le champs aéroport d'arriver est obligatoire")]
        public string SelectedArrivee { get; set; }
        public List<SelectListItem> Avions { get; set; }
        [Required(ErrorMessage = "Le champs avions est obligatoire")]
        public int SelectedAvion { get; set; }
        public double DureeVol { get; set; }
        [Required(ErrorMessage = "Le champs date est obligatoire")]
        public DateTime DateVol { get; set; }
        public double DistanceBetweenAero { get; set; }
        public double ConsommationAvion { get; set; }
        public List<string> Errors { get; set; }


    }
}
