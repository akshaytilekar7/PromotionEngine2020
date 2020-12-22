using PromotionEngine.Helpers;
using PromotionEngine.Models;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class PromotionServices : IPromotionRuleServices
    {
        public PromotionRule GetPromotionRulesBySkuId(char skuId)
        {
            return GetPromotionRules().FirstOrDefault(x => x.SkuId == skuId);
        }

        public List<PromotionRule> GetPromotionRules()
        {
            List<PromotionRule> promotionRules = new List<PromotionRule>()
            {
                new PromotionRule
                {
                    RuleName = "3_A_For_130 ",
                    SkuId = Constants.A,
                    NumberOfAppearance = 3 ,
                    LumpSumAmountToReduceFromPrice = 20,
                    PercentageToReduceFromPrice = 0
                },
                new PromotionRule
                {
                    RuleName = "2_B_For_45",
                    SkuId = Constants.B,
                    NumberOfAppearance = 2 ,
                    LumpSumAmountToReduceFromPrice = 15,
                    PercentageToReduceFromPrice = 0
                },
                new PromotionRule
                {
                    RuleName = "C_&_D_For_30",
                    SkuId = Constants.C,
                    NumberOfAppearance = 1,
                    LumpSumAmountToReduceFromPrice = 5,
                    PercentageToReduceFromPrice = 0,
                    ListOfAnotherItemsToBeConsidered = new List<char>() { Constants.D }
                },
            };
            return promotionRules;
        }
    }
}
