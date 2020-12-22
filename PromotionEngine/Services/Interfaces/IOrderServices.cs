using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Services.Interfaces
{
    public interface IOrderServices
    {
        int ProcessBill(List<Cart> carts);
    }
}
