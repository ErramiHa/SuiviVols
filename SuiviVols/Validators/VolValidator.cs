using FluentValidation;
using SuiviVols.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Validators
{

    public class VolValidator : AbstractValidator<Vol>
    {
        public VolValidator()
        {
            RuleFor(x => x.SelectedArrivee).NotEqual("-1").WithMessage("Le champs aéroport d'arrivée est obligatoire.");
            RuleFor(x => x.SelectedDepart).NotEqual("-1").WithMessage("Le champs aéroport de départ est obligatoire.");
            RuleFor(x => x.SelectedAvion).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Le champs avion est obligatoire.");
            RuleFor(x => x.DateVol).NotEmpty().WithMessage("Le champs date est obligatoire.");
            RuleFor(x => x.DateVol).GreaterThanOrEqualTo(p => DateTime.Now).WithMessage("Le champs date doit être supérieur de la date d'aujourd'hui.");



        }

    }
}
