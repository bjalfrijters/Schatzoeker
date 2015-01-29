using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schatzoeker.Model
{
    class HighScore
    {
        public Dictionary<Player, int> hsDict = null;
        public Dictionary<Player, int> sortedDictDesc = null;

        public HighScore()
        {
            hsDict = new Dictionary<Player, int>();
            var sortedDictDescVar = from entry in hsDict orderby entry.Value descending select entry;
            sortedDictDesc = (Dictionary<Player, int>)sortedDictDescVar;

        }

        public void addPlayer(Player player, int HighScore)
        {
            if (hsDict == null)
            {
                hsDict.Add(player, HighScore);
            }
            else
            {
                var sortedDict = from entry in hsDict orderby entry.Value ascending select entry;
                    if (hsDict.Count >= 10)
                    {
                        if (sortedDict.First().Value < HighScore)
                        {
                            hsDict.Remove(sortedDict.First().Key);
                            hsDict.Add(player, HighScore);
                        }
                    }
                    else hsDict.Add(player, HighScore);
                    
            }
        }
    }
}
