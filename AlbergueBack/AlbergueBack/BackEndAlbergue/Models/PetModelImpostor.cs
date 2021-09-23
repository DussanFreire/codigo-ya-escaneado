using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;


namespace BackEndAlbergue.Models
{
    public class PetModelImpostor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public char? Sex { get; set; }
        [StringLength(55, MinimumLength = 3)]
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Vaccines { get; set; }
        public bool? Sterilization { get; set; }
        public string Size { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? age { get; set; }
        public bool? IsAdopted { get; set; }
        public int? previous { get; set; } = null;
        public int? next { get; set; } = null;
    }
}
