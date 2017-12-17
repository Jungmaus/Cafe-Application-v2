using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApplication.DAL.Abstract;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.DAL.Concrate
{
    public class EFGarsonDal : IGarsonDAL
    {
        public void AddGarson(Garsonlar g)
        {
            using (var db = new NusrETEntities())
            {
                db.Garsonlar.Add(g);
                db.SaveChanges();
            }
        }

        public void DeleteGarson(int garsonID)
        {
            using (var db = new NusrETEntities())
            {
                Garsonlar g = db.Garsonlar.Find(garsonID);
                List<Masalar> ls = db.Masalar.Where(x => x.GarsonID == g.Id).ToList();
                foreach (var item in ls)
                {
                    item.GarsonID = 0;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.Garsonlar.Remove(g);
                db.SaveChanges();
            }
        }

        public Garsonlar GetGarson(int garsonID)
        {
            using (var db = new NusrETEntities())
            {
                Garsonlar g = db.Garsonlar.Find(garsonID);
                return g;
            }
        }

        public List<Siparisler> GetGarsonAktifSiparisler(int garsonID)
        {
            using (var db = new NusrETEntities())
            {
                List<Siparisler> ls = db.Siparisler.OrderByDescending(x=>x.SiparisZamani).Where(x => x.GarsonID == garsonID && x.Statu == 1).ToList();
                return ls;
            }
        }

        public string GetGarsonIsim(int garsonID)
        {
            using (var db = new NusrETEntities())
            {
                Garsonlar g = db.Garsonlar.Find(garsonID);
                return g.Ad + " " + g.Soyad;
            }
        }

        public List<Garsonlar> GetGarsonList()
        {
            using (var db = new NusrETEntities())
            {
                List<Garsonlar> lg = db.Garsonlar.ToList();
                return lg;
            }
        }

        public List<Siparisler> GetGarsonSiparisler(int garsonID)
        {
            using (var db = new NusrETEntities())
            {
                List<Siparisler> ls = db.Siparisler.OrderByDescending(x=>x.SiparisZamani).Where(x => x.GarsonID == garsonID).ToList();
                return ls;
            }
        }

        public int Login(string username, string pass)
        {
            using (var db = new NusrETEntities())
            {
                Garsonlar g = db.Garsonlar.FirstOrDefault(x => x.Kadi == username && x.Sifre == pass);
                if (g != null)
                    return 1;
                else
                    return 0;
            }
        }

        public void UpdateGarson(Garsonlar g)
        {
            using (var db = new NusrETEntities())
            {
                db.Entry(g).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}



