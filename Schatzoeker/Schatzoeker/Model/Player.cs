using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schatzoeker.Model
{
    class Player
    {
        private string _playerName;
        
        
        public Player(string name)
        {
            _playerName = name;
        }

        public void SetPlayerName(String name)
        {
            _playerName = name;
        }

        public String GetPlayerName()
        {
            return _playerName;
        }        
        
    }
}
