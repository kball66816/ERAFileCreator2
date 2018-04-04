using System.Windows;
using EraFileCreator.Utility;
using System.Windows.Controls;
using System.Windows.Input;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for BillingProviderView.xaml
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

        private void BillingProviderNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderNameInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BusinessNpiInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void ProviderAddressLineOneInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void ProviderAddressLineOneInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderAddressTwoInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderAddressTwoInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderCityInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderCityInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderZipCodeInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderLastNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderNpiInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void BillingProviderFirstNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }
    }
}