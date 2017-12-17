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
    public class EFSepetDal : ISepetDAL
    {
        public void MasaSepetleriSil(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                List<Sepet> ls = db.Sepet.Where(x => x.MasaID == masaID).ToList();
                foreach (var sepet in ls)
                {
                    db.Sepet.Remove(sepet);
                }
                db.SaveChanges();
            }
        }

        public void SepetEkle(Sepet s)
        {
            using (var db = new NusrETEntities())
            {
                Urunler u = db.Urunler.Find(s.UrunID);
                u.Stok = (u.Stok - s.Adet);
                db.Entry(u).State = EntityState.Modified;
                db.Sepet.Add(s);
                db.SaveChanges();
            }
        }

        public void SepetOnayla(int masaID)
        {
            using (var db = new NusrETEntities())
            {
                List<Sepet> ls = db.Sepet.Where(x => x.MasaID == masaID).ToList();
                List<Siparisler> ll = new List<Siparisler>();
                foreach (var item in ls)
                {
                    Siparisler s = new Siparisler();
                    s.GarsonID = item.GarsonID;
                    s.MasaID = item.MasaID;
                    s.SiparisZamani = DateTime.Now;
                    s.Statu = 0;
                    s.Adet = item.Adet;
                    s.UrunID = item.UrunID;
                    s.Tutar = item.Tutar;
                    ll.Add(s);
                    db.Sepet.Remove(item);
                }
                foreach (var item in ll)
                {
                    db.Siparisler.Add(item);
                }
                db.SaveChanges();
            }
        }

        public void SepettenCikar(int sepetID)
        {
            using (var db = new NusrETEntities())
            {
                Sepet s = db.Sepet.Find(sepetID);
                Urunler u = db.Urunler.Find(s.UrunID);
                u.Stok = (u.Stok + s.Adet);
                db.Entry(u).State = EntityState.Modified;
                db.Sepet.Remove(s);
                db.SaveChanges();
            }
        }
    }
}

