using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.DAL.Abstract
{
    interface ISiparisDAL
    {
        void SiparisEkle(Siparisler s);
        void SiparisSil(int siparisID);
        List<Siparisler> TeslimBekleyenSiparisler();
        List<Siparisler> TumSiparisler();
        void SiparisTeslimEt(int siparisID);
        void SiparisIptalEt(int siparisID);
    }
}
