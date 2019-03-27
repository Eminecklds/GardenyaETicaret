using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class Siparis
    {
        [Key]
        public int SiparisID { get; set; }
        public DateTime Tarih { get; set; }
        public bool Durum { get; set; }
        public virtual List<SiparisDetay> SiparisDetays { get; set; }
        
        public int KullaniciID { get; set; }
        public decimal Total { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}