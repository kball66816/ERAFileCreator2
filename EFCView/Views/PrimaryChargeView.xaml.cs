using System;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Common.Extensions;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for PrimaryChargeView.xaml
    /// </summary>
    public partial class PrimaryChargeView : UserControl
    {
        public PrimaryChargeView()
        {
            InitializeComponent();
        }

        private void PrimaryChargesAmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.ToString() == ".")
            {

            }
        }

        private void BillIdInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PatientCopayInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryProcedureCodeInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryModifierOneInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryModifierTwoInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryModifierThreeInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryModifierFourInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryPaidAmountInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryChargesAmountInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PatientCopayInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryChargesAmountInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryPaidAmountInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryChargesAmountInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            InputRules.IgnoreDecimalInput(sender, e);
        }

        private void PrimaryPaidAmountInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            InputRules.IgnoreDecimalInput(sender,e);
        }

        private void PatientCopayInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            InputRules.IgnoreDecimalInput(sender, e);
        }
    }
}