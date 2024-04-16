using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyamNyam2024.DB
{
    public partial class IngredientOfStage
    {
        private bool _color;
        public double TotalQuantity { get; set; }
        public double TotalCost { get; set; }
        public double SumQuantity { get; set; }

        public bool ColorRadioButtom
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
    }
}
