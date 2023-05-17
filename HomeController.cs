using ProgrammingConcepts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammingConcepts.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            //Order order = null;
            //SellInStore sellInStore;
            //decimal price;

            //// Create a new order with no discount
            //order = new Order() { Total = 1200 };
            //sellInStore = new SellInStore(order);
            //price = sellInStore.CalculateTotal();

            //return View();

            //------------------------------------------------------------------------------------------------

            //Order order = null;
            //DiscountOrder discountOrder = null;
            //SellInStore sellInStore;
            //decimal price;

            //// Create a new order with no discount
            //order = new Order() { Total = 1200 };
            //sellInStore = new SellInStore(order);
            //price = sellInStore.SellOrder();

            //// Create a new order with a 10% discount
            //discountOrder = new DiscountOrder(percentOff: 10) { Total = 1200 };
            //sellInStore = new SellInStore(discountOrder);
            //price = sellInStore.SellOrderWithDiscoun();

            //return View();

            //---------------------------------------------------------------------------------------------------


            IDiscountStrategy order;
            SellInStore sellInStore;
            decimal price;

            // Create a new order with no discount
            order = new NoDiscountStrategy() { Total = 1200 };
            sellInStore = new SellInStore(order);
            price = sellInStore.CalculateTotal();

            // Create a new order with a 10% discount
            order = new PercentOffDiscountStrategy(percentOff: 10) { Total = 1200 };
            sellInStore = new SellInStore(order);
            price = sellInStore.CalculateTotal();

            // Create a new order with a $60 discount
            order = new FlatDiscountStrategy(flatAmount: 60) { Total = 1200 };
            sellInStore = new SellInStore(order);
            price = sellInStore.CalculateTotal();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}