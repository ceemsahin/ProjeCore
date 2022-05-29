using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCore.Controllers
{
    public class PersonelimController : Controller
    {

        Context c = new Context();
        [Authorize]


        public IActionResult Index()
        {
            var degerler = c.Personels.Include(x => x.Birim).ToList();




            return View(degerler);
        }
        [HttpGet]
       public IActionResult YeniPersonel()
        {
            List<SelectListItem> degerler = (from x in c.Birims.ToList()
                                             select new SelectListItem
                                             {

                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }
            ).ToList();
            ViewBag.dgr = degerler;

            return View();
        }


        public IActionResult YeniPersonel(Personel p)
        {
            var per = c.Birims.Where(x => x.BirimID == p.Birim.BirimID).FirstOrDefault();
            p.Birim = per;
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult PersonelSil(int id)
        {

            var dep = c.Personels.Find(id);
            c.Personels.Remove(dep);
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult PersonelGetir(int id)
        {

            var depart = c.Personels.Find(id);
            return View("PersonelGetir", depart);

        }


        public IActionResult PersonelGuncelle(Personel d)
        {
           
            var dep = c.Personels.Find(d.PersonelID);
            dep.Ad = d.Ad;
            dep.Soyad = d.Soyad;
            dep.Sehir = d.Sehir;
            dep.BirimID = d.BirimID;

            c.SaveChanges();
            return RedirectToAction("Index");

        }



        



    }
}
