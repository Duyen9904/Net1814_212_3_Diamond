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
        //TO-DO CODE HERE/////////////////
    }
}
