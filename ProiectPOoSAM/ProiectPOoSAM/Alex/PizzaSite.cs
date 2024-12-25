using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM.Alex
{
    public class PizzaSite
    {
        private string pizzaSiteName;
        private string pizzaSiteLocation;
        private List<Orders> orders;


        public PizzaSite(string pizzaSiteName, string pizzaSiteLocation)
        {
            this.pizzaSiteName = pizzaSiteName;
            this.pizzaSiteLocation = pizzaSiteLocation;
        }

        // The orders needs to be stocked in a file, Sebi your turn there :D 
    }
}
