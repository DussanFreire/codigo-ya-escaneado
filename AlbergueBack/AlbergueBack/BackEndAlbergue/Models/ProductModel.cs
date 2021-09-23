using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public char? Sex { get; set; }
        public string Photo { get; set; }
        public string Category { get; set; }
        public string SizeOfPet { get; set; }
        public string Price { get; set; }
        public int? Stock { get; set; }
    }
}
