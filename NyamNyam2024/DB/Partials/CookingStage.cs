using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyamNyam2024.DB
{
    public partial class CookingStage
    {
        public static int stepNumber = 1; // Инициализируем переменную для порядкового номера шага за пределами свойства Steps

        public string Steps
        {
            get
            {
                string[] steps = ProcessDescription.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder stepsWithNumbers = new StringBuilder();

                foreach (var step in steps)
                {
                    string cleanStep = step.Trim();
                    stepsWithNumbers.AppendFormat("{0}. {1}\n", stepNumber, cleanStep);
                    stepNumber++;
                }

                return stepsWithNumbers.ToString();
            }
        }
    }
}
