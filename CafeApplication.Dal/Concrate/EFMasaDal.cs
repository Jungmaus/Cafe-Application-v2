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
    public class EFMasaDal : IMasaDAL
    {
        public void GarsonEkle(int masaID, int garsonID)
        {
            using (var db = new NusrETEntities())
            {
                Masalar m = db.Masalar.Find(masaID);
                if (m.GarsonID != 0 || m.GarsonID != null)
                {
                    m.GarsonID = garsonID;
                    db.Entry(m).State = EntityState.Modified;
                    Garsonlar g = db.Garsonlar.Find(garsonID);
                    g.Statu = 1;
                    db.Entry(g).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void GarsonKaldir(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                Masalar m = db.Masalar.Find(masaID);
                if (m.GarsonID != 0 || m.GarsonID != null)
                {
                    m.GarsonID = 0;
                    db.Entry(m).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public Garsonlar GetGarson(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                Masalar m = db.Masalar.Find(masaID);
                Garsonlar g = new Garsonlar();
                if (m != null)
                { 
                    if (m.GarsonID != null || m.GarsonID != 0)
                    {
                        g = db.Garsonlar.Find(m.GarsonID);
                    }
                }
                return g;
            }
        }

        public Masalar GetMasa(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                Masalar m = db.Masalar.Find(masaID);
                return m;
            }
        }

        public List<Masalar> GetMasaListesi()
        {
            using (var db = new NusrETEntities())
            {
                List<Masalar> lm = db.Masalar.ToList();
                return lm;
            }
        }

        public Masalar GetMasaWithNo(int masaNo)
        {
            using (var db = new NusrETEntities())
            {
                Masalar m = db.Masalar.First(x => x.MasaNo == masaNo);
                return m;
            }
        }

        public void MasaAc(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                Masalar m = db.Masalar.Find(masaID);
                if (m.Statu == 0)
                {
                    m.Statu = 1;
                    db.Entry(m).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public List<Sepet> MasaAktifSepetleri(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                List<Sepet> ls = db.Sepet.Where(x => x.MasaID == masaID && x.Statu == 1).ToList();
                return ls;
            }
        }

        public List<Siparisler> MasaAktifSiparisleri(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                List<Siparisler> ls = db.Siparisler.OrderByDescending(x=>x.SiparisZamani).Where(x => x.MasaID == masaID).ToList();
                return ls;
            }
        }

        public void MasaEkle(Masalar m)
        {
            using (var db = new NusrETEntities())
            {
                db.Masalar.Add(m);
                db.SaveChanges();
            }
        }

        public void MasaKapat(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                Masalar m = db.Masalar.Find(masaID);
                if (m.Statu == 1)
                {
                    List<Siparisler> siparisList = MasaAktifSiparisleri(masaID);
                    foreach (var item in siparisList)
                    {
                        Siparisler s = db.Siparisler.Find(item.Id);
                        db.Siparisler.Remove(s);
                    }
                    List<Sepet> sepetList = MasaAktifSepetleri(masaID);
                    foreach (var item in sepetList)
                    {
                        Sepet s = db.Sepet.Find(item.Id);
                        db.Sepet.Remove(s);
                    }
                    m.Statu = 0;
                    Garsonlar g = db.Garsonlar.Find(m.GarsonID);
                    g.Statu = 0;
                    m.GarsonID = 0;
                    db.Entry(m).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void MasaSil(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                Masalar m = db.Masalar.Find(masaID);
                db.Masalar.Remove(m);
                db.SaveChanges();
            }
        }

        public List<Siparisler> MasaTeslimEdilenSiparisler(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                List<Siparisler> ls = db.Siparisler.Where(x => x.MasaID == masaID && x.Statu == 1).ToList();
                return ls;
            }
        }

        public int MasaToplamTutar(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                List<Siparisler> ls = db.Siparisler.Where(x => x.MasaID == masaID && x.Statu == 1).ToList();
                int tutar = 0;
                foreach (var siparisler in ls)
                {
                    Urunler u = db.Urunler.Find(siparisler.UrunID);
                    tutar = tutar + (u.Fiyat * siparisler.Adet);
                }
                return tutar;
            }
        }
    }
}

