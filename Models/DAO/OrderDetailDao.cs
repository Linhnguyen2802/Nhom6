using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using PagedList;

namespace Models.DAO
{
    public class OrderDetailDao
    {
        OnlineShopDbContext db = null;
         public OrderDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
        public IEnumerable<OrderDetail> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            
            return model.OrderByDescending(x => x.OrderID).ToPagedList(page, pageSize);
        }
    }
}
