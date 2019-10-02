using System;
using System.Collections.Generic;

namespace cmsTelevision.Models
{
    public partial class Demarrage
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? Nombre { get; set; }

        public string Type { get; set; }
    }
}
