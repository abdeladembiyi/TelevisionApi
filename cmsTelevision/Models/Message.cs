using System;
using System.Collections.Generic;

namespace cmsTelevision.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public DateTime? DateMessage { get; set; }
        public string Titre { get; set; }
        public bool? Show { get; set; }
    }
}
