using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.UIMvc.Models
{
    public class UrunModel
    {
        public int UrunId { get; set; }
        public string Ad { get; set; }
        public int Fiyat { get; set; }
        public int Stok { get; set; }
        public string ResimYolu { get; set; }
        public string CategoryName { get; set; }
    }
}