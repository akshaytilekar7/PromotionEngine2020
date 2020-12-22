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

            if (promoRule.LumpSumAmountToReduceFromPrice > 0)
            {
                return CalculatePrice(carts, cart, promoRule);
            }
            return 0;
        }

        private int CalculatePrice(List<Cart> carts, Cart cart, PromotionRule promoRule)
        {
            var price = 0;
            var item = _itemServices.GetItemBySkuId(cart.SkuId);

            // process until item count are applicable for promo rules
            while (promoRule.NumberOfAppearance <= cart.CountOfRemainingItemsForPromo && cart.CountOfRemainingItemsForPromo != 0)
            {
                price += GetProductPrice(item, promoRule.NumberOfAppearance) - promoRule.LumpSumAmountToReduceFromPrice;
                cart.CountOfRemainingItemsForPromo = cart.CountOfRemainingItemsForPromo - promoRule.NumberOfAppearance;
            }

            if (cart.CountOfRemainingItemsForPromo > 0) // remaining item after promo rule, will process with normal calulation
            {
                price += GetProductPrice(item, cart.CountOfRemainingItemsForPromo);
                cart.CountOfRemainingItemsForPromo = 0; // all item are processed
            }
            return price;
        }

        private int GetProductPrice(Item item, int cnt)
        {
            return item.Price * cnt;
        }

    }
}
