using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService()
        {

        }

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            //The method ToList is assyncron because of the needed to be change
            //return _context.Department.OrderBy(dep => dep.Name).ToList();

            return await _context.Department.OrderBy(dep => dep.Name).ToListAsync();
        }
    }
}
