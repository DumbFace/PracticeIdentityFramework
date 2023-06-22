using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Roles
{
    public class RoleModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn vui lòng nhập tên nhóm quyền")]
        [Display(Name = "Nhóm quyền")]
        public string Name { get; set; }
    }
}