using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Data.Entities
{
    public class PetEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char? Sex { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Vaccines { get; set; }
        public bool? Sterilization { get; set; }
        public string Size { get; set; }
        public DateTime? age { get; set; }
        public bool? IsAdopted { get; set; }
        public int? previous { get; set; } = null;
        public int? next { get; set; } = null;


    }
}
