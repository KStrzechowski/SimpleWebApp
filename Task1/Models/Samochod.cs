using System;
using System.Collections.Generic;

#nullable disable

namespace Task1.Models
{
    public partial class Samochod
    {
        public Samochod()
        {
            Osoby = new HashSet<Osoba>();
        }

        public int SamochodId { get; set; }
        public decimal Pojemnosc { get; set; }
        public decimal Cena { get; set; }

        public virtual ICollection<Osoba> Osoby { get; set; }
    }
}
