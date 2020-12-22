namespace PromotionEngine.Models
{
    public class Cart
    {
        public Cart(char skuId, int totalCount)
        {
            SkuId = skuId;
            CountOfRemainingItemsForPromo = TotalCount = totalCount;
        }

        public char SkuId { get; set; }
        public int TotalCount { get; set; }

        public int CountOfRemainingItemsForPromo { get; set; }

    }
}
