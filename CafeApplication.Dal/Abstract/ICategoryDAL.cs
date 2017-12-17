using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.DAL.Abstract
{
    interface ICategoryDal
    {
        Kategoriler GetKategori(int categoryID);
        void AddKategori(Kategoriler k);
        void DelKategori(int categoryID);
        void UpdateKategori(Kategoriler k);
        List<Kategoriler> GetKategoriList();
    }
}
