using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApplication.Dal.Abstract;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.Dal.Concrate
{
    public class EFUrunDal : IUrun
    {
        public Urunler GetUrun(int UrunID)
        {
            using (var db = new NusrETEntities())
            {
                Urunler u = db.Urunler.Find(UrunID);
                return u;
            }
        }

        public List<Urunler> GetUrunList()
        {
            using (var db = new NusrETEntities())
            {
                List<Urunler> lu = db.Urunler.ToList();
                return lu;
            }
        }

        public void UrunEkle(Urunler u)
        {
            using (var db = new NusrETEntities())
            {
                u.Ad = u.Ad.Trim();
                u.ResimYolu = u.ResimYolu.Trim();
                db.Urunler.Add(u);
                db.SaveChanges();
            }
        }

        public void UrunGuncelle(Urunler u)
        {
            using (var db = new NusrETEntities())
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void UrunSil(int UrunID)
        {
            using (var db = new NusrETEntities())
            {
                Urunler u = db.Urunler.Find(UrunID);
                string imgPath = "~" + u.ResimYolu;
                if (File.Exists(imgPath))
                    File.Delete(imgPath);
                db.Urunler.Remove(u);
                db.SaveChanges();
            }
        }
    }
}
