using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfCalculator.Models;

namespace wpfCalculator.ViewModels
{
    class CalculatorViewModel
    {
        private CalculatorModel _model;

        public string CurrentValue
        {
            get { return _model.CurrentValue; }
            set { _model.CurrentValue = value; }
        }
        public string[] Functions
        {
            get { return _model.Functions; }
            set { _model.Functions = value; }
        }
        public string[] Operators
        {
            get { return _model.Operators; }
            set { _model.Operators = value; }
        }
        public string[] Values
        {
            get { return _model.Values; }
            set { _model.Values = value; }
        }

        CalculatorViewModel()
        {
            _model = new CalculatorModel()
            {};
        }
    }
}
