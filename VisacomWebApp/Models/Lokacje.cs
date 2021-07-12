using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisacomWepApp.Models
{
    public class Lokacje
    {
        public int Id { get; set; }
        public string nazwa { get; set; }
        public Postacie gra { get; set; }
    }
}
