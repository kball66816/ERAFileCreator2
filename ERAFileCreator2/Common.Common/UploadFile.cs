using System;
using System.IO;
using System.Windows.Forms;

namespace Common.Common
{
    public static class UploadFile
    {
        public static string Contents { get; private set; }

        public static void SelectFileForUpload()
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
                        Contents = streamReader.ReadLine();
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }



        }
    }
}
