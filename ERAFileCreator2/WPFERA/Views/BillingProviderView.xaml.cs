using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
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
            InputRestrictions.RestrictTextToIntegerOnly(e);
        }

        private void BillingProviderNpiInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToIntegerOnly(e);
        }
    }
}
