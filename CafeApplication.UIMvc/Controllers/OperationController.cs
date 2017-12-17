using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using CafeApplication.Dal.Concrate;
using CafeApplication.DAL.Concrate;
using CafeApplication.Entities.Concrate;
using CafeApplication.UIMvc.Models;

namespace CafeApplication.UIMvc.Controllers
{
    public class OperationController : Controller
    {
        EFMasaDal efMasa = new EFMasaDal();
        EFCategoryDal efCategory = new EFCategoryDal();
        EFGarsonDal efGarson = new EFGarsonDal();
        EFSepetDal efSepet = new EFSepetDal();
        EFSiparisDal efSiparis = new EFSiparisDal();
        EFUrunDal efUrun = new EFUrunDal();


        public ActionResult EditGarson(int Id)
        {
            int control = SessionControl();
            if (control == 1)
            {
                Garsonlar g = efGarson.GetGarson(Id);
                if (g != null)
                {
                    ViewBag.GarsonAd = efGarson.GetGarsonIsim(g.Id);
                    Session["GarsonId"] = g.Id;
                }
                else
                    return RedirectToAction("Login", "Login");
                return View(g);
            }
            else
                return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult EditGarson(Garsonlar g)
        {
            try
            {
                ViewBag.GarsonAd = efGarson.GetGarsonIsim(Convert.ToInt32(Session["GarsonId"]));
                efGarson.UpdateGarson(g);
                ViewBag.Msg = "1";
                Garsonlar gc = efGarson.GetGarson(Convert.ToInt32(Session["GarsonId"]));
                return View(gc);
            }
            catch
            {
                ViewBag.GarsonAd = efGarson.GetGarsonIsim(Convert.ToInt32(Session["GarsonId"]));
                Garsonlar gc = efGarson.GetGarson(Convert.ToInt32(Session["GarsonId"]));
                ViewBag.Msg = "0";
                return View(gc);
            }
        }

        public ActionResult GarsonEkle(Garsonlar g)
        {
            if(g != null)
                efGarson.AddGarson(g);
            return RedirectToAction("Garsonlar", "Home");
        }

        public ActionResult DeleteGarson(int Id)
        {
            int control = SessionControl();
            if (control == 1)
            {
                Session["SilinecekGarsonID"] = Id;
            ViewBag.Garsonisim = efGarson.GetGarsonIsim(Id);
            return View();
            }
            else
                return RedirectToAction("Login", "Login");
        }

        public ActionResult DeleteSure()
        {
            int garsonID = Convert.ToInt32(Session["SilinecekGarsonID"]);
            efGarson.DeleteGarson(garsonID);
            return RedirectToAction("Garsonlar", "Home");
        }

        public ActionResult CikisYap()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }


        public ActionResult UrunSil(int Id)
        {
            int control = SessionControl();
            if (control == 1)
            {
                Session["UrunID"] = Id;
                ViewBag.UrunAd = efUrun.GetUrun(Id).Ad;
                return View();
            }
            else
                return RedirectToAction("Login", "Login");
        }


        public ActionResult UrunSilConfirm()
        {
            efUrun.UrunSil(Convert.ToInt32(Session["UrunID"]));
            return RedirectToAction("Stok", "Home");
        }

        public ActionResult UrunEdit(int Id)
        {
            int control = SessionControl();
            if (control == 1)
            {
                Urunler u = efUrun.GetUrun(Id);
            if (u != null)
            {
                List<SelectListItem> dropw = new List<SelectListItem>();
                List<Kategoriler> lk = efCategory.GetKategoriList();
                foreach (var item in lk)
                {
                    dropw.Add(new SelectListItem
                    {
                        Text = item.Ad,
                        Value = item.Id.ToString()
                    });
                }
                ViewData["CategoryName"] = dropw;
                    return View(u);
            }
            else
                return RedirectToAction("Stok", "Home");
            }
            else
                return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult UrunEdit(UrunModel um, HttpPostedFileBase file)
        {
            try
            {
                if (um != null)
                {
                    List<SelectListItem> dropw = new List<SelectListItem>();
                    List<Kategoriler> lk = efCategory.GetKategoriList();
                    foreach (var item in lk)
                    {
                        dropw.Add(new SelectListItem
                        {
                            Text = item.Ad,
                            Value = item.Id.ToString()
                        });
                    }
                    ViewData["CategoryName"] = dropw;
                    Urunler u = new Urunler();
                    u.Id = um.UrunId;
                    u.Ad = um.Ad;
                    u.Fiyat = um.Fiyat;
                    u.CategoryID = Convert.ToInt32(um.CategoryName);
                    u.Stok = um.Stok;
                    Urunler mu = efUrun.GetUrun(um.UrunId);
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            int ctgID = Convert.ToInt32(um.CategoryName);
                            string guid = Guid.NewGuid().ToString();
                            um.CategoryName = efCategory.GetKategori(ctgID).Ad;
                            string filePath = Path.Combine(Server.MapPath("~/UrunResimleri/" + um.CategoryName),
                                guid + "_" + Path.GetFileName(file.FileName));
                            file.SaveAs(filePath);
                            u.ResimYolu = "/UrunResimleri/" + um.CategoryName.Trim() + "/" + guid + "_" +
                                          Path.GetFileName(file.FileName);
                        }
                        else
                        {
                            u.ResimYolu = efUrun.GetUrun(um.UrunId).ResimYolu;
                        }
                    }
                    else
                    {
                        u.ResimYolu = efUrun.GetUrun(um.UrunId).ResimYolu;
                    }
                    efUrun.UrunGuncelle(u);
                    ViewBag.Msg = "1";
                    return View(mu);
                }
                else
                {
                    Urunler mu = efUrun.GetUrun(um.UrunId);
                    return View(mu);
                }
            }
            catch
            {
                Urunler mu = efUrun.GetUrun(um.UrunId);
                ViewBag.Msg = "0";
                return View(mu);
            }
        }

        int SessionControl()
        {
            if (Session["Kadi"] != null)
                return 1;
            else
                return 0;
        }

    }
}