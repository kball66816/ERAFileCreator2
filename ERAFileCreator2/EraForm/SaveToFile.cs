using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EraForm
{
    public class SaveToFile
    {
        public void SaveFile(string file)
        {
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "Text Files| *.txt";
                saveFile.DefaultExt = "txt";
                saveFile.FileName = DateTime.Now.ToString("yyyy_MM_dd_hmmssff");
                if(saveFile.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFile.FileName, file.ToString());
                }

            }
        }

    }
}

