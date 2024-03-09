using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ilk_oyun
{
    public partial class Form1 : Form
    {

        bool goLeft, goRihgt, goUp, goDown, gameOver;
        string facing = "up";
        int playerSaglik = 100;
        int Speed = 10;
        int mermi = 10;
        int ZombiSpeed = 1;
        Random randNum = new Random();
        int score;
        List<PictureBox> zombiesList = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
            TekrarOyna();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }

        private void SağlıkBar_Click(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {

            if (playerSaglik > 1)
            {
                SağlıkBar.Value = playerSaglik;
            }
            else
            {
                gameOver = true;
                Player.Image = Properties.Resources.dead;
            }
            txtMermi.Text = "mermi: " + mermi;
            txtScore.Text = "kills: " + score;
            if (goLeft == true && Player.Left > 0)
            {
                Player.Left -= Speed;
            }
            if (goRihgt == true && Player.Left + Player.Width < this.ClientSize.Width)
            {
                Player.Left += Speed;
            }
            if (goUp == true && Player.Top > 37)
            {
                Player.Top -= Speed;
            }
            if (goDown == true && Player.Top + Player.Height < this.ClientSize.Height)
            {
                Player.Top += Speed;
            }
            foreach (Control x in this.Controls) 
            
            {
                if (x is PictureBox && (string)x.Tag == "mermi") 
                
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        mermi += 5;
                    }
                }
                if (x is PictureBox && (string)x.Tag == "zombie")
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerSaglik -=1 ;
                    }


                    if (x.Left > Player.Left)
                    {
                        x.Left -=ZombiSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }

                    if (x.Left < Player.Left)
                    {
                        x.Left += ZombiSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zrigh;
                    }
                    if(x.Top>Player.Top)
                    {
                        x.Top -= ZombiSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (x.Top < Player.Top)
                    {
                        x.Top += ZombiSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }

                }
                foreach(Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "kursun" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            ZombieKur();
                        }

                    }
                }
                
            }
        }

        private void KeylsDown(object sender, KeyEventArgs e)
        {
            if (gameOver == true)
            {
                return;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                Player.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRihgt = true;
                facing = "rihgt";
                Player.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                Player.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                Player.Image = Properties.Resources.down;
            }
            
        }

        private void KeylsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;

            }

            if (e.KeyCode == Keys.Right)
            {
                goRihgt = false;

            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;


            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;

            }
            if (e.KeyCode == Keys.Space && mermi > 0&& gameOver==false) 
            {
                mermi--;
                ShootKursun(facing);
                if(mermi<1) 
                {
                    Mermiler();
                }
            }
            if(e.KeyCode == Keys.Enter&&gameOver==true)
            {
                TekrarOyna();
            }
        }
        private void ShootKursun(string yon)
        {
            kursun ShotKursun = new kursun();
            ShotKursun.yon =yon;
            ShotKursun.kursunLeft = Player.Left + (Player.Width / 2);
            ShotKursun.kursunTop=Player.Top+ (Player.Height / 2);
            ShotKursun.KursunYap(this);
        }
        private void ZombieKur()
        {
            PictureBox zombie= new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = randNum.Next(0, 900);
            zombie.Top = randNum.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            Player.BringToFront();
        }
        private void Mermiler()
        {
            PictureBox mermi= new PictureBox();
            mermi.Image = Properties.Resources.mermi;
            mermi.SizeMode = PictureBoxSizeMode.AutoSize;
            mermi.Left= randNum.Next(10,this.ClientSize.Width - mermi.Width);
            mermi.Top= randNum.Next(60,this.ClientSize.Height - mermi.Height);
            mermi.Tag = "mermi";
            this.Controls.Add(mermi);
            mermi.BringToFront();
            Player.BringToFront();
        }
        private void TekrarOyna()
        {
            Player.Image = Properties.Resources.up;
            foreach(PictureBox i in zombiesList)
            {
                this.Controls.Remove(i);

            }
            zombiesList.Clear();
            for(int i=0;i<3;i++)
            {
                ZombieKur();
            }
            goUp = false;
            goDown = false;
            goLeft = false;
            goRihgt = false;
            gameOver = false;
            playerSaglik=(100);
            score = 0;
            mermi = 10;
            GamerTimer.Start(); 

        }
    }

}

    

