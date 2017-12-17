using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.Dal.Abstract
{
    interface IUrun
    {
        Urunler GetUrun(int UrunID);
        void UrunEkle(Urunler u);
        void UrunSil(int UrunID);
        void UrunGuncelle(Urunler u);
        List<Urunler> GetUrunList();
    }
}
