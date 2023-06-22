using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeIdentity.Models
{
    public class PermissionModel
    {
        public string GroupPermission { get; set; }
        public string View { get; set; }
        public string Create { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }
    }
}