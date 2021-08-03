using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfCalculator.Models;

namespace wpfCalculator.Views
{
    /// <summary>
    /// Interaction logic for CalculatorControl.xaml
    /// </summary>
    public partial class CalculatorControl : UserControl, INotifyPropertyChanged
    {
        private CalculatorModel _calculator;
        private double _operandA;
        private double _operandB;
        private string _currentOperator;
        private string _previewValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public string PreviewValue
        {
            get { return _previewValue; }
            set { _previewValue = value; OnPropertyChanged("PreviewValue"); }
        }

        public string CurrentValue
        {
            get { return _calculator.CurrentValue; }
            set { _calculator.CurrentValue = value; OnPropertyChanged("CurrentValue"); }
        }

        public double OperandA
        {
            get { return _operandA; }
            set { _operandA = value; OnPropertyChanged("OperandA"); }
        }

        public double OperandB
        {
            get { return _operandB; }
            set { _operandB = value; OnPropertyChanged("OperandB"); }
        }

        public string CurrentOperator
        {
            get { return _currentOperator; }
            set { _currentOperator = value; OnPropertyChanged("CurrentOperator"); }
        }

        public CalculatorControl()
        {
            InitializeComponent();
            _calculator = new CalculatorModel()
            {
                Functions = new string[] { "", "", "", "Clr" },
                Operators = new string[] { "+", "-", "x", "/", "=" },
                Values = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' }
            };
            PreviewValue = "";
            DataContext = this;
            PopulateCalculatorButtons();
        }

        public void Functions_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (button.Content.Equals("Clr"))
                {
                    CurrentValue = "";
                    OperandA = 0d;
                    OperandB = 0d;
                    CurrentOperator = "";
                }
            }
        }

        public void Operators_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                if (button.Content.ToString().Equals("="))
                {
                    // = pressed with no operator
                    if (CurrentOperator.Equals(""))
                    {

                    }
                    // = pressed with operator
                    else
                    {
                        OperandB = Double.Parse(CurrentValue);
                        CurrentValue = PerformOperation(CurrentOperator).ToString();
                        OperandA = 0d;
                        OperandB = 0d;
                        CurrentOperator = "";
                    }
                    CurrentOperator = "";
                }

                // operator pressed
                if (!button.Content.ToString().Equals("="))
                {
                    OperandA = Double.Parse(CurrentValue);
                    CurrentOperator = button.Content.ToString();
                    CurrentValue = "";
                    PreviewValue = $"{OperandA} {CurrentOperator}";
                }
            }
        }

        public void Values_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // no operator active
                if (!CurrentOperator.Equals(""))
                {

                }
                // an operator is active
                else
                {
                  //  CurrentValue = "";
                }
                CurrentValue += button.Content.ToString();
            }
        }

        public double PerformOperation(string operation)
        {
            if (operation.Equals("+"))
            {
                return OperandA + OperandB;
            }
            else if (operation.Equals("-"))
            {
                return OperandA - OperandB;
            }
            else if (operation.Equals("x"))
            {
                return OperandA * OperandB;
            }
            else if (operation.Equals("/"))
            {
                return OperandA / OperandB;
            }
            throw new ArgumentException("The operation provided could not be performed.");
        }

        public void PopulateCalculatorButtons()
        {
            int row = 0;
            int col = 0;

            // Assumes the number of Grid definitions is greater than or equal to the length of the list.
            foreach (string func in _calculator.Functions)
            {
                Button b = new Button() { Content = func };
                b.Click += new RoutedEventHandler(Functions_Click);
                Grid.SetColumn(b, col);
                FunctionsGrid.Children.Add(b);
                col++;
            }

            col = 0;
            // Assumes the number of Grid definitions is greater than or equal to the length of the list.
            foreach (string c in _calculator.Operators)
            {
                Button b = new Button() { Content = c };
                b.Click += new RoutedEventHandler(Operators_Click);
                Grid.SetRow(b, row);
                OperatorsGrid.Children.Add(b);
                row++;
            }

            row = 0;
            // Assumes the number of Grid definitions is greater than or equal to the length of the list.
            foreach (char c in _calculator.Values)
            {
                Button b = new Button() { Content = c };
                b.Click += new RoutedEventHandler(Values_Click);
                Grid.SetColumn(b, col);
                Grid.SetRow(b, row);
                ValuesGrid.Children.Add(b);

                col++;
                if (col > ValuesGrid.ColumnDefinitions.Count - 1)
                {
                    col = 0;
                    row++;
                }
                if (row > ValuesGrid.RowDefinitions.Count - 1)
                {
                    break;
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

    }
}
