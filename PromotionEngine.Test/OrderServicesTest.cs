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
        private Mock<IPromotionRuleServices> _PromotionRuleServicesMock;
        private Mock<IItemServices> _ItemServicesMock;
        private OrderServices OrderServices;

        [TestInitialize]
        [SetUp]
        public void SetUp()
        {
            _PromotionRuleServicesMock = new Mock<IPromotionRuleServices>();
            _ItemServicesMock = new Mock<IItemServices>();
            OrderServices = new OrderServices(_PromotionRuleServicesMock.Object, _ItemServicesMock.Object);
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

            _PromotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySkuId(Constants.A))
                .Returns<PromotionRule>(null);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.A)).Returns(item);

            //Act
            var totalPrice = OrderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 100);

        }
    }
}
