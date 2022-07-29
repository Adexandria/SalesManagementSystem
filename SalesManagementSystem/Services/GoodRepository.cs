using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public class GoodRepository : IGood
    {
        List<Good> goods = new();

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
            return goods.Where(s => s.Name.Contains(name)).ToList();
        }

        public void CreateGood(Good good)
        {
            goods.Add(good);
        }

        public Good GetGood(Guid goodId)
        {
            return goods.FirstOrDefault(s => s.GoodId == goodId);
        }

        public void UpdateGoodName(Guid goodId, string name)
        {
            Good good = GetGood(goodId);
            if(good is not null)
            {
                good.Name = name;
            }  
        }

        public void UpdateGoodPrice(Guid goodId, float price)
        {
            Good good = GetGood(goodId);
            if (good is not null)
            {
                good.Price = price; 
            }
        }

        public void UpdateGoodQuantity(Guid goodId, int quantity)
        {
            Good good = GetGood(goodId);
            if (good is not null)
            {
               good.Quantity = quantity;
            } 
        }
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
