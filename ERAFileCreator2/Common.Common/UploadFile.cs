using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Common.Common.Extensions;

namespace Common.Common
{
    public static class UploadFile
    {
        public static string UploadedFileAsStringContent { get; private set; }

        public static void TextFileUpload()
        {
            var file = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = "txt files (*.txt)|*.txt|837 files(*.837)|*.837|All files(*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (file.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var streamReader = new StreamReader(file.OpenFile()))
                    {
                        BuildFileToSTring(streamReader);
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                UploadedFileAsStringContent = string.Empty;
            }
        }

        private static void BuildFileToSTring(StreamReader streamReader)
        {
            var sb = new StringBuilder();
            while (!streamReader.EndOfStream)
            {
                sb.Append(streamReader.ReadLine()?.TrimStart());
            }

            UploadedFileAsStringContent = sb.ToString();
        }
    }
}
