using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class Cart
    {
        [Key]
        public int RecordID { get; set; }
        public string CartID { get; set; }
        public int UrunID { get; set; }
        public int Count { get; set; }
        public int SubTotal { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}