using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wardrobe.DAL.Models
{
    public class GarmentType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Garment>? Garments { get; set; }
    }
}
