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
    public class EFCategoryDal : ICategoryDal
    {
        public void AddKategori(Kategoriler k)
        {
            using (var db = new NusrETEntities())
            {
                db.Kategoriler.Add(k);
                db.SaveChanges();
            }
        }

        public void DelKategori(int categoryID)
        {
            using (var db = new NusrETEntities())
            {
                Kategoriler k = db.Kategoriler.Find(categoryID);
                db.Kategoriler.Remove(k);
                db.SaveChanges();
            }
        }

        public Kategoriler GetKategori(int categoryID)
        {
            using (var db = new NusrETEntities())
            {
                Kategoriler k = db.Kategoriler.Find(categoryID);
                return k;
            }
        }

        public List<Kategoriler> GetKategoriList()
        {
            using (var db = new NusrETEntities())
            {
                List<Kategoriler> k = db.Kategoriler.ToList();
                return k;
            }
        }

        public void UpdateKategori(Kategoriler k)
        {
            using (var db = new NusrETEntities())
            {
                db.Entry(k).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
