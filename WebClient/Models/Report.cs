using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NumberOfHours { get; set; }
        public int UserId { get; set; }

        public Report Clone()
        {
            return (Report)MemberwiseClone();
        }
    }
}
