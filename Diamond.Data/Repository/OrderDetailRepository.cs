using Diamond.Data.Base;
using Diamond.Data.Models;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Orderdetail> SearchOrderdetail(
        string orderDetailId = null,
        string orderId = null,
        string mainDiamondId = null,
        string shellId = null,
        string subDiamondId = null,
        decimal lineTotal = 0,
        int quantity = 0,
        decimal unitWeight = 0,
        decimal unitPrice = 0,
        decimal discountPercentage = 0,
        string note = null)
        {
            var query = _context.Orderdetails.AsQueryable();

            if (!string.IsNullOrWhiteSpace(orderDetailId))
                query = query.Where(od => od.OrderDetailId.Contains(orderDetailId));

            if (!string.IsNullOrWhiteSpace(orderId))
                query = query.Where(od => od.OrderId.Contains(orderId));

            if (!string.IsNullOrWhiteSpace(mainDiamondId))
                query = query.Where(od => od.MainDiamondId.Contains(mainDiamondId));

            if (!string.IsNullOrWhiteSpace(shellId))
                query = query.Where(od => od.ShellId.Contains(shellId));

            if (!string.IsNullOrWhiteSpace(subDiamondId))
                query = query.Where(od => od.SubDiamondId.Contains(subDiamondId));

            if (lineTotal != 0)
                query = query.Where(od => od.LineTotal == lineTotal);

            if (quantity != 0)
                query = query.Where(od => od.Quantity == quantity);

            if (unitWeight != 0)
                query = query.Where(od => od.UnitWeight == unitWeight);

            if (unitPrice != 0)
                query = query.Where(od => od.UnitPrice == unitPrice);

            if (discountPercentage != 0)
                query = query.Where(od => od.DiscountPercentage == discountPercentage);


            if (!string.IsNullOrWhiteSpace(note))
                query = query.Where(od => od.Note.Contains(note));

            return query.ToList();
        }
        public async Task<List<Orderdetail>> SearchByFieldsAsync(
     string orderDetailId = null,
     string orderId = null,
     string mainDiamondId = null,
     string shellId = null,
     string subDiamondId = null,
     decimal? lineTotal = null,
     int? quantity = null,
     decimal? unitWeight = null,
     decimal? unitPrice = null,
     decimal? discountPercentage = null,
     string note = null
 )
        {
            return await SearchByFieldsAsync(new Orderdetail()
            {
                OrderDetailId = orderDetailId,
                OrderId = orderId,
                MainDiamondId = mainDiamondId,
                ShellId = shellId,
                SubDiamondId = subDiamondId,
                LineTotal = lineTotal ?? 0,
                Quantity = quantity ?? 0,
                UnitWeight = unitWeight ?? 0,
                UnitPrice = unitPrice ?? 0,
                DiscountPercentage = discountPercentage ?? 0,
                Note = note
            });
        }

        public async Task<List<Orderdetail>> SearchByFieldsAsync(Orderdetail orderdetail)
        {
            var query = _context.Set<Orderdetail>().AsQueryable();
            if (!string.IsNullOrWhiteSpace(orderdetail.OrderDetailId))
                query = query.Where(od => od.OrderDetailId.Contains(orderdetail.OrderDetailId));

            if (!string.IsNullOrWhiteSpace(orderdetail.OrderId))
                query = query.Where(od => od.OrderId.Contains(orderdetail.OrderId));

            if (!string.IsNullOrWhiteSpace(orderdetail.MainDiamondId))
                query = query.Where(od => od.MainDiamondId.Contains(orderdetail.MainDiamondId));

            if (!string.IsNullOrWhiteSpace(orderdetail.ShellId))
                query = query.Where(od => od.ShellId.Contains(orderdetail.ShellId));

            if (!string.IsNullOrWhiteSpace(orderdetail.SubDiamondId))
                query = query.Where(od => od.SubDiamondId.Contains(orderdetail.SubDiamondId));

            if (orderdetail.LineTotal != 0)
                query = query.Where(od => od.LineTotal == orderdetail.LineTotal);

            if (orderdetail.Quantity != 0)
                query = query.Where(od => od.Quantity == orderdetail.Quantity);

            if (orderdetail.UnitWeight != 0)
                query = query.Where(od => od.UnitWeight == orderdetail.UnitWeight);

            if (orderdetail.UnitPrice != 0)
                query = query.Where(od => od.UnitPrice == orderdetail.UnitPrice);

            if (orderdetail.DiscountPercentage != 0)
                query = query.Where(od => od.DiscountPercentage == orderdetail.DiscountPercentage);

            if (!string.IsNullOrWhiteSpace(orderdetail.Note))
                query = query.Where(od => od.Note.Contains(orderdetail.Note));

            return await query.ToListAsync();
        }
    }
}
