using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class KategoriAltkategoriViewModel
    {
        public int KategoriID { get; set; }
        public int AltKategoriID { get; set; }
        public string KategoriAdi { get; set; }
        public string AltKategoriAdi { get; set; }
        public string AltKategoriLink { get; set; }
        public ICollection<Kategori> Kategoris { get; set; }
        public ICollection<AltKategori> AltKategoris { get; set; }

    }
}