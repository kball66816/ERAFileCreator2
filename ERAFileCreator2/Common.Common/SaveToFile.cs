using System;
using System.IO;
using System.Windows.Forms;


namespace Common.Common
{
    public static class SaveToFile
    {
        public static void SaveTextFiletoSelectedDirectory(this string file)
        {
            using (var saveFile = new SaveFileDialog())
            {
                SetTextFileFiltersForSaving(saveFile);
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFile.FileName, file);
                }
            }
        }

        /// <summary>
        /// Due to restrictions in read/write privilage this is the default folder
        /// for saving batch files to.
        /// </summary>
        /// <param name="file"></param>
        public static void SaveFiletoADefaultDirectory(this string file)
        {
            using (var saveFile = new SaveFileDialog())
            {
                const string directory = @"C:\Users\Public\Documents\835 Batch\";
                SetTextFileFiltersForSaving(saveFile);
                Directory.CreateDirectory(directory);
                var path = $"{directory}{saveFile.FileName}.{saveFile.DefaultExt}";
                File.WriteAllText(path, file);

            }
        }

        private static void SetTextFileFiltersForSaving(SaveFileDialog saveFile)
        {
            saveFile.Filter = "Text Files| *.txt";
            saveFile.DefaultExt = "txt";
            saveFile.FileName = DateTime.Now.ToString("yyyy_MM_dd_hmmssff");
        }

    }
}

