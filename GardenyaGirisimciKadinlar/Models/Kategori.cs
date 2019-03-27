using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }
        [Required]
        public string KategoriAdi { get; set; }
        public string KategoriLink { get; set; }
        public virtual List<AltKategori> AltKategoris { get; set; }
    }
}