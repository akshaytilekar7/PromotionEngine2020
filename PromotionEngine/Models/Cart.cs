namespace PromotionEngine.Models
{
    public class Cart
    {
        public Cart(char skuId, int totalCount)
        {
            SkuId = skuId;
            TotalCount = totalCount;
        }

        public char SkuId { get; set; }
        public int TotalCount { get; set; }
    }
}
