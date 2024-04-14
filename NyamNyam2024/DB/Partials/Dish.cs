using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyamNyam2024.DB
{
    public partial class Dish
    {
        public double FinalPriceInDollar
        {
            get
            {
                return FinalPriceInCents * 0.01;
            }

        }
    }
}
