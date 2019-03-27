using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class SiparisDetay
    {
        [Key]
        public int SiparisDetayID { get; set; }
        public int Adet { get; set; }
        public int Fiyat { get; set; }
        public int SiparisID { get; set; }
        public virtual Siparis Siparis { get; set; }
        public int UrunID { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}