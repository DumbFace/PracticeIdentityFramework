using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeIdentity.DTOS;

namespace PracticeIdentity.Models
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public List<string> ListRole { get; set; } 
    }
}