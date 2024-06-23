using Wardrobe.DAL.Data;
using Wardrobe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wardrobe.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private WardrobeContext _context;
        private GenericRepository<Garment> garmentRepository;
        private GenericRepository<GarmentType> garmentTypeRepository;
        private GenericRepository<GlobalBrand> globalBrandRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<UserBrand> userBrandRepository;

        public UnitOfWork(WardrobeContext context)
        {
            _context = context;
        }

        public GenericRepository<Garment> GarmentRepository
        {
            get
            {
                if (garmentRepository == null)
                {
                    garmentRepository = new GenericRepository<Garment>(_context);
                }
                return garmentRepository;
            }
        }

        public GenericRepository<GarmentType> GarmentTypeRepository
        {
            get
            {
                if (garmentTypeRepository == null)
                {
                    garmentTypeRepository = new GenericRepository<GarmentType>(_context);
                }
                return garmentTypeRepository;
            }
        }

        public GenericRepository<GlobalBrand> GlobalBrandRepository
        {
            get
            {
                if (globalBrandRepository == null)
                {
                    globalBrandRepository = new GenericRepository<GlobalBrand>(_context);
                }
                return globalBrandRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new GenericRepository<User>(_context);
                }
                return userRepository;
            }
        }

        public GenericRepository<UserBrand> UserBrandRepository
        {
            get
            {
                if (userBrandRepository == null)
                {
                    userBrandRepository = new GenericRepository<UserBrand>(_context);
                }
                return userBrandRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
