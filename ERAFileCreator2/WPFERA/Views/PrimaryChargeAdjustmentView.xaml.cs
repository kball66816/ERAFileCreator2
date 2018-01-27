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
using EraView.Utility;

namespace EraView.Views
{
    /// <summary>
    /// Interaction logic for PrimaryChargeAdjustmentView.xaml
    /// </summary>
    public partial class PrimaryChargeAdjustmentView : UserControl
    {
        public PrimaryChargeAdjustmentView()
        {
            InitializeComponent();
        }

        private void PrimaryAdjustmentAmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToDecimalOnly(e);
        }
    }
}
