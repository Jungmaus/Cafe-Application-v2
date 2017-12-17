using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeApplication.Dal.Concrate;
using CafeApplication.DAL.Concrate;
using CafeApplication.Entities.Concrate;
using CafeApplication.UIMvc.Models;

namespace CafeApplication.UIMvc.Controllers
{
    public class PartController : Controller
    {
        EFMasaDal efMasa = new EFMasaDal();
        EFCategoryDal efCategory = new EFCategoryDal();
        EFGarsonDal efGarson = new EFGarsonDal();
        EFSepetDal efSepet = new EFSepetDal();
        EFSiparisDal efSiparis = new EFSiparisDal();
        EFUrunDal efUrun = new EFUrunDal();

        // GET: Part
        [ChildActionOnly]
        public ViewResult GarsonDetay()
        {
            int masaID = Convert.ToInt32(Session["MasaID"]);
            Garsonlar g = efMasa.GetGarson(masaID);
            List<Garsonlar> lg = efGarson.GetGarsonList();
            List<SelectListItem> dropwItems = new List<SelectListItem>();
            foreach (var garson in lg)
            {
                dropwItems.Add(new SelectListItem
                {
                    Text = "(" + garson.Id + ")" + garson.Ad + garson.Soyad,
                    Value = garson.Id.ToString()
                });
            }
            ViewData["DataListGarson"] = dropwItems;
            return View(g);
        }

        [ChildActionOnly]
        public ViewResult PartUrunEkle()
        {
            int masaID = Convert.ToInt32(Session["MasaID"]);
            ViewBag.MasaID = masaID;
            List<SepetModel> ls = new List<SepetModel>();
            var sepetList = efMasa.MasaAktifSepetleri(masaID);
            foreach (var item in sepetList)
            {
                SepetModel s = new SepetModel();
                Urunler u = efUrun.GetUrun(item.UrunID);
                s.Id = item.Id;
                s.ResimYolu = u.ResimYolu;
                s.UrunAd = u.Ad;
                s.UrunAdet = item.Adet;
                s.UrunFiyat = u.Fiyat;
                s.Tutar = item.Tutar;
                ls.Add(s);
            }
            return View(ls);
        }

        [ChildActionOnly]
        public ViewResult UrunListesi()
        {
            List<UrunModel> lu = new List<UrunModel>();
            var urunList = efUrun.GetUrunList();
            foreach (var item in urunList)
            {
                UrunModel u = new UrunModel();
                u.UrunId = item.Id;
                u.Fiyat = item.Fiyat;
                u.Ad = item.Ad;
                u.CategoryName = efCategory.GetKategori(item.CategoryID).ToString();
                u.ResimYolu = item.ResimYolu;
                u.Stok = item.Stok;
                lu.Add(u);
            }
            return View(lu);
        }

        [ChildActionOnly]
        public ViewResult MasaKapat(int Id)
        {
            ViewBag.MasaNo = efMasa.GetMasa(Id).MasaNo;
            ViewBag.MasaKat = efMasa.GetMasa(Id).Kat;
            if(efMasa.GetGarson(Id) != null)
            ViewBag.GarsonAd = efGarson.GetGarsonIsim(efMasa.GetGarson(Id).Id);
            List<SiparisModel> ls = new List<SiparisModel>();
            var siparisList = efMasa.MasaTeslimEdilenSiparisler(Id);
            foreach (var item in siparisList)
            {
                Urunler u = efUrun.GetUrun(item.UrunID);
                SiparisModel sm = new SiparisModel();
                sm.Adet = item.Adet;
                sm.SiparisZamani = item.SiparisZamani;
                sm.Tutar = item.Tutar;
                sm.UrunAD = u.Ad;
                sm.UrunFiyat = u.Fiyat;
                ls.Add(sm);
            }
            ViewBag.ToplamTutar = efMasa.MasaToplamTutar(Id);
            return View(ls);
        }


    }
}