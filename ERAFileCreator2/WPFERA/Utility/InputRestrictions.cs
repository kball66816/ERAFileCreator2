using System.Windows.Input;

namespace EraFileCreator.Utility
{
    public static class InputRestrictions
    {
        public static void RestrictTextToIntegerOnly(TextCompositionEventArgs e)
        {
            if (!(int.TryParse(e.Text, out int result)))
            {
                e.Handled = true;
            }
        }

        public static void RestrictTextToDecimalOnly(TextCompositionEventArgs e)
        {
            if (!(decimal.TryParse(e.Text, out decimal result)))
            {
                e.Handled = true;
            }
        }
    }
}
