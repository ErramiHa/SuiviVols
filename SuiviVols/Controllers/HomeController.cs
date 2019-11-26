using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuiviVols.Models;
using SuiviVols.Models.Repository;
using SuiviVols.Validators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SuiviVols.Controllers
{
    public class HomeController : Controller
    {
        private IAeroportRepository _aeroportRepository;
        private IAvionRepository _avionRepository;
        private List<Aeroport> listAeroports = new List<Aeroport>();
        private List<Avion> listAvions = new List<Avion>();
        int IDVol = 1;

        public HomeController(IAeroportRepository aeroportRepository, IAvionRepository avionRepository)
        {
            _aeroportRepository = aeroportRepository;
            listAeroports = aeroportRepository.GetAllAeroport();
            _avionRepository = avionRepository;
            listAvions = avionRepository.GetAllAvions();

        }

        [HttpGet]
        public IActionResult Index()
        {
            Vol vol;
            List<SelectListItem> listAer = new List<SelectListItem>();
            List<SelectListItem> listAv = new List<SelectListItem>();

            foreach (var item in listAeroports)
            {
                listAer.Add(new SelectListItem { Text = item.NomAeroport, Value = item.CodeAeroport.ToString() });
            }
            foreach (var item in listAvions)
            {
                listAv.Add(new SelectListItem { Text = item.Designation, Value = item.IdAvion.ToString() });
            }

            var vObj = TempData.Get<Vol>("VolObject");
            if (vObj == null)
            {
                vol = new Vol
                {
                    SelectedAvion = -1,
                    SelectedDepart = "-1",
                    SelectedArrivee = "-1",
                    AeroportsArrivee = new List<SelectListItem>()
                };
            }
            else
            {
                vol = vObj;
                vol.AeroportsArrivee = listAer;
            }

            vol.AeroportsDepart = listAer;
            vol.Avions = listAv;

            return View(vol);
        }

        public JsonResult GetlisteAeroport(string Code)
        {
            return Json(listAeroports.Where(x => x.CodeAeroport != Code).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveVol(Vol Vol)
        {
            var validator = new VolValidator();
            var validatorResult = validator.Validate(Vol);
            if (validatorResult.IsValid)
            {
                var SavedFlights = HttpContext.Session.GetObjectFromJson<List<Vol>>("SavedVols");
                if (SavedFlights != null)
                {
                    IDVol = IDVol + 1;
                    Vol.IdVol = IDVol;
                    SavedFlights.Add(Vol);
                    HttpContext.Session.SetObjectAsJson("SavedVols", SavedFlights);
                    return RedirectToRoute(new
                    {
                        controller = "Home",
                        action = "ListVols"
                    });
                }
                else
                {
                    List<Vol> vols = new List<Vol>();
                    Vol.IdVol = IDVol;
                    vols.Add(Vol);
                    HttpContext.Session.SetObjectAsJson("SavedVols", vols);
                    return RedirectToRoute(new
                    {
                        controller = "Home",
                        action = "ListVols",
                    });

                }
            }
            else
            {
                Vol.Errors = new List<string>();
                foreach (var item in validatorResult.Errors)
                {
                    Vol.Errors.Add(item.ErrorMessage);
                }
                TempDataExtensions.Put(TempData, "VolObject", Vol);

                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index",
                });
            }



        }


        public ActionResult ListVols()
        {
            var AllVols = HttpContext.Session.GetObjectFromJson<List<Vol>>("SavedVols");

            List<SelectListItem> listAer = new List<SelectListItem>();
            List<SelectListItem> listAv = new List<SelectListItem>();
            foreach (var item in listAeroports)
            {
                listAer.Add(new SelectListItem { Text = item.NomAeroport, Value = item.CodeAeroport.ToString() });
            }

            foreach (var item in listAvions)
            {
                listAv.Add(new SelectListItem { Text = item.Designation, Value = item.IdAvion.ToString() });
            }

            List<Vol> savedVols = new List<Vol>();

            foreach (var item in AllVols)
            {
                Vol Fly = new Vol();
                Fly.IdVol = item.IdVol;
                Fly.SelectedDepart = item.SelectedDepart;
                Fly.SelectedArrivee = item.SelectedArrivee;
                Fly.SelectedAvion = item.SelectedAvion;
                Fly.DateVol = item.DateVol;
                Fly.AeroportsArrivee = listAer;
                Fly.AeroportsDepart = listAer;
                Fly.Avions = listAv;
                var ConsAv = listAvions.FirstOrDefault(x => x.IdAvion == item.SelectedAvion).Consommation;
                var latArr = listAeroports.FirstOrDefault(x => x.CodeAeroport == item.SelectedArrivee).Latitude;
                var lonArr = listAeroports.FirstOrDefault(x => x.CodeAeroport == item.SelectedArrivee).Longitude;
                var latDep = listAeroports.FirstOrDefault(x => x.CodeAeroport == item.SelectedDepart).Latitude;
                var lonDep = listAeroports.FirstOrDefault(x => x.CodeAeroport == item.SelectedDepart).Longitude;
                Fly.DistanceBetweenAero = SuiviVols.Utility.FlightsUtility.GetDistanceFromLatLonInKm(latArr, lonArr, latDep, lonDep);
                Fly.ConsommationAvion = SuiviVols.Utility.FlightsUtility.ConsommationAvion(ConsAv, Fly.DistanceBetweenAero);
                savedVols.Add(Fly);
            }
            return View(savedVols);

        }

        public ActionResult UpdateVol(int Idvol)
        {
            List<SelectListItem> listAer = new List<SelectListItem>();
            List<SelectListItem> listAv = new List<SelectListItem>();
            foreach (var item in listAeroports)
            {
                listAer.Add(new SelectListItem { Text = item.NomAeroport, Value = item.CodeAeroport.ToString() });
            }

            foreach (var item in listAvions)
            {
                listAv.Add(new SelectListItem { Text = item.Designation, Value = item.IdAvion.ToString() });
            }
            var ListVols = HttpContext.Session.GetObjectFromJson<List<Vol>>("SavedVols");
            Vol Modelvol = new Vol();
            var volToUpdate = ListVols?.FirstOrDefault(x => x.IdVol == Idvol);
            if (volToUpdate != null)
            {
                Modelvol.AeroportsDepart = listAer;
                Modelvol.AeroportsArrivee = listAer;
                Modelvol.Avions = listAv;
                Modelvol.SelectedDepart = volToUpdate.SelectedDepart;
                Modelvol.SelectedArrivee = volToUpdate.SelectedArrivee;
                Modelvol.SelectedAvion = volToUpdate.SelectedAvion;
                Modelvol.DateVol = volToUpdate.DateVol;
                var ConsAv = listAvions.FirstOrDefault(x => x.IdAvion == volToUpdate.SelectedAvion).Consommation;
                var latArr = listAeroports.FirstOrDefault(x => x.CodeAeroport == volToUpdate.SelectedArrivee).Latitude;
                var lonArr = listAeroports.FirstOrDefault(x => x.CodeAeroport == volToUpdate.SelectedArrivee).Longitude;
                var latDep = listAeroports.FirstOrDefault(x => x.CodeAeroport == volToUpdate.SelectedDepart).Latitude;
                var lonDep = listAeroports.FirstOrDefault(x => x.CodeAeroport == volToUpdate.SelectedDepart).Longitude;
                Modelvol.DistanceBetweenAero = SuiviVols.Utility.FlightsUtility.GetDistanceFromLatLonInKm(latArr, lonArr, latDep, lonDep);
                Modelvol.ConsommationAvion = SuiviVols.Utility.FlightsUtility.ConsommationAvion(ConsAv, Modelvol.DistanceBetweenAero);
            }
            return View(Modelvol);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateVol(Vol vol)
        {
            var AllVols = HttpContext.Session.GetObjectFromJson<List<Vol>>("SavedVols");
            var VolUpdated = AllVols.FirstOrDefault(x => x.IdVol == vol.IdVol);
            VolUpdated.SelectedArrivee = vol.SelectedArrivee;
            VolUpdated.SelectedDepart = vol.SelectedDepart;
            VolUpdated.SelectedAvion = vol.SelectedAvion;
            VolUpdated.DateVol = vol.DateVol;
            HttpContext.Session.SetObjectAsJson("SavedVols", AllVols);

            return RedirectToRoute(new
            {
                controller = "Home",
                action = "ListVols"
            });

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
