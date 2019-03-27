using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class Kullanici:ApplicationUser
    {
        [Key]
        public int KullaniciID { get; set; }
        //[Required]
        //public string Ad { get; set; }
        //[Required]
        //public string Soyad { get; set; }
        public DateTime? UyelikTarihi { get; set; } = DateTime.Now;

        //public string Resim { get; set; } 
        //[Required]
        //[DataType(DataType.PhoneNumber)]
        //public string Tel { get; set; }
        //[Required]
        //public string Adres { get; set; }
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }
        //[Required]
        //[StringLength(100, ErrorMessage = "Parolanız en az 6 karakterden oluşmalıdır", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Parola { get; set; }

        public virtual List<Siparis> Siparis { get; set; }
        public virtual List<Urunler> Urunlers { get; set; }

    }
}