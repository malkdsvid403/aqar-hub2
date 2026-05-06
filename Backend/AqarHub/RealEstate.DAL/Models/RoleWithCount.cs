using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DAL.Models
{
    public class RoleWithCount
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserCount { get; set; }
    }
}
