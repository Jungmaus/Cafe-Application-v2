using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.DAL.Abstract
{
    interface ISepetDAL
    {
        void SepetEkle(Sepet s);
        void MasaSepetleriSil(int masaID);
        void SepetOnayla(int masaID);
        void SepettenCikar(int sepetID);
    }
}
