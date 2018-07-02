using Personel.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Personel.ViewModels;

namespace Personel.Controllers
{
    public class PersonelController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();

        public ActionResult Index()
        {
            var model = db.Personels.Include(x=>x.Departman).ToList();
            return View(model);
        }


        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel() {
                Departmanlar=db.Departmen.ToList()
            };
            return View("PersonelForm",model);
        }

        public ActionResult Kaydet(Personel.Models.Entity_Framework.Personel personel)
        {
            if(personel.Id==0) //ekleme
            {
                db.Personels.Add(personel);
            } else //güncelleme
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Personel");
        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departmen.ToList(),
                Personel=db.Personels.Find(id)
            };
            return View("PersonelForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecek = db.Personels.Find(id);
            if (silinecek == null)
                return HttpNotFound();
            db.Personels.Remove(silinecek);
            db.SaveChanges();
            return RedirectToAction("Index", "Personel");
        }
    }
}