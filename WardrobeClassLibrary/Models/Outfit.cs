using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wardrobe.DAL.Models
{
    public class Outfit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public ICollection<Garment> Garments { get; set; }
        public User User { get; set; }
    }
}
