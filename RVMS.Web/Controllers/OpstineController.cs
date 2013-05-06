using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RVMS.Model.Entities;
using RVMS.Model.Repository;
using RVMS.Web.Models;

namespace RVMS.Web.Controllers
{
    public class OpstineController : Controller
    {
        private readonly Repository<Opstina> fRepository = new Repository<Opstina>(); 
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult VratiOpstine()
        {
            var opstine = fRepository.GetAll();
            return Json(opstine, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Insert(Opstina municipality)
        {
            municipality.Aktivan = true;
            if (!ModelState.IsValid)
            {
                throw new HttpRequestValidationException("Objekat nije validan");
            }
            fRepository.Add(municipality);
            fRepository.Save();

        }

        [HttpPost]
        public void Update(Opstina municipality)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestValidationException("Objekat nije validan");
            }
            fRepository.Update(municipality);
            fRepository.Save();

        }

        public JsonResult VratiAktivneOpstine()
        {
            var opstine = fRepository.GetActive().OrderBy(x => x.NazivOpstine);
            return Json(opstine, JsonRequestBehavior.AllowGet);
        }

    }
}
