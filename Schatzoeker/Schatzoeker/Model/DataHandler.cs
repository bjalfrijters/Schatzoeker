using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Windows.Storage;

namespace Schatzoeker.Model
{
    class DataHandler
    {
        private String fileName;
        private String[] text;
        public static DataHandler()
        {

        }

        public static void writeFile(String filename)
        {
            //File
        }

        /*
        public async void start()
        {
            await WriteToFile("test");
        }

        private async Task WriteToFile(String text)
        {
            // Get the text data from the textbox. 
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(text.ToCharArray());

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Create a new folder name DataFolder.
            var dataFolder = await local.CreateFolderAsync("DataFolder",
                CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            var file = await dataFolder.CreateFileAsync("DataFile.txt",
            CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        private async Task ReadFile()
        {
            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (local != null)
            {
                // Get the DataFolder folder.
                var dataFolder = await local.GetFolderAsync("DataFolder");

                // Get the file.
                var file = await dataFolder.OpenStreamForReadAsync(fileName);

                // Read the data.
                using (StreamReader streamReader = new StreamReader(file))
                {
                    this.text = streamReader.ReadToEnd().Split();
                }

            }
        }
        */
    }



}