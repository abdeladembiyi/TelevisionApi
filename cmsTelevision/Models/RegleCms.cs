using System;
using System.Collections.Generic;

namespace cmsTelevision.Models
{
    public partial class RegleCms
    {
        public int Id { get; set; }
        public int? NumOrdre { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool? Show { get; set; }
    }
}
