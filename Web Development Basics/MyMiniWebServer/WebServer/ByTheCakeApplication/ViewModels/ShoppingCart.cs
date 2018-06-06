namespace WebServer.ByTheCakeApplication.ViewModels
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.ProductIds = new List<int>();
        }

        public const string SessionKey = "%^Current_Shopping_Cart^%";

        public List<int> ProductIds { get; private set; } 
    }
}