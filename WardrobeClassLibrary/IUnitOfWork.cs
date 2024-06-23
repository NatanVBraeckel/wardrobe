using Wardrobe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wardrobe.DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<Garment> GarmentRepository { get; }
        GenericRepository<GarmentType> GarmentTypeRepository { get; }
        GenericRepository<GlobalBrand> GlobalBrandRepository { get; }
        GenericRepository<User> UserRepository { get; }
        GenericRepository<UserBrand> UserBrandRepository { get; }

        Task SaveAsync();
    }
}
