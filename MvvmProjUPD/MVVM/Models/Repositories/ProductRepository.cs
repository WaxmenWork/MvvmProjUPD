using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmProjUPD.MVVM.Models.Repositories
{
    public class ProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetByAll()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<string> GetAllProviders()
        {
            return _context.Products.Select(a => a.ProductProvider).Distinct();
        }

        public IEnumerable<string> GetAllManufacturers()
        {
            return _context.Products.Select(a => a.ProductManufacturer).Distinct();
        }
    }
}
