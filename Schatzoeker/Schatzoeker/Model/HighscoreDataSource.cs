using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using Windows.Storage.Streams;

namespace Schatzoeker.Model
{
    class HighscoreDataSource
    {
        private static ObservableCollection<HighScore> _highscores = new ObservableCollection<HighScore>();

        public static ObservableCollection<HighScore> GetHighscores()
        {
                _highscores.Add(new HighScore() { name = "Bart", score = 420 });
                _highscores.Add(new HighScore() { name = "Piet", score = 2 });
            
            return _highscores;
        }

        

    }
}
