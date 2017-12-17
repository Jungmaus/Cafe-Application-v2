using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.DAL.Abstract
{
    interface IMasaDAL
    {
        Masalar GetMasa(int masaID);
        void MasaEkle(Masalar m);
        void MasaSil(int masaID);
        void MasaAc(int masaID);
        void MasaKapat(int masaID);
        void GarsonKaldir(int masaID);
        void GarsonEkle(int masaID, int garsonID);
        int MasaToplamTutar(int masaID);
        List<Siparisler> MasaAktifSiparisleri(int masaID);
        List<Sepet> MasaAktifSepetleri(int masaID);
        List<Masalar> GetMasaListesi();
        Garsonlar GetGarson(int masaID);
        List<Siparisler> MasaTeslimEdilenSiparisler(int masaID);
        Masalar GetMasaWithNo(int masaNo);
    }
}
