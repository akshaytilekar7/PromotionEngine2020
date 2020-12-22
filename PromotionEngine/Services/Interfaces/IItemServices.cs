using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Services.Interfaces
{
    public interface IItemServices
    {
        List<Item> GetItems();
        Item GetItemBySkuId(char skuId);
    }
}
