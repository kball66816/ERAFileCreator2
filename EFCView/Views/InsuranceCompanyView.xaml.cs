using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for InsuranceCompanyView.xaml
    /// </summary>
    public partial class InsuranceCompanyView : UserControl
    {
        public InsuranceCompanyView()
        {
            InitializeComponent();
        }

        private void InsuranceZipCodeInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRestrictions.RestrictTextToIntegerOnly(e);
        }

        private void InsuranceCompanyNameInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceCompanyNameInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceCompanyTaxIdInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceStreetLineOneInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceStreetLineOneInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceStreetLineTwoInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceStreetLineTwoInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceCityInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceCityInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceZipCodeInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void InsuranceZipCodeInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }
    }
}