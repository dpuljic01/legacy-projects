using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whack_a_Mouse
{
    public class Player
    {
        #region Properties

        public static string ScoresWon;//statičko svojstvo..stavljeno da bi bodovima moga pristupit iz bilo koje klase, tj. da bi ih ispisa na drugoj formi

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }
        private int _score;
        public int Score
        {
            get { return _score; }
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Bodovi ne mogu biti manji od nula!");//primjer iznimke, uhvaćena je u klasi Submit
                _score = value; 
            }
        }
        
        //private int _rank;
        //public int Rank
        //{
        //    get { return _rank; }
        //    set { _rank = value; }
        //}
        
        public Player(string n, int s)
        {
            PlayerName = n;
            Score = s;
        }
        #endregion

        public override string ToString()//override metode ToString()
        {
            return PlayerName + "\t" + Score;
        }
    }
}
