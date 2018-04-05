using System.Windows.Controls;
using System.Windows.Input;


namespace EraFileCreator.Utility
{
    public static class InputRules
    {
        public static void HighlightAllText(object sender)
        {
            if (sender is TextBox tb)
            {
               tb.SelectAll();
            }
        }

        public static void IgnoreDecimalInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Decimal|| e.Key == Key.OemPeriod)
            {
                if (sender is TextBox tb)
                {
                    e.Handled = true;
                    tb.CaretIndex += 1;
                }
            }
        }
    }
}
