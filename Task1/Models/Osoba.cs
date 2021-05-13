using System;
using System.Collections.Generic;

#nullable disable

namespace Task1.Models
{
    public partial class Osoba
    {
        public int OsobaId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int SamochodId { get; set; }
        public DateTime? DataProd { get; set; }

        public virtual Samochod Samochod { get; set; }
    }
}
