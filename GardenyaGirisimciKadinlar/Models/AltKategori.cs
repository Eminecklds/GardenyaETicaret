using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class AltKategori
    {
        [Key]
        public int AltKategoriID { get; set; }
        [Required]
        [Display(Name = "Alt Kategori Adı")]
        public string AltKategoriAdi { get; set; }
        public string AltKategoriLink { get; set; }
        public int KategoriID { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual List<Urunler> Urunlers { get; set; }
    }
}