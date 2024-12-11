using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM
{
    public class Orders
    {
        // User (TREBUIE ADAUGAT OBIECT)
        private int deliveredPizza { get; }
        private bool isDelivered { get; }
        private decimal totalPrice { get; }

        public Orders(int deliveredPizza, bool isDelivered, decimal totalPrice)
        {
            this.deliveredPizza = deliveredPizza;
            this.isDelivered = isDelivered;
            this.totalPrice = totalPrice;
        }


        public void ViewCommands()
        {
            if (isDelivered == true)
            Console.WriteLine($"Your command: {deliveredPizza} ordered with the price {totalPrice} has been delivered");
            if (isDelivered == false)
                Console.WriteLine($"Your command: {deliveredPizza} ordered with the price {totalPrice} has been not delivered");
        }







    }
}
