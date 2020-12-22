using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Services.Interfaces
{
    public interface IPromotionRuleServices
    {
        List<PromotionRule> GetPromotionRules();

    }
}
