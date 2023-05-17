using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingConcepts.Repository
{
    ////-----------------------------------------------------OCP------------------------------------------------------------
    //// Start with Order Class to calculate total of order
    //public class Order 
    //{
    //    public decimal Total { get; set; }

    //    public virtual decimal CalculateTotal()
    //    {
    //        return Total;
    //    }
    //}

    //public class SellInStore : Order
    //{
    //    private Order order;

    //    public SellInStore(Order order)
    //    {
    //        this.order = order;
    //    }

    //    public override decimal CalculateTotal()
    //    {
    //        var price = order.CalculateTotal();
    //        return price;
    //    }
    //}


    ////----------------------------------------------------------------------------------------------------------------
    //// add SellOrderWithDiscoun method to Order Class 
    //// In this way, we violate the principle of OCP
    //public class Order
    //{
    //    public decimal Total { get; set; }

    //    public virtual decimal CalculateTotal()
    //    {
    //        return Total;
    //    }

    //    public decimal SellOrderWithDiscoun(decimal percentOff)
    //    {
    //        return Total - (Total * percentOff / 100);
    //    }
    //}

    //public class SellInStore : Order
    //{
    //    private Order order;

    //    public SellInStore(Order order)
    //    {
    //        this.order = order;
    //    }

    //    public override decimal CalculateTotal()
    //    {
    //        var price = order.CalculateTotal();
    //        return price;
    //    }
    //}



    //-----------------------------------------------------OCP------------------------------------------------------------
    //-------------------------------------------------Approach one ----------------------------------------------------
    //Approach one to extend Order class
    //The problem is when we want to add Calculate Total With PercentOff to SellInStore Class we have to
    //Add separate constructor with DiscountOrder Class OR  
    // change contractor parameter from "Order" to "DiscountOrder"
    ////----------------------------------------------------------------------------------
    //public class Order
    //{
    //    public decimal Total { get; set; }

    //    public virtual decimal CalculateTotal()
    //    {
    //        return Total;
    //    }
    //}

    //public class DiscountOrder : Order
    //{
    //    private readonly decimal percentOff;

    //    public DiscountOrder(decimal percentOff)
    //    {
    //        this.percentOff = percentOff;
    //    }

    //    public override decimal CalculateTotal()
    //    {
    //        return Total - (Total * percentOff / 100);
    //    }
    //}


    //public class SellInStore
    //{
    //    private Order order;
    //    private DiscountOrder discountOrder;


    //    public SellInStore(Order order)
    //    {
    //        this.order = order;
    //    }
    //    public SellInStore(DiscountOrder discountOrder)
    //    {
    //        this.discountOrder = discountOrder;
    //    }


    //    public decimal SellOrder()
    //    {
    //        var price = order.CalculateTotal();
    //        return price;
    //    }

    //    public decimal SellOrderWithDiscoun()
    //    {
    //        var price = discountOrder.CalculateTotal();
    //        return price;
    //    }
    //}


    //-------------------------------------------------Approach Two ----------------------------------------------------
    //Approach Two to extend Order class
    //
    ////----------------------------------------------------------------------------------

    public interface IOrder
    {
        decimal CalculateTotal();
    }

    public interface IDiscountStrategy
    {
        decimal ApplyDiscount();

    }

    public class Order : IOrder
    {
        public decimal Total { get; set; }

        public virtual decimal CalculateTotal()
        {
            // implementation for CalculateTotal() method
            return Total;
        }
    }

    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal Total { get; set; }
        public decimal ApplyDiscount()
        {
            return Total;
        }
    }

    public class PercentOffDiscountStrategy : IDiscountStrategy
    {
        public decimal Total { get; set; }
        private readonly decimal percentOff;

        public PercentOffDiscountStrategy(decimal percentOff)
        {
            this.percentOff = percentOff;
        }

        public decimal ApplyDiscount()
        {
            return Total - (Total * percentOff / 100);
        }
    }

    public class FlatDiscountStrategy : IDiscountStrategy
    {
        public decimal Total { get; set; }

        private readonly decimal flatAmount;

        public FlatDiscountStrategy(decimal flatAmount)
        {
            this.flatAmount = flatAmount;
        }

        public decimal ApplyDiscount()
        {
            return Total - flatAmount;
        }
    }

    public class SellInStore : Order
    {
        public int PercentOff { get; internal set; }

        private readonly IDiscountStrategy discountStrategy;

        public SellInStore(IDiscountStrategy discountStrategy)
        {
            this.discountStrategy = discountStrategy;
        }

        public override decimal CalculateTotal()
        {
            decimal totalWithDiscount = discountStrategy.ApplyDiscount();
            return totalWithDiscount;
        }
    }

}

