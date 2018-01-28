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
                if (saveFile.ShowDialog() == DialogResult.OK) File.WriteAllText(saveFile.FileName, file);
            }
        }

        public static void SaveFiletoADefaultDirectory(this string file)
        {
            using (var saveFile = new SaveFileDialog())
            {
                const string directory = @"\835 Batch\";
                var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                {
                    Directory.CreateDirectory(filePath + directory);
                }
                SetTextFileFiltersForSaving(saveFile);
                Directory.CreateDirectory(filePath);
                var path = $"{filePath}{directory}{saveFile.FileName}.{saveFile.DefaultExt}";
                File.WriteAllText(path, file);
            }
        }

        private static void SetTextFileFiltersForSaving(SaveFileDialog saveFile)
        {
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            saveFile.Filter = "Text Files| *.txt";
            saveFile.DefaultExt = "txt";
            saveFile.FileName = DateTime.Now.ToString("yyyy_MM_dd_hmmssff");
        }
    }
}