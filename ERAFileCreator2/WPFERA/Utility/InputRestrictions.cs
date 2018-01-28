using System.Windows.Input;

namespace EraFileCreator.Utility
{
    public class InputRestrictions
    {
        public static void RestrictTextToNumericOnly(TextCompositionEventArgs e)
        {
            if (!(int.TryParse(e.Text, out int result)))
            {
                e.Handled = true;
            }
        }
    }
}
