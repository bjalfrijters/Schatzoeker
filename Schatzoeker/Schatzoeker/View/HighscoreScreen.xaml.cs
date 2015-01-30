using Schatzoeker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Schatzoeker.View
{

    public sealed partial class HighscoreScreen : Page
    {
        private string message;
        private string playerName;
        private int score;

        private ObservableCollection<HighScore> _highscoresViewModel = HighscoreDataSource.GetHighscores();
        public ObservableCollection<HighScore> HighscoresViewModel
        {
            get { return this._highscoresViewModel; }
        }

        public HighscoreScreen()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //await ReadFromFile();

            if (this.Frame.BackStack.Last().SourcePageType == typeof(PuzzleScreen))
            {
                if (e.Parameter != null)
                {
                    var messageVar = e.Parameter;
                    message = (String)messageVar;
                    char[] splitToken = { ':' };
                    string[] messageSplit = message.Split(splitToken);
                    playerName = messageSplit[0];
                    string scoreString = messageSplit[1];
                    score = Int32.Parse(scoreString);
                    Debug.WriteLine(message + "hs screen");
                    _highscoresViewModel.Add(new HighScore() { name = playerName, score = score });


                }
            }

            foreach (HighScore hs in _highscoresViewModel)
            {
                HighscoreBlock.Text += hs.name.ToString() + " " + hs.score.ToString() + "\n";
            }

            //await WriteToFile();

        }

        //private async Task WriteToFile()
        //{
        //    var FileName = "Highscore.txt";
        //    DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<HighScore>));

        //    var localFolder = ApplicationData.Current.LocalFolder;
        //    var file = await localFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
        //    IRandomAccessStream sessionRandomAccess = await file.OpenAsync(FileAccessMode.ReadWrite);

        //    using (IOutputStream sessionOutputStream = sessionRandomAccess.GetOutputStreamAt(0))
        //    {
        //        serializer.WriteObject(sessionOutputStream.AsStreamForWrite(), _highscoresViewModel);
        //        Debug.WriteLine("I have written");
        //    }
        //}

        //private async Task ReadFromFile()
        //{
        //    var FileName = "Highscore.txt";
        //    DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<HighScore>));

        //    var localFolder = ApplicationData.Current.LocalFolder;
        //    var file = await localFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
        //    file = await localFolder.GetFileAsync(FileName);
        //    using (IInputStream sessionInputStream = await file.OpenReadAsync())
        //    {

        //        _highscoresViewModel = (ObservableCollection<HighScore>)serializer.ReadObject(sessionInputStream.AsStreamForRead());
        //        Debug.WriteLine("I have read");
        //    }
        //}
    }
}
