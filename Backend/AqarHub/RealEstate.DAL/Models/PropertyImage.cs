using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DAL.Models
{
    public class PropertyImage
    {
        public int ImageId { get; set; }
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
