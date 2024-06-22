using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardrobeClassLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AuthId { get; set; }

        public ICollection<Garment>? Garments { get; set; }
        public ICollection<UserBrand>? UserBrands { get; set; }
    }
}
