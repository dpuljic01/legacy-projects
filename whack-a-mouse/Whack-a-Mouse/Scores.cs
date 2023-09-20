using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Whack_a_Mouse
{
    public partial class Scores : Form
    {
        public Scores()
        {
            InitializeComponent();
        }
        protected override bool ProcessDialogKey(Keys keyData)//prati je li pritisnuta tipka escape..ako se pritisne dok je forma otvorena, zatvorit će se
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return false;
        }

        List<Player> topList = new List<Player>();
        private void Scores_Load(object sender, EventArgs e)
        {
            topList.Clear();
            string filename = "highscores.txt";
            using (StreamReader sr = File.OpenText(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(';');
                    string name = s[0];
                    int score = int.Parse(s[1]);
                    topList.Add(new Player(name, score));
                }
            }

            //language integrated query(linq)
            List<Player> sorted = topList.OrderByDescending(p => p.Score).ToList();//spremiti sortiranu listu u novu listu pomoću linq (našao na stackoverflow stranici)
            //ili ovako 
            //List<Player> sorted = (from pl in topList
            //                       orderby pl.Score descending
            //                       select pl).ToList();
            
            lstScores.Items.Clear();
            int rank = 1;
            foreach (Player p in sorted)
            {
                if (rank < 10)//top 9
                {
                    lstScores.Items.Add("  "+rank.ToString() + ".\t" + p.ToString());//override metode ToString(), vraća Name+"\t"+Score;
                }
                if (rank == 10)
                {
                    lstScores.Items.Insert(9, rank.ToString()+".\t" + p.ToString());//dodaj 10. igrača na zadnje mjesto(indeks 9)
                }
                rank++;
            }
        }
    }
}
