using System.Windows;
using EraFileCreator.Utility;
using System.Windows.Controls;
using System.Windows.Input;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for AddonChargeView.xaml
    /// </summary>
    public partial class AddonChargeView : UserControl
    {
        public AddonChargeView()
        {
            InitializeComponent();
        }

        private void AddonProcedureCodeInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonModifierOneInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonModifierTwoInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonModifierThreeInput_GotFocus(object sender,RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonPaidAmountInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonChargesAmountInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonChargesAmountInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonModifierTwoInput_GotFocus_1(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonModifierFourInput_OnGotFocus(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }
    }
}