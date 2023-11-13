using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Security.Cryptography;
using System.Globalization;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentNumber = "";
        private string previousExpression = "";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (currentNumber == "0")
            {
                currentNumber = digit;
            }
            else
            {
                currentNumber += digit;
            }

            currentNumberTextBox.Text = currentNumber;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operatorSymbol = button.Content.ToString();

            if (!string.IsNullOrEmpty(currentNumber))
            {
                previousExpression = $"{previousExpression} {currentNumber} {operatorSymbol}";
                currentNumber = "";
                previousExpressionTextBox.Text = previousExpression;
                currentNumberTextBox.Text = currentNumber;
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentNumber))
            {
                previousExpression = $"{previousExpression} {currentNumber}";
                try
                {
                    var result = new DataTable().Compute(previousExpression, null);
                    currentNumber = result.ToString();
                    previousExpressionTextBox.Text = "";
                    currentNumberTextBox.Text = currentNumber;
                    previousExpression = "";
                }
                catch (Exception ex)
                {
                    currentNumberTextBox.Text = "Ошибка";
                }
            }
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = "";
            currentNumberTextBox.Text = currentNumber;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = "";
            previousExpression = "";
            currentNumberTextBox.Text = currentNumber;
            previousExpressionTextBox.Text = previousExpression;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentNumber))
            {
                currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
                currentNumberTextBox.Text = currentNumber;
            }
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!currentNumber.Contains("."))
            {
                currentNumber += ".";
                currentNumberTextBox.Text = currentNumber;
            }
        }
    }
}