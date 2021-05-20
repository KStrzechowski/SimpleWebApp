using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Task1.Models
{
    public partial class Samochod
    {
        public Samochod()
        {
            Osoby = new HashSet<Osoba>();
        }

        [Required]
        public int SamochodId { get; set; }
        [Required]
        public decimal Pojemnosc { get; set; }
        [Required]
        public decimal Cena { get; set; }

        public virtual ICollection<Osoba> Osoby { get; set; }
    }
}
