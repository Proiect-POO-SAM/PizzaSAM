using ProiectPOOSAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM
{
    public class Orders
    {
        public USER USER;
        private int deliveredPizza;
        public enum delivery { toHome , toRestaurant };
        public delivery deliveryMethod;
        private decimal totalPrice;

        public Orders(int deliveredPizza, delivery deliveryMethod, decimal totalPrice, USER USER)
        {
            this.deliveredPizza = deliveredPizza;
            this.deliveryMethod = deliveryMethod;
            this.totalPrice = totalPrice;
            this.USER = USER;
        }

        public int getDeliveredPizza()
        {
            return deliveredPizza;
        }
        public delivery getDeliveryMethod()
        {
            return deliveryMethod;
        }
        public decimal getTotalPrice()
        {
            return totalPrice;
        }



        public void ViewCommands()
        {
            if(deliveryMethod == delivery.toHome)
            {
                Console.WriteLine($"Pizza {deliveredPizza} will be delivered to your home and it will cost {totalPrice}");
            }
            if(deliveryMethod == delivery.toRestaurant)
            {
                Console.WriteLine($"Pizza {deliveredPizza} will await you at the restaurant and it will cost {totalPrice}");
            }
        }
        
        public void ViewMyCommands(string USERNAME)
        {
            if (USERNAME == USER.GetUsername())
            {
                // cod ig
            }
        }







    }
}
