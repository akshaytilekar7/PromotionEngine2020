using PromotionEngine.Models;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;

namespace PromotionEngine.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IPromotionRuleServices _promotionRuleServices;
        private readonly IItemServices _itemServices;
        public OrderServices(IPromotionRuleServices promotionRuleServices, IItemServices itemServices)
        {
            this._promotionRuleServices = promotionRuleServices;
            this._itemServices = itemServices;
        }

        public int ProcessBill(List<Cart> carts)
        {
            var total = 0;
            foreach (var cart in carts)
            {
                var promoRule = _promotionRuleServices.GetPromotionRulesBySkuId(cart.SkuId);
                total += ApplyPromotionRule(carts, cart, promoRule);
            }
            return total;
        }

        private int ApplyPromotionRule(List<Cart> carts, Cart cart, PromotionRule promoRule)
        {
            var item = _itemServices.GetItemBySkuId(cart.SkuId);

            if (promoRule == null)
                return cart.TotalCount * item.Price;

            return 0;
        }


    }
}
