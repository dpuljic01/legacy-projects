using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Whack_a_Mouse
{
    
    public partial class Game : Form
    {
        public static bool START = true;
        public static List<object> allItems = new List<object>();
        Sensing sensing=new Sensing();
        //SoundPlayer[] sounds = new SoundPlayer[3];

        public Game() : base()
        {
            InitializeComponent();

            //Set the maximum size, so if user maximizes form, it 
            //will not cover entire desktop.  
            this.MaximumSize = new System.Drawing.Size(800,600);


        }
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (Field f in allItems)
            {
                if (f != null)
                {
                    if (f.Show == true)
                    {
                        lock (f.CurrentItem)
                            g.DrawImage(f.CurrentItem, new Rectangle(f.X, f.Y, f.Width, f.Height));
                    }
                }
            }
        }
        private void LoadForm(object sender, EventArgs e)
        {
            Transparency();
            panel1.BackgroundImage = new Bitmap("background\\start.png");
        }

        
        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            SlipperChange();
            CountPoints();
            lblPoints.Text = "Points: " + bodovi.ToString();
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            sensing.MouseDown = true;
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;
        }
        
        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                Player.ScoresWon = lblPoints.Text;//spremiti bodove u statičku varijablu te ih prikazat na formi kad se otvori
                Stop();
                panel1.Show();
                sensing.Key = "";//da ne ostane Escape pritisnut, jer ako se ne upre neka druga tipka prije ponovnog pokretanja igre, igra će biti odma završena
            }
            if (START)
            {
                this.Refresh();//dok je start=true, svake milisekunde osvježava svijet
            }
        }


        #endregion
        
        #region SoundMethods

        //public void loadSound(int soundNum, string filename)
        //{
        //    sounds[soundNum] = new SoundPlayer(filename);
        //}
        //public void playSound(int soundNum)
        //{
        //    sounds[soundNum].Play();
        //}
        //public void stopSound(int soundNum)
        //{
        //    sounds[soundNum].Stop();
        //}

        #endregion

        private void CountPoints()//metoda za brojanje bodova
        {
            if (bodovi >= 0)
            {
                if (bodovi == 0 && bod == -7)//ako su bodovi na nula i bude promašaj, postaviti bodove na 0, da ne padnu ispod
                {
                    bodovi = 0;
                }
                else
                    bodovi += bod;//inače povećati bodove
            }
        }
        
        private void Transparency()
        {
            this.lblVrijeme.BackColor = Color.Transparent;
            this.lblStart.BackColor = Color.Transparent;
            this.lblAbout.BackColor = Color.Transparent;
            this.lblHelp.BackColor = Color.Transparent;
            this.lblScores.BackColor = Color.Transparent;
            this.lblPoints.BackColor = Color.Transparent;
            this.lblExit.BackColor = Color.Transparent;
            this.lblEsc.BackColor = Color.Transparent;
        }

        public static void AddItem(Field s)
        {
            Game.allItems.Add(s);
        }

        private void StartScript(Func<int> scriptName)
        {
            Task t;
            t = Task.Run(scriptName);
        }

        private int SlucajanBroj(int min, int max)//[min, max]
        {
            Random r = new Random();
            int br = r.Next(min, max + 1);
            return br;
        }
        
        private void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        
        //Inicijalizacija

        int bodovi;
        int mouseOutCount;//broji koliko je puta miš izašao
        Mouse mis;
        Hole[] hole = new Hole[3], abovehole = new Hole[3];
        Slipper slipper, slipperdown;
        private void SetupGame()
        {
            setBackgroundPicture("background\\background.png");
            mis = new Mouse("Pictures\\mouse1.png", 570, 300);
            mis.SetSize(40);

            slipperdown = new Slipper("Pictures\\slipperdown1.png", sensing.Mouse.X, sensing.Mouse.Y);
            slipperdown.SetSize(30);
            slipperdown.Show = false;
            slipper = new Slipper("Pictures\\slipper1.png", sensing.Mouse.X, sensing.Mouse.Y);
            slipper.SetSize(40);

            hole = new Hole[3];
            hole[0] = new Hole("Pictures\\hole1.png", 120, 250);
            hole[1] = new Hole("Pictures\\hole1.png", 345, 250);
            hole[2] = new Hole("Pictures\\hole1.png", 570, 250);

            abovehole = new Hole[3];//napola izrezana rupa stavljena iznad postojećih rupa koja je ujedno i iznad miša, tako da izgleda kao da miš izlazi iz nje
            abovehole[0] = new Hole("Pictures\\hole21.png", 120, 281);
            abovehole[1] = new Hole("Pictures\\hole21.png", 345, 281);
            abovehole[2] = new Hole("Pictures\\hole21.png", 570, 281);


            for (int i = 0; i < 3; i++)
            {
                hole[i].SetSize(65);
                AddItem(hole[i]);
            }

            AddItem(mis);

            for (int i = 0; i < 3; i++)
            {
                abovehole[i].SetSize(65);
                AddItem(abovehole[i]);
            }

            AddItem(slipperdown);
            AddItem(slipper);
            
            //SoundLoad();
            StartScript(MisIzlazi);
            StartScript(MoveSlipperAndWhack);
        }

        public delegate void MisHandler();//vlastiti delegat, sadrži referencu na metodu mousePositionChange
        MisHandler misIzlazi;//...instanca delegata (delegat objekt)-->vidi skriptu "MisIzlazi"

        #region Scripts

        private int MisIzlazi()
        {
            misIzlazi = mousePositionChange;//slanje delegata događaju misIzlazi, koji poziva ovu metodu mousePositionChange
            while (START)
            {
                misIzlazi.Invoke();
                MisGore();
            }
            return 0;
        }
        
        private int MoveSlipperAndWhack()
        {
            while (START)
            {
                slipper.GotoMousePoint(sensing.Mouse);
                slipperdown.GotoMousePoint(sensing.Mouse);
                ClickAndWhack();
            }
            return 0;
        }

        #endregion

        private void mousePositionChange()//metoda za mijenjanje pozicije miša, pokrećem je mojim događajem MisHandler
        {
            new ManualResetEvent(false).WaitOne(500);//vrijeme čekanja kada miš izađe( pola sekunde )
            mis.Show = false;//kad prođe pola sekunde, sakriti miša
            int x = SlucajanBroj(0, 2);//promijeniti mu x i y koordinatu
            switch (x)
            {
                case 0:
                    mis.SetX(150);//x koordinata prve rupe
                    break;
                case 1:
                    mis.SetX(375);//druga rupa
                    break;
                case 2:
                    mis.SetX(600);//treća rupa
                    break;
            }
            mis.SetY(215 + mis.Height);//y koordinata, ista za sve rupe
            mis.Show = true;// i ponovo pokazati miša
            if (mouseOutCount % 7 == 0)//svakih 7 izlazaka povećati brzinu za 1 piksel
                GameOptions.Speed += 1;
            mouseOutCount++;//brojač izlaska miša povećati za 1
        }
        
        int bod;//pomoćna varijabla, ovisno o udarcu zapisuje broj bodova i predaje ga varijabli "bodovi"
        private void ClickAndWhack()
        {
            if(sensing.MouseDown)
            {
                SlipperChange();//ako se klikne tipka miša, zamijeni papuču
                if (slipperdown.TouchingItem(mis))//ako papuča klepne miša, sakrij miša, pokreni zvuk udarca i povećaj bodove
                {
                    //playSound(0);
                    mis.Show = false;
                    bod = 14;
                    sensing.MouseDown = false;
                }
                else//ako je papuča spuštena a ne dira miša, pokreni zvuk i smanji bodove
                {
                    bod = -7;
                    //playSound(1);
                    sensing.MouseDown = false;
                }
                
            }
        }

        private void SlipperChange()//mijenjanje papuče
        {
            slipper.Show = false;
            slipperdown.Show = true;
            new ManualResetEvent(false).WaitOne(50);//bez ovoga se prebrzo izmijene papuče, pa se ne vidi efekt udarca
            slipper.Show = true;
            slipperdown.Show = false;
        }
        
        //private void SoundLoad()
        //{
        //    loadSound(0, "whack.wav");
        //    loadSound(1, "missed1.wav");
        //}

        private void MisGore()//mis izlazi iz rupe
        {
            while (mis.Y > 235)
            {
                mis.MoveUp(GameOptions.Speed);
                new ManualResetEvent(false).WaitOne(10);
            }
        }

        private void tmrCountdown_Tick(object sender, EventArgs e)//interval je postavljen na 1000ms(= 1s), tako da svake sekunde otkuca
        {
            if (GameOptions.TimeLeft > 0)
            {
                GameOptions.TimeLeft -= 1;
                lblVrijeme.Text = "Time left: " + GameOptions.TimeLeft + " sec";
                if (GameOptions.TimeLeft <= 10) //kada vrijeme padne ispod 10 sekundi, promijeniti boju labele u crveno
                    lblVrijeme.ForeColor = Color.Red;
            }
            else
            {
                Player.ScoresWon = lblPoints.Text;//zapisati bodove s labele u statičku varijablu u klasu Player
                lblVrijeme.Text = "Time's up!";
                Stop();
                panel1.Show();
            }
        }

        private void ShowScores()
        {
            Submit s = new Submit();
            s.ShowDialog();
        }

        private void tmrSlide_Tick(object sender, EventArgs e)
        {
            //while (this.Height < 600)
            //{
            //    this.Height += 8;//dok visina prozora ne bude 480, povećavati ga za 8 piksela uz interval od 1 ms
            //}

            //while (this.Width < 800)
            //{
            //    this.Width += 8;
            //}
            //tmrSlide.Stop();

            //kad se otvori cijela forma prikazati labele
            //lblStart.Visible = true;
            //lblHelp.Visible = true;
            //lblAbout.Visible = true;
            //lblScores.Visible = true;
            //lblExit.Visible = true;
            
        }

        private void Start()
        {
            mouseOutCount = 1;//vratit sve na početne vrijednosti
            bodovi = 0;
            allItems.Clear();
            GameOptions.Speed = 1;
            GameOptions.TimeLeft = 60;
            panel1.Visible = false;//sakriti početni zaslon
            tmrCountdown.Start();//startati odbrojavanje vremena
            lblVrijeme.Text = "Time left: " + GameOptions.TimeLeft.ToString() + " sec";//prikazati vrijeme
            lblVrijeme.ForeColor = Color.Black;//vratiti boju na crno, jer ispod 10 sekundi se mijenja u crvenu
            lblPoints.Show();//pokazat labelu bodova
            lblPoints.Text = "Points: " + bodovi.ToString();//prikazati bodove
            lblEsc.Show();//prikazati labelu za izaći
            Cursor.Hide();//sakriti pokazivač miša(da ne smeta u igri)
            timer1.Start();//startati timer (svake milisekunde osvježava igru-->interval=1)
            START = true;//pokrenuti skripte
            SetupGame();//inicijalizacija svijeta i likova, njihove postavke
        }
        private void Stop()
        {
            START = false;//zaustaviti skripte
            lblPoints.Hide();
            lblEsc.Hide();
            tmrCountdown.Stop();//zaustaviti odbrojavanje
            timer1.Stop();//zaustaviti timer
            new ManualResetEvent(false).WaitOne(1000);//čekati sekundu prije pokazivanja forme s bodovima
            Cursor.Show();//pokazati pokazivač miša
            ShowScores();//prikazati formu s bodovima
        }
        
        
        //private void LabelSound()
        //{
        //    loadSound(2, "mouseover.wav");
       
        //    playSound(2);
        //}

        #region StartScreen Label Styles and Events

        Font enhance = new Font("Algerian", 50, FontStyle.Bold);
        Font normal = new Font("Algerian", 44, FontStyle.Bold);

        private void lblStart_Click(object sender, EventArgs e)
        {
            Start();
        }
        
        private void lblStart_MouseEnter_1(object sender, EventArgs e)
        {
            //LabelSound();
            lblStart.Font = enhance;
        }

        private void lblStart_MouseLeave(object sender, EventArgs e)
        {
            lblStart.Font = normal;
        }

        private void lblScores_MouseEnter(object sender, EventArgs e)
        {
            //LabelSound();
            lblScores.Font = enhance;
        }

        private void lblScores_MouseLeave(object sender, EventArgs e)
        {
            lblScores.Font = normal;
        }

        private void lblHelp_MouseEnter(object sender, EventArgs e)
        {
            //LabelSound();
            lblHelp.Font = enhance;
        }

        private void lblHelp_MouseLeave(object sender, EventArgs e)
        {
            lblHelp.Font = normal;
        }

        private void lblAbout_MouseEnter(object sender, EventArgs e)
        {
            //LabelSound();
            lblAbout.Font = enhance;
        }

        private void lblAbout_MouseLeave(object sender, EventArgs e)
        {
            lblAbout.Font = normal;
        }
        

        private void lblScores_Click(object sender, EventArgs e)
        {
            Scores scores = new Scores();
            scores.ShowDialog();
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("  Skupite što više bodova u 60 sekundi.\n  Svaki promašaj oduzima bodove.\n  Udara se lijevim ili desnim klikom miša.\n  Za prekid trenutne igre pritisnite Escape.",
                "Help!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Question
                );
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("  Naziv: Whack - a - Mouse!\n  Izradio: Domagoj Puljić\n  Datum izrade: 15.02.2016.",
                "About!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk //ono slovo i, kao info
                );
        }


        private void label1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblExit_MouseEnter(object sender, EventArgs e)
        {
            //LabelSound();
            lblExit.Font = new Font("Arial Rounded MT Bold", 28);
        }

        private void lblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.Font = new Font("Arial Rounded MT Bold", 24);
        }
        #endregion
    }
}
 
