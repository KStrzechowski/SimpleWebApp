using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

#nullable disable

namespace Task1.Models
{
    public partial class Osoba
    {
        [Required]
        public int OsobaId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Imie { get; set; }

        [Required]
        [MaxLength(25)]
        public string Nazwisko { get; set; }

        [Required]
        public int SamochodId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataProd { get; set; }

        public virtual Samochod Samochod { get; set; }
    }
}
