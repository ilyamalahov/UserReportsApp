using System;
using System.Collections.Generic;
using System.Text;

namespace UserReportsApp.Shared.Models
{
    public class UserListItemDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
    }
}
