using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public interface IGood
    {
        List<Good> GetGoods { get; }
        List<Good> GetGoodByName(string name);
        Good GetGood(Guid goodId);
        void CreateGood(Good good);
        void UpdateGoodName(Guid goodId, string name);
        void UpdateGoodPrice(Guid goodId, float price);
        void UpdateGoodQuantity(Guid goodId, int quantity);
        void DeleteGood(Guid goodId);
    }
}
