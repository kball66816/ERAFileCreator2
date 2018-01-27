using EraView.Utility;
using System.Windows.Controls;
using System.Windows.Input;

namespace EraView.Views
{
    /// <summary>
    /// Interaction logic for AddonChargeView.xaml
    /// </summary>
    public partial class AddonChargeView : UserControl
    {
        public AddonChargeView()
        {
            InitializeComponent();
        }

        private void AddonChargesAmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToDecimalOnly(e);
        }

        private void AddonPaidAmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToDecimalOnly(e);

        }
    }
}
