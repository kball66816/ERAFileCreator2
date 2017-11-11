using System;
using System.IO;
using System.Windows.Forms;

namespace Common.Common
{
    public class SaveToFile
    {
        public void SaveTextFile(string file)
        {
            using (var saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "Text Files| *.txt";
                saveFile.DefaultExt = "txt";
                saveFile.FileName = DateTime.Now.ToString("yyyy_MM_dd_hmmssff");
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFile.FileName, file);
                }
            }
        }
    }
}

