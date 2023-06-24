using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PracticeIdentity.DTOS
{
    public class UserDTO
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn vui lòng nhập số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Bạn vui lòng chọn nhóm quyền")]
        public List<string> Roles { get; set; }
    }
}