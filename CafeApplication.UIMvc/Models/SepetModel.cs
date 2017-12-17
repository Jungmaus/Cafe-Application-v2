using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeApplication.UIMvc.Models
{
    public class SepetModel
    {
        [Required]
        public int Id { get; set; }
        public string UrunAd { get; set; }
        public string ResimYolu { get; set; }
        public int UrunFiyat { get; set; }
        public int UrunAdet { get; set; }
        public int Tutar { get; set; }
    }
}