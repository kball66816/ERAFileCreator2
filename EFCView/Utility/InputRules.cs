using System.Windows;
using System.Windows.Controls;


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
    }
}
