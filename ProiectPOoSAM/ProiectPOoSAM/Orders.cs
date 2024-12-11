using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM
{
    public class Orders
    {
        //public User User (TREBUIE ADAUGAT OBIECT)
        private int deliveredPizza { get; }
        public enum delivery { toHome , toRestaurant };
        public delivery deliveryMethod;
        private decimal totalPrice { get; }

        public Orders(int deliveredPizza, delivery deliveryMethod, decimal totalPrice)
        {
            this.deliveredPizza = deliveredPizza;
            this.deliveryMethod = deliveryMethod;
            this.totalPrice = totalPrice;
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







    }
}
