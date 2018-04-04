using System.Windows.Controls;
using System.Windows.Input;
using EraFileCreator.Utility;

namespace EraFileCreator.Views
{
    /// <summary>
    ///     Interaction logic for PrimaryChargeAdjustmentView.xaml
    /// </summary>
    public partial class PrimaryChargeAdjustmentView : UserControl
    {
        public PrimaryChargeAdjustmentView()
        {
            InitializeComponent();
        }

        private void PrimaryAdjustmentAmountInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void PrimaryAdjustmentAmountInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}