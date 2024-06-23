using Wardrobe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wardrobe.DAL.Data;

namespace Shop.DAL.Data
{
    public class DBInitializer
    {
        public static void Initialize(WardrobeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any garment types.
            if (context.GarmentTypes.Any())
            {
                return;   // DB has been seeded
            }

            //Add garments
            context.AddRange(
                new GarmentType { Id = 1, Name = "Pants" },
                new GarmentType { Id = 2, Name = "T-shirt" },
                new GarmentType { Id = 3, Name = "Sweater" }
                );

            //Add users
            User userNatan = new User()
            {
                Username = "Natan",
            };

            User userNona = new User()
            {
                Username = "Nona",
            };
            context.Add(userNatan);
            context.Add(userNona);

            //Add global brands
            GlobalBrand nike = new GlobalBrand()
            {
                Name = "Nike" 
            };
            GlobalBrand adidas = new GlobalBrand()
            {
                Name = "Adidas"
            };
            GlobalBrand balenciaga = new GlobalBrand()
            {
                Name = "Balenciaga"
            };

            context.Add(nike);
            context.Add(adidas);
            context.Add(balenciaga);

            //Add garments
            Garment g = new Garment()
            {
                Name = "Nike shirt",
                GlobalBrandId = 1,
                GarmentTypeId = 2,
                UserId = 1,
            };

            Garment g2 = new Garment()
            {
                Name = "Adidas pants",
                GlobalBrandId = 2,
                GarmentTypeId = 1,
                UserId = 1,
            };

            Garment g3 = new Garment()
            {
                Name = "Gucci sweater",
                GlobalBrandId = 3,
                GarmentTypeId = 3,
                UserId = 2,
            };

            context.Add(g);
            context.Add(g2);
            context.Add(g3);


            context.SaveChanges();
        }
    }
}
