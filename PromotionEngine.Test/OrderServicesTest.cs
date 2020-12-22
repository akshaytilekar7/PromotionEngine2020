using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using PromotionEngine.Helpers;
using PromotionEngine.Models;
using PromotionEngine.Services;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PromotionEngine.Test
{
    [TestClass]
    public class OrderServicesTest
    {
        private Mock<IPromotionRuleServices> _promotionRuleServicesMock;
        private Mock<IItemServices> _itemServicesMock;
        private OrderServices _orderServices;


        [TestInitialize]
        [SetUp]
        public void SetUp()
        {
            _promotionRuleServicesMock = new Mock<IPromotionRuleServices>();
            _itemServicesMock = new Mock<IItemServices>();
            _orderServices = new OrderServices(_promotionRuleServicesMock.Object, _itemServicesMock.Object);

        }

        [TestMethod]
        public void ProcessBill_WhenNoPromotionRule_Then_CalculateTotalPrice()
        {
            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.A,  2)
            };

            var item = new Item() { SkuId = Constants.A, Name = "A Name", Price = 50 };

            _promotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySkuId(Constants.A))
                .Returns<PromotionRule>(null);
            _itemServicesMock.Setup(x => x.GetItemBySkuId(Constants.A)).Returns(item);

            //Act
            var totalPrice = _orderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 100);

        }


        #region Scenario for A

        static readonly object[] ScenariosForA =
        {
            // SCENARIO 1 : 3 A : final total cost : 150 - 30 = 120
            new object[] { new List<Cart>() { new Cart(Constants.A, 3) }, 130 },

            // SCENARIO 2 : final total cost : (150 - 30) + 50 = 180
            new object[] { new List<Cart>() { new Cart(Constants.A, 4) }, 180 },
        };

        [Test, TestCaseSource(nameof(ScenariosForA))]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario_For_A(List<Cart> carts, int res)
        {
            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_A",
                SkuId = Constants.A,
                NumberOfAppearance = 3,
                LumpSumAmountToReduceFromPrice = 20,
                PercentageToReduceFromPrice = 0
            };

            var item = new Item() { SkuId = Constants.A, Name = "A Name", Price = 50 };

            _promotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySkuId(Constants.A)).Returns(promotionRule);
            _itemServicesMock.Setup(x => x.GetItemBySkuId(Constants.A)).Returns(item);

            //Act
            var totalPrice = _orderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, res);

        }

        #endregion

        #region Scenario for B

        static readonly object[] ScenariosForB =
        {
            // SCENARIO 3 : 2 B : final total cost : (30 + 30) - 15 = 45
            new object[] { new List<Cart>() { new Cart(Constants.B, 2) }, 45 },

            // SCENARIO 4 : 3 B : final total cost : (30 + 30 + 30) - 15 = 75
            new object[] { new List<Cart>() { new Cart(Constants.B, 3) }, 75 },
        };

        [Test, TestCaseSource(nameof(ScenariosForB))]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario_For_B(List<Cart> carts, int res)
        {

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_B",
                SkuId = Constants.B,
                NumberOfAppearance = 2,
                LumpSumAmountToReduceFromPrice = 15,
                PercentageToReduceFromPrice = 0
            };

            var item = new Item() { SkuId = Constants.B, Name = "B Name", Price = 30 };

            _promotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySkuId(Constants.B)).Returns(promotionRule);
            _itemServicesMock.Setup(x => x.GetItemBySkuId(Constants.B)).Returns(item);

            //Act
            var totalPrice = _orderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, res);

        }

        #endregion

        #region Scenario for C

        [Test]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario5()
        { // C + D : final total cost : (20 + 15) - 5 = 30

            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.C,1),
                new Cart(Constants.D,1)
            };

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_C",
                SkuId = Constants.C,
                NumberOfAppearance = 1,
                LumpSumAmountToReduceFromPrice = 5,
                PercentageToReduceFromPrice = 0,
                ListOfAnotherItemsToBeConsidered = new List<char>() { Constants.D }
            };

            var itemC = new Item() { SkuId = Constants.C, Name = "C Name", Price = 20 };
            var itemD = new Item() { SkuId = Constants.D, Name = "D Name", Price = 15 };

            _promotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySkuId(Constants.C)).Returns(promotionRule);
            _itemServicesMock.Setup(x => x.GetItemBySkuId(Constants.C)).Returns(itemC);
            _itemServicesMock.Setup(x => x.GetItemBySkuId(Constants.D)).Returns(itemD);


            //Act
            var totalPrice = _orderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 30);

        }


        #endregion
    }
}
