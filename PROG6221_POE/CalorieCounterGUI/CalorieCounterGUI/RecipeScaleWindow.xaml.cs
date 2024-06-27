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
using System.Windows.Shapes;

namespace CalorieCounterGUI
{
    public partial class ScaleWindow : Window
    {
        public double ScaleFactor { get; private set; }

        public ScaleWindow()
        {
            InitializeComponent();
        }

        private void ApplyScale_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ScaleFactorTextBox.Text, out double factor))
            {
                ScaleFactor = factor;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number for the scale factor.", "Invalid Input");
            }
        }
    }
}