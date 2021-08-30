using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserReportsApp.Api.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NumberOfHours { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
