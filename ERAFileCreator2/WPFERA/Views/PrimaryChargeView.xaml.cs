using System.Windows.Controls;
using System.Windows.Input;
using EraView.Utility;

namespace EraView.Views
{
    /// <summary>
    /// Interaction logic for PrimaryChargeView.xaml
    /// </summary>
    public partial class PrimaryChargeView : UserControl
    {
        public PrimaryChargeView()
        {
            InitializeComponent();
        }

        private void PrimaryChargesAmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToDecimalOnly(e);
        }

        private void PrimaryPaidAmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToDecimalOnly(e);
        }

        private void PatientCopayInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToDecimalOnly(e);
        }
    }
}
