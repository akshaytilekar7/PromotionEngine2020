using PromotionEngine.Helpers;
using PromotionEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class ItemServices : IItemServices
    {
        public Item GetItemBySkuId(char skuId)
        {
            return GetItems().FirstOrDefault(x => x.SkuId == skuId);
        }

        public List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item() {SkuId = Constants.A, Name = "A", Price = 50},
                new Item() {SkuId = Constants.B, Name = "B", Price = 30},
                new Item() {SkuId = Constants.C, Name = "C", Price = 20},
                new Item() {SkuId = Constants.D, Name = "D", Price = 15}
            };
        }
    }
}