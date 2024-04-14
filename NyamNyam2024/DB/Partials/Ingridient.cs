using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyamNyam2024.DB
{
    public partial class Ingredient
    {
        public double CostInDollar
        {
            get
            {
                return CostInCents * 0.01;
            }

        }
    }
}
