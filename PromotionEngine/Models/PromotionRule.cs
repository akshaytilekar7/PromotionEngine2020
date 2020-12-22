using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class PromotionRule
    {
        public PromotionRule()
        {
            ListOfAnotherItemsToBeConsidered = new List<char>();
        }

        public char SkuId { get; set; }

        public string RuleName { get; set; }

        public int NumberOfAppearance { get; set; }

        public int PercentageToReduceFromPrice { get; set; }

        public int LumpSumAmountToReduceFromPrice { get; set; }

        public List<char> ListOfAnotherItemsToBeConsidered { get; set; }
    }
}