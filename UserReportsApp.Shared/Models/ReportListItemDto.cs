using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserReportsApp.Shared.Models
{
    public class ReportDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public int NumberOfHours { get; set; }
        [Required]
        public string Remark { get; set; }
        public int UserId { get; set; }
    }
}
