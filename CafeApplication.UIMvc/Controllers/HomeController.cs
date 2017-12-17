using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CafeApplication.Dal.Concrate;
using CafeApplication.DAL.Concrate;
using CafeApplication.UIMvc.Models;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.UIMvc.Controllers
{
    public class HomeController : Controller
    {
        EFMasaDal efMasa = new EFMasaDal();
        EFCategoryDal efCategory = new EFCategoryDal();
        EFGarsonDal efGarson = new EFGarsonDal();
        EFSepetDal efSepet = new EFSepetDal();
        EFSiparisDal efSiparis = new EFSiparisDal();
        EFUrunDal efUrun = new EFUrunDal();

        // GET: Home
        public ActionResult Masalar()
        {
            int control = SessionControl();
            if (control == 1)
            {
                List<MasaModel> lmm = new List<MasaModel>();
                List<Masalar> lm = efMasa.GetMasaListesi();
                foreach (var item in lm)
                {
                    MasaModel m = new MasaModel();
                    m.Statu = item.Statu;
                    if (item.GarsonID != null && item.GarsonID != 0)
                        m.GarsonIsmi = efGarson.GetGarsonIsim((int)item.GarsonID);
                    m.MasaID = item.Id;
                    m.MasaNo = item.MasaNo;
                    m.ToplamTutar = efMasa.MasaToplamTutar(item.Id);
                    lmm.Add(m);
                }
                return View(lmm);
            }
            else
                return RedirectToAction("Login", "Login");
        }


        public ActionResult Siparisler()
        {
            List<Siparisler> ls = efSiparis.TumSiparisler();
            List<SiparisModel> lm = new List<SiparisModel>();

            foreach (var item in ls)
            {
                Urunler u = efUrun.GetUrun(item.UrunID);
                SiparisModel sm = new SiparisModel();
                sm.Id = item.Id;
                sm.Durum = item.Statu;
                sm.Adet = item.Adet;
                sm.GarsonIsmi = efGarson.GetGarsonIsim(item.GarsonID);
                sm.MasaID = item.MasaID;
                sm.SiparisZamani = item.SiparisZamani;
                sm.Tutar = item.Tutar;
                sm.UrunAD = u.Ad;
                sm.UrunFiyat = u.Fiyat;
                sm.UrunResmi = u.ResimYolu;
                lm.Add(sm);
            }
            return View(lm);
        }



        public ActionResult Garsonlar()
        {
            List<Garsonlar> lg = efGarson.GetGarsonList();
            return View(lg);
        }


        public ActionResult Stok()
        {
            if (Session["Path"] != null)
            {
                Response.Write("<script>alert('" + Session["Path"].ToString() + "');</script>");
            }

            List<Urunler> lu = efUrun.GetUrunList();
            List<UrunModel> lm = new List<UrunModel>();
            foreach (var item in lu)
            {
                UrunModel um = new UrunModel();
                um.Stok = item.Stok;
                um.Ad = item.Ad;
                um.CategoryName = efCategory.GetKategori(item.CategoryID).Ad;
                um.Fiyat = item.Fiyat;
                um.ResimYolu = item.ResimYolu;
                um.UrunId = item.Id;
                lm.Add(um);
            }

            List<Kategoriler> lk = efCategory.GetKategoriList();
            List<SelectListItem> dropwItems = new List<SelectListItem>();
            foreach (var item in lk)
            {
                dropwItems.Add(new SelectListItem
                {
                    Text = item.Ad,
                    Value = item.Id.ToString()
                });
            }
            ViewData["CategoryName"] = dropwItems;

            return View(lm);
        }



        public ActionResult MasaDetay(int Id)
        {
            int control = SessionControl();
            if (control == 1)
            {
                Session["MasaID"] = Id;
                ViewData["MasaID"] = Id;
                ViewData["GarsonID"] = efMasa.GetGarson(Id);
                Masalar m = efMasa.GetMasa(Id);
                List<SiparisModel> ls = new List<SiparisModel>();
                var siparisList = efMasa.MasaAktifSiparisleri(Id).ToList();
                foreach (var item in siparisList)
                {
                    Urunler u = efUrun.GetUrun(item.UrunID);
                    SiparisModel s = new SiparisModel();
                    s.Id = item.Id;
                    if (m.GarsonID != null && m.GarsonID != 0)
                        s.GarsonIsmi = efGarson.GetGarsonIsim(Convert.ToInt32(m.GarsonID));
                    s.Adet = item.Adet;
                    s.UrunFiyat = u.Fiyat;
                    s.MasaID = m.Id;
                    s.MasaNo = m.MasaNo;
                    s.Durum = item.Statu;
                    s.UrunAD = u.Ad;
                    s.Tutar = s.TutarHesapla(s.Adet, u.Fiyat);
                    s.SiparisZamani = item.SiparisZamani;
                    s.UrunResmi = u.ResimYolu.Trim();
                    ls.Add(s);
                }
                return View(ls);
            }
            else
                return RedirectToAction("Login", "Login");
        }


        public ActionResult GarsonAta(int GarsonID)
        {
            int masaID = Convert.ToInt32(Session["MasaID"]);
            efMasa.GarsonEkle(masaID, GarsonID);
            return RedirectToAction("MasaDetay", "Home", new { Id = masaID });
        }

        public ActionResult GarsonKaldir()
        {
            int masaID = Convert.ToInt32(Session["MasaID"]);
            efMasa.GarsonKaldir(masaID);
            return RedirectToAction("MasaDetay", "Home", new { Id = masaID });
        }


        public ActionResult SepeteEkle(int Id, int Adet)
        {
            int masaID = Convert.ToInt32(Session["MasaID"]);
            Urunler u = efUrun.GetUrun(Id);
            if (u.Stok > Adet)
            {
                Sepet s = new Sepet();
                s.GarsonID = Convert.ToInt32(efMasa.GetGarson(masaID).Id);
                s.Adet = Adet;
                s.UrunID = Id;
                s.Tutar = (Adet * u.Fiyat);
                s.MasaID = masaID;
                s.Statu = 1;
                efSepet.SepetEkle(s);
            }
            return RedirectToAction("MasaDetay", "Home", new { Id = masaID });
        }

        public ActionResult SepettenCikar(int Id)
        {
            efSepet.SepettenCikar(Id);
            return ReturnMasaDetay();
        }


        public ActionResult SiparisIptal(int Id)
        {
            int masaID = Convert.ToInt32(Session["MasaID"]);
            efSiparis.SiparisIptalEt(Id);
            if (efMasa.MasaAktifSiparisleri(masaID).Count < 1)
                efMasa.MasaKapat(masaID);
            return RedirectToAction("MasaDetay", "Home", new { Id = masaID });
        }

        public ActionResult SiparisTeslimEt(int Id)
        {
            efSiparis.SiparisTeslimEt(Id);
            return ReturnMasaDetay();
        }

        public ActionResult MasaKapat(int Id)
        {
            Masalar m = efMasa.GetMasaWithNo(Id);
            efMasa.MasaKapat(m.Id);
            return RedirectToAction("Masalar", "Home");
        }

        public ActionResult SepetiOnayla(int Id)
        {
            efMasa.MasaAc(Id);
            efSepet.SepetOnayla(Id);
            return ReturnMasaDetay();
        }


        int SessionControl()
        {
            if (Session["Kadi"] != null)
                return 1;
            else
                return 0;
        }

        ActionResult ReturnMasaDetay()
        {
            int masaID = Convert.ToInt32(Session["MasaID"]);
            return RedirectToAction("MasaDetay", "Home", new { Id = masaID });
        }

        public ActionResult UrunKaydet(UrunModel um, HttpPostedFileBase Resim)
        {
            if (Resim.ContentLength > 0 && um.Fiyat > 0)
            {
                int ctgID = Convert.ToInt32(um.CategoryName);
                string guid = Guid.NewGuid().ToString();
                um.CategoryName = efCategory.GetKategori(ctgID).Ad;
                string filePath = Path.Combine(Server.MapPath("~/UrunResimleri/" + um.CategoryName),
                    guid + "_" + Path.GetFileName(Resim.FileName));
                Resim.SaveAs(filePath);

                Urunler u = new Urunler();
                u.Stok = um.Stok;
                u.Ad = um.Ad;
                u.CategoryID = ctgID;
                u.Fiyat = um.Fiyat;
                u.ResimYolu = "/UrunResimleri/" + um.CategoryName.Trim() + "/" + guid + "_" +
                              Path.GetFileName(Resim.FileName);
                efUrun.UrunEkle(u);

                Session["Path"] = filePath;
            }
            return RedirectToAction("Stok", "Home");
        }


    }
}