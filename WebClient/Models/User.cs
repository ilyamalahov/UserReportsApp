using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string FullName => string.Join(" ", LastName, FirstName, MiddleName);

        public User Clone()
        {
            return (User)MemberwiseClone();
        }
    }
}
