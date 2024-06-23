using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wardrobe.DAL.Models
{
    public class Garment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImgPath { get; set; }

        public int? GlobalBrandId { get; set; }
        public int? UserBrandId { get; set; }
        public int GarmentTypeId { get; set; }
        public int UserId { get; set; }

        public GlobalBrand? GlobalBrand { get; set; }
        public UserBrand? UserBrand { get; set; }
        public GarmentType GarmentType { get; set; }
        public User User { get; set; }
    }
}
