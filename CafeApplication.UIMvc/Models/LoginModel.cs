using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeApplication.UIMvc.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(20,ErrorMessage = "Maksimum 20 hane girebilirsiniz.")]
        public string Username { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Maksimum 20 hane girebilirsiniz.")]
        public string Password { get; set; }
    }
}