using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Models
{
    public class NoticeModel
    {
        
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public string image { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? date { get; set; }


    }
}
