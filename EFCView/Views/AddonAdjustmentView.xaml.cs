using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for AddonAdjustmentView.xaml
    /// </summary>
    public partial class AddonAdjustmentView : UserControl
    {
        public AddonAdjustmentView()
        {
            InitializeComponent();
        }

        private void AddonAdjustmentAmountInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonAdjustmentAmountInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void AddonAdjustmentAmountInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            InputRules.IgnoreDecimalInput(sender, e);
        }
    }
}