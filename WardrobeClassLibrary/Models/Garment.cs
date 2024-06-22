using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardrobeClassLibrary.Models
{
    public class Garment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }

        public GlobalBrand? GlobalBrand { get; set; }
        public UserBrand? UserBrand { get; set; }
        public GarmentType Type { get; set; }
        public User User { get; set; }
    }
}
