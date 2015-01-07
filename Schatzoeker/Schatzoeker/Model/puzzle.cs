using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schatzoeker.Model
{

    class Puzzle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("answer")]
        public string _answer { get; set; }


        public Puzzle()
        {

        }

        public Puzzle(string answer)
        {
            _answer = answer;
        }

        public string getRandom(){
            char[] chars = _answer.ToCharArray();
            Random rnd = new Random();
            string puzzleAnswer = "";
            while (puzzleAnswer.Length < _answer.Length)
            {
                int random = rnd.Next(_answer.Length);
                if (chars[random] != '0')
                {
                    puzzleAnswer += chars[random];
                    chars[random] = '0';
                }
            }
            return puzzleAnswer;
        }
    }
}
