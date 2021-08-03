using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfCalculator.Models
{
    public class CalculatorModel
    {
        public string CurrentValue { get; set; }

        public string Operand1 { get; set; }
        public string Operand2 { get; set; }

        // Declare Buttons.
        public string[] Operators { get; set; }
        public char[] Values { get; set; }
        public string[] Functions { get; set; }
    }
}
