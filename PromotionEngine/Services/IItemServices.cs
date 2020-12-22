using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Services
{
    public interface IItemServices
    {
        List<Item> GetItems();
        Item GetItemBySkuId(char skuId);
    }
}
