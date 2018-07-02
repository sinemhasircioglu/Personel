using Personel.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personel.Controllers
{
    public class DepartmanController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        
        public ActionResult Index()
        {
            var model = db.Departmen.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Kaydet()
        {
            return View("DepartmanForm");
        }

        [HttpPost]
        public ActionResult Kaydet(Departman departman)
        {
            if(departman.Id==0)
            {
                db.Departmen.Add(departman);
            }
            else
            {
                var guncellenecekDepartman = db.Departmen.Find(departman.Id);
                if (guncellenecekDepartman == null)
                    return HttpNotFound();
                else
                {
                    guncellenecekDepartman.Ad = departman.Ad;
                }
            }
            db.SaveChanges();           
            return RedirectToAction("Index","Departman");
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departmen.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm",model);
        }

        public ActionResult Sil(int id)
        {
            var departman = db.Departmen.Find(id);
            if (departman == null)
                return HttpNotFound();
            db.Departmen.Remove(departman);
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }
    }
}