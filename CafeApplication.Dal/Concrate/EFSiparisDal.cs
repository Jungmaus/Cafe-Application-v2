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
    public class EFSiparisDal : ISiparisDAL
    {
        public void SiparisEkle(Siparisler s)
        {
            using (var db = new NusrETEntities())
            {
                if (s.Statu != 0)
                    s.Statu = 0;
                db.Siparisler.Add(s);
                Masalar m = db.Masalar.Find(s.MasaID);
                if (m.Statu != 1)
                {
                    m.Statu = 1;
                    db.Entry(m).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }

        public void SiparisIptalEt(int siparisID)
        {
            using (var db = new NusrETEntities())
            {
                Siparisler s = db.Siparisler.Find(siparisID);
                if (s.Statu == 0)
                {
                    Urunler u = db.Urunler.Find(s.UrunID);
                    u.Stok = (u.Stok + s.Adet);
                    db.Entry(u).State = EntityState.Modified;
                    db.Siparisler.Remove(s);
                    db.SaveChanges();
                }
            }
        }

        public void SiparisSil(int siparisID)
        {
            using (var db = new NusrETEntities())
            {
                Siparisler s = db.Siparisler.Find(siparisID);
                Urunler u = db.Urunler.Find(s.UrunID);
                u.Stok = (u.Stok + s.Adet);
                db.Entry(u).State = EntityState.Modified;
                db.Siparisler.Remove(s);
                db.SaveChanges();
            }
        }

        public void SiparisTeslimEt(int siparisID)
        {
            using (var db = new NusrETEntities())
            {
                Siparisler s = db.Siparisler.Find(siparisID);
                if (s.Statu != 1)
                {
                    s.Statu = 1;
                    db.Entry(s).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public List<Siparisler> TeslimBekleyenSiparisler()
        {
            using (var db = new NusrETEntities())
            {
                List<Siparisler> ls = db.Siparisler.Where(x => x.Statu == 0).ToList();
                return ls;
            }
        }

        public List<Siparisler> TumSiparisler()
        {
            using (var db = new NusrETEntities())
            {
                List<Siparisler> ls = db.Siparisler.ToList();
                return ls;
            }
        }
    }
}

