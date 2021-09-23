using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Data.Entities
{
    public class NoticeEntity
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public DateTime? date { get; set; }
    }
}
