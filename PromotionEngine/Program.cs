using PromotionEngine.Helpers;
using PromotionEngine.Models;
using PromotionEngine.Services;
using PromotionEngine.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            IPromotionRuleServices PromotionRules = new PromotionServices();
            IItemServices itemServices = new ItemServices();
            IOrderServices orderServices = new OrderServices(PromotionRules, itemServices);


            #region 3_A_FOR_130
            List<Cart> ListOfCartItemDetails1 = new List<Cart>()
            {
                new Cart(Constants.A, 3)
            };

            var total = orderServices.ProcessBill(ListOfCartItemDetails1);
            Console.WriteLine(total);
            #endregion

            #region 2_B_For_45
            List<Cart> ListOfCartItemDetails2 = new List<Cart>()
            {
                new Cart(Constants.B, 2)
            };

            total = orderServices.ProcessBill(ListOfCartItemDetails2);
            Console.WriteLine(total);
            #endregion

            #region C_AND_D_FOR_30
            List<Cart> ListOfCartItemDetails3 = new List<Cart>()
            {
                new Cart(Constants.C, 1),
                new Cart(Constants.D, 1)
            };

            total = orderServices.ProcessBill(ListOfCartItemDetails3);
            Console.WriteLine(total);
            #endregion
        }
    }
}
