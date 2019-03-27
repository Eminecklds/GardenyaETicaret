using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class Urunler
    {
        [Key]
        public int UrunID { get; set; }
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public string Resim { get; set; }
        public int Adet { get; set; }
        public int SatılanAdet { get; set; }
        public int Fiyat { get; set; }
        [Display(Name = "Eklenme Tarihi")]
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public int AltKategoriID { get; set; }
        public virtual AltKategori AltKategori { get; set; }
        public string GirisimciID { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        //public int GirisimciID { get; set; }
        //public virtual Girisimci Girisimci { get; set; }
        public virtual List<SiparisDetay> SiparisDetays { get; set; }

        //[Key]
        //public int GirisimciID { get; set; }
        //[Required]
        //public string Ad { get; set; }
        //[Required]
        //public string Soyad { get; set; }
        //[Required]
        //[StringLength(11, ErrorMessage = "TC 11 haneli olmalıdır", MinimumLength = 11)]
        //public string TC { get; set; }
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
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "Parola ve parola tekrar eşleşmemektedir")]
        //public string ParolaTekrar { get; set; }
        //public virtual List<Urunler> Urunlers { get; set; }
    }
}