using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EraView.Utility
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
