using System;
using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        public readonly SalesWebMvcContext _context;

        public SellerService()
        {

        }

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return  await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {

            //obj.Department = _context.Department.First();

            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            //This way was returning just the seller on details view.
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);

            //this way is returning the Department and the Id Seller
            //I had to include: using Microsoft.EntityFrameworkCore and use the include of Entity Framework
            //this is called Eager loading. its load others objects linked with main object.
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);

                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                throw new IntegrityException("Cant delete Seller because She/He has sales");
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny =  await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not Found");

            }

            try
            {
                _context.Update(seller);
               await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }


        }
    }
}
