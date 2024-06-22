using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardrobeClassLibrary.Models
{
    public class UserBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
        public ICollection<Garment>? Garments { get; set; }
    }
}
