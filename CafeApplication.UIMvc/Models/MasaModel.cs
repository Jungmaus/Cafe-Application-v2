using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.UIMvc.Models
{
    public class MasaModel
    {
        public int MasaID { get; set; }
        public int MasaNo { get; set; }
        public string GarsonIsmi { get; set; }
        public int ToplamTutar { get; set; }
        public int Statu { get; set; }
    }
}