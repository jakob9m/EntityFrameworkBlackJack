using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Serialization
    {

        public static void SerializeTextToFile(StringBuilder text)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = ".bin";

            if (saveFileDialog.ShowDialog() == true)  // Show the dialog and check if a file was selected
            {
                string filePath = saveFileDialog.FileName;
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(stream, text.ToString());
                }
            }
        }


    }
}
