using System;
using System.Collections.Generic;

namespace cmsTelevision.Models
{
    public partial class Pointeuse
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public bool? Etat { get; set; }
    }
}
