using System;
using System.Collections.Generic;

namespace cmsTelevision.Models
{
    public partial class Accident
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public DateTime? DateAccident { get; set; }
        public int? NombreMort { get; set; }
        public int? NombreBlessure { get; set; }
        public string Description { get; set; }
    }
}
