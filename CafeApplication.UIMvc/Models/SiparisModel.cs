using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.UIMvc.Models
{
    public class SiparisModel
    {
        public int Id { get; set; }
        public int MasaID { get; set; }
        public int MasaNo { get; set; }
        public string UrunResmi { get; set; }
        public string UrunAD { get; set; }
        public int UrunFiyat { get; set; }
        public int Adet { get; set; }
        public int Tutar { get; set; }
        public System.DateTime SiparisZamani { get; set; }
        public string GarsonIsmi { get; set; }
        public int Durum { get; set; }

        public int TutarHesapla(int fiyat, int adet)
        {
            return (fiyat * adet);
        }
    }
}