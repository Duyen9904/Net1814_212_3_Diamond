using Diamond.Data.Base;
using Diamond.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Data.Repository
{
    public class OrderDetailRepository : GenericRepository<Orderdetail>
    {
        public OrderDetailRepository()
        {  
        }
        public OrderDetailRepository(Net1814_212_3_DianContext context) => _context = context;

        public IEnumerable<Orderdetail> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _context.Orderdetails.ToList();
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            return _context.Orderdetails.Where(od =>
                od.OrderDetailId.ToLower().Contains(lowerCaseSearchTerm) ||
                od.OrderId.ToLower().Contains(lowerCaseSearchTerm) ||
                od.LineTotal.ToString().ToLower().Contains(lowerCaseSearchTerm) ||
                (od.MainDiamondId != null && od.MainDiamondId.ToLower().Contains(lowerCaseSearchTerm)) ||
                (od.ShellId != null && od.ShellId.ToLower().Contains(lowerCaseSearchTerm)) ||
                (od.SubDiamondId != null && od.SubDiamondId.ToLower().Contains(lowerCaseSearchTerm)) ||
                od.Quantity.ToString().ToLower().Contains(lowerCaseSearchTerm) ||
                od.UnitWeight.ToString().ToLower().Contains(lowerCaseSearchTerm) ||
                od.UnitPrice.ToString().ToLower().Contains(lowerCaseSearchTerm) ||
                od.DiscountPercentage.ToString().ToLower().Contains(lowerCaseSearchTerm) ||
                (od.Note != null && od.Note.ToLower().Contains(lowerCaseSearchTerm))
            ).ToList();
        }
    }
}
