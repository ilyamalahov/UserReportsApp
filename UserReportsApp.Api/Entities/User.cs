using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserReportsApp.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
