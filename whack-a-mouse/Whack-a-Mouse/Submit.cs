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
    public partial class Submit : Form
    {
        public Submit()
        {
            InitializeComponent();
        }
        private void Submit_Load(object sender, EventArgs e)
        {
            /* zapisivanje broja bodova u labelu "lblScore" na formi "Submit" pomoću svojstva "ScoresWon"
             * pošto se labela Points u igri sastoji od rijeci "Points:" i  "broja bodova", podijelio sam je na
             * dva dijela i uzeo drugi dio (broj bodova)  */
            lblScore.Text = Player.ScoresWon.Split(' ')[1];

            textBox1.Focus();//prilikom pokretanja fokus će biti na upisu imena
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            int score;
            try
            {
                score = Convert.ToInt32(lblScore.Text);
            }
            catch (ArgumentException greska)//hvata onu iznimku u klasi player (bodovi ne mogu bit manji od 0)
            {
                MessageBox.Show(greska.Message);
                return;
            }
            catch (FormatException)
            {
                MessageBox.Show("Greška pri parsiranju!");
                return;
            }

            if (name != "")
            {
                string s = String.Format(name + ";" +score);
                File.AppendAllText("highscores.txt", s + Environment.NewLine);//promijenio putanju, bila je na C: disku, a sad je u bin/Debug
            }
            else
            {
                MessageBox.Show("Unesite ime ili pritisnite 'Cancel' ako ne želite spremiti rezultat!", "Warning!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
