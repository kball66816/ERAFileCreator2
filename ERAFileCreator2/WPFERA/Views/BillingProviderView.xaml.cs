using System.Windows.Controls;
using System.Windows.Input;
using EraView.Utility;

namespace EraView.Views
{
    /// <summary>
    /// Interaction logic for BillingProviderView.xaml
    /// </summary>
    public partial class BillingProviderView : UserControl
    {
        public BillingProviderView()
        {
            InitializeComponent();
        }

        private void BillingProviderZipCodeInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToNumericOnly(e);
        }

        private void BillingProviderNpiInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToNumericOnly(e);
        }
    }
}
