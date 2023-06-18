using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeIdentity.DTOS
{
    public class RoleDTO
    {
        [Required(ErrorMessage = "Bạn vui lòng nhập tên nhóm quyền")]
        [Display(Name = "Nhóm quyền")]
        public string Name { get; set; }
    }
}