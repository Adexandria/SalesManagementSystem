using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public class GoodRepository : IGood
    {
        public static readonly List<Good> goods = new();

        
        //Get all Goods in the database
        public List<Good> GetGoods
        {
            get
            {
                return goods;
            }
        }

        //Search Goods by Name
        public List<Good> GetGoodByName(string name)
        {
            List<Good> searchGoods = goods.Where(s => s.Name.Contains(name)).ToList();
            return searchGoods;
        }

        //Create new good
        public void CreateGood(Good good)
        {
            good.GoodId = Guid.NewGuid();
            goods.Add(good);
        }

        //get good by good Id
        public Good GetGood(Guid goodId)
        {
            return goods.FirstOrDefault(s => s.GoodId == goodId);
        }

        //Update good name
        public void UpdateGoodName(Guid goodId, string name)
        {
            Good good = GetGood(goodId);
            if(good is not null)
            {
                good.Name = name;
            }  
        }

        //Update good price
        public void UpdateGoodPrice(Guid goodId, float price)
        {
            Good good = GetGood(goodId);
            if (good is not null)
            {
                good.Price = price; 
            }
        }

        //Update good quantity
        public void UpdateGoodQuantity(Guid goodId, int quantity)
        {
            Good good = GetGood(goodId);
            if (good is not null)
            {
               good.Quantity = quantity;
            } 
        }

        //Update Order good quantity
        public void UpdateOrderGoodQuantity(Guid goodId, int quantity)
        {
            Good good = GetGood(goodId);
            if (good is not null)
            {
                good.Quantity -= quantity;
            }
        }

        //Delete Good
        public void DeleteGood(Guid goodId)
        {
            Good good = GetGood(goodId);
            if(good is not null)
            {
               goods.Remove(good);
            }
            
        }

       
    }
}
