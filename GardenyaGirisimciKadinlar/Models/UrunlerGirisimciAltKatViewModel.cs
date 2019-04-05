using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class UrunlerGirisimciAltKatViewModel
    {
        [Key]
        public int ID { get; set; }
        public int AltKategoriID { get; set; }
        public int KategoriID { get; set; }
        public int GirisimciID { get; set; }
        public int UrunID { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Resim { get; set; }
        public int Fiyat { get; set; }
        public string AltKategoriAdi { get; set; }
        public string GirisimciAdi { get; set; }
        public int Adet { get; set; }
   
        public ICollection<AltKategori> AltKategoris { get; set; }
        public ICollection<ApplicationUser > User { get; set; }
        public ICollection<Urunler > Urunlers { get; set; }
    }
}