using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            this.InitializeComponent();
        }

        private void HightlightAllText(object sender, RoutedEventArgs e)
        {
            InputRules.HighlightAllText(sender);
        }

        private void IgnoreDecimalInput(object sender, KeyEventArgs e)
        {
            InputRules.IgnoreDecimalInput(sender, e);
        }

        private void Expander_KeyDown(object sender, KeyEventArgs e)
        {
            ExpandOrRetractOnSpaceDown(sender, e);
        }

        private static void ExpandOrRetractOnSpaceDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                if (sender is Expander exp)
                {
                    e.Handled = true;
                    exp.IsExpanded = exp.IsExpanded != true;
                }
        }

        private void Expander_KeyDown_1(object sender, KeyEventArgs e)
        {
            ExpandOrRetractOnSpaceDown(sender, e);
        }

        private void Expander_KeyDown_2(object sender, KeyEventArgs e)
        {
            ExpandOrRetractOnSpaceDown(sender, e);
        }
    }
}