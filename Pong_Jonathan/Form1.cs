using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong_Jonathan
{
    public partial class Pong : Form
    {
        public Pong()
        {
            InitializeComponent();
        }
        double dblSpeedJC = 20;
        int intAngleJC = 30;
        int intXMoveJC = 0;
        int intYMoveJC = 0;
        int intDirectionJC = 1;
        int intPlayerScore = 0;
        int intCompScore = 0;
        private void btnStart_Click(object sender, EventArgs e) //when user clicks start
        {
            //make buttons and labels disappear
            this.btnStart.Visible = false;
            this.btnInstructions.Visible = false;
            this.lblTitle.Visible = false;
            GameStarterJC();
        }
        private void GameStarterJC()
        {
            //intialize timer
            tmrGame.Enabled = true;
            //put ball back into the starting position
            pcbBall.Location = new Point(400, 300);
            //reset the speed
            dblSpeedJC = 6.0;
            //randomly select a direction
            Random rndJC = new Random();
            int intRandomJC = rndJC.Next(4);
            if (intRandomJC == 0)
            {
                intDirectionJC = 1;
            }
            else if (intRandomJC == 1)
            {
                intDirectionJC = 2;
            }
            else if (intRandomJC == 2)
            {
                intDirectionJC = 3;
            }
            else if (intRandomJC == 3)
            {
                intDirectionJC = 4;
            }
        }
        private void Form1_KeyDown_1(object sender, KeyEventArgs e) //move user paddle
        {
            if (e.KeyData == Keys.Up && this.pcbPaddle.Top > 0)
            {
                pcbPaddle.Top -= 10;
            }
            else if (e.KeyData == Keys.Down && this.pcbPaddle.Bottom + 38 < this.Height)
            {
                pcbPaddle.Top += 10;
            }
        }
        private void tmrGame_Tick_1(object sender, EventArgs e)//timer 
        {
            //set angle coordinates
            intXMoveJC = intHoriMove((int)dblSpeedJC, intAngleJC);
            intYMoveJC = intVertMove((int)dblSpeedJC, intAngleJC);
            //set direction          
            if (intDirectionJC == 1)
            {
                intXMoveJC = Math.Abs(intXMoveJC);
                intYMoveJC = Math.Abs(intYMoveJC) * -1;
            }
            else if (intDirectionJC == 2)
            {
                intXMoveJC = Math.Abs(intXMoveJC) * -1;
                intYMoveJC = Math.Abs(intYMoveJC) * -1;
            }
            else if (intDirectionJC == 3)
            {
                intXMoveJC = Math.Abs(intXMoveJC) * -1;
                intYMoveJC = Math.Abs(intYMoveJC);
            }
            else if (intDirectionJC == 4)
            {
                intXMoveJC = Math.Abs(intXMoveJC);
                intYMoveJC = Math.Abs(intYMoveJC);
            }
            //move the CPU paddle
            if (pcbPaddleCPU.Top + 50 > pcbBall.Top && pcbPaddleCPU.Top > 0)
            {
                //make game harder depending on the socre
                if (intCompScore == 3 || intPlayerScore == 3)
                {
                    pcbPaddleCPU.Top -= 6;
                }
                else
                {
                    pcbPaddleCPU.Top -= 3;
                }
            }
            else if (pcbPaddleCPU.Top - 50 < pcbBall.Top && pcbPaddleCPU.Top < this.Height)
            {
                //make game harder depending on the socre
                if (intCompScore == 3 || intPlayerScore == 3)
                {
                    pcbPaddleCPU.Top += 6;
                }
                else
                {
                    pcbPaddleCPU.Top += 3;
                }
            }
            //if ball touches the  user paddle          
            if (this.pcbPaddle.Bounds.IntersectsWith(this.pcbBall.Bounds))
            {
                //how the ball bounces            
                if ((this.pcbBall.Top > this.pcbPaddle.Top || this.pcbBall.Top < this.pcbPaddle.Top) && this.pcbBall.Top < (this.pcbPaddle.Top + this.pcbPaddle.Height * 0.25))//if ball hits the first quarter
                {
                    intDirectionJC = 1;
                    intAngleJC = 20;
                    dblSpeedJC += 0.25;//make game progressively harder by increasing speed
                }
                else if (this.pcbBall.Top > this.pcbPaddle.Top && this.pcbBall.Top < (this.pcbPaddle.Top + this.pcbPaddle.Height * 0.5))//if ball hits the second quarter 
                {
                    intDirectionJC = 1;
                    intAngleJC = 45;
                    dblSpeedJC += 0.25;//make game progressively harder by increasing speed
                }
                else if (this.pcbBall.Top > this.pcbPaddle.Top && this.pcbBall.Top < (this.pcbPaddle.Top + this.pcbPaddle.Height * 0.75))//if ball hits third quarter
                {
                    intDirectionJC = 4;
                    intAngleJC = 45;
                    dblSpeedJC += 0.25;//make game progressively harder by increasing speed
                }
                else if ((this.pcbBall.Top > this.pcbPaddle.Top || this.pcbBall.Top < this.pcbPaddle.Top) && this.pcbBall.Top < (this.pcbPaddle.Top + this.pcbPaddle.Height))//if ball hits fourth quarter
                {
                    intDirectionJC = 4;
                    intAngleJC = 20;
                    dblSpeedJC += 0.25;//make game progressively harder by increasing speed
                }
            }
            //if ball touches CPU paddle
            if (this.pcbPaddleCPU.Bounds.IntersectsWith(this.pcbBall.Bounds))
            {
                //how the ball bounces
                if (this.pcbBall.Top > this.pcbPaddleCPU.Top && this.pcbBall.Top < (this.pcbPaddleCPU.Top + this.pcbPaddleCPU.Height * 0.25))//if ball hits first quarter of paddle
                {
                    intDirectionJC = 2;
                    intAngleJC = 20;
                }
                else if (this.pcbBall.Top > this.pcbPaddleCPU.Top && this.pcbBall.Top < (this.pcbPaddleCPU.Top + this.pcbPaddleCPU.Height * 0.5)) //if ball hits the second quarter
                {
                    intDirectionJC = 2;
                    intAngleJC = 45;
                }
                else if (this.pcbBall.Top > this.pcbPaddleCPU.Top && this.pcbBall.Top < (this.pcbPaddleCPU.Top + this.pcbPaddleCPU.Height * 0.75)) //if ball hits the third quarter
                {
                    intDirectionJC = 3;
                    intAngleJC = 45;

                }
                else if (this.pcbBall.Top > this.pcbPaddleCPU.Top && this.pcbBall.Top < (this.pcbPaddleCPU.Top + this.pcbPaddleCPU.Height)) //if ball hits the fourth quarter
                {
                    intDirectionJC = 3;
                    intAngleJC = 20;
                }
            }
            //set boundaries and direction (counter clockwise)
            if (intDirectionJC == 1 && this.pcbBall.Left + pcbBall.Width >= this.Width - pcbBall.Width) //if ball hits the right side, player wins
            {
                //increment player score
                intPlayerScore++;
                this.lblPlayerScore.Text = intPlayerScore.ToString();
                dblSpeedJC = 3;
                tmrGame.Enabled = false;
                GameStarterJC();

            }
            else if (intDirectionJC == 2 && this.pcbBall.Top < 0)
            {
                intDirectionJC = 3;
            }
            else if (intDirectionJC == 3 && this.pcbBall.Left < 0) //when ball hits left side, CPU wins
            {
                //increment CPU score
                intCompScore++;
                this.lblCompScore.Text = intCompScore.ToString();
                dblSpeedJC = 3;
                tmrGame.Enabled = false;
                GameStarterJC();
            }
            else if (intDirectionJC == 4 && this.pcbBall.Top >= 570 - this.pcbBall.Height)
            {
                intDirectionJC = 1;
            }
            //set boundaries and direction (clockwise)
            if (intDirectionJC == 2 && this.pcbBall.Left < 0)//when ball hits left side, CPU wins
            {
                //increment CPU score
                intCompScore++;
                this.lblCompScore.Text = intCompScore.ToString();
                dblSpeedJC = 3;
                tmrGame.Enabled = false;
                GameStarterJC();
            }
            else if (intDirectionJC == 1 && this.pcbBall.Top < 0)
            {
                intDirectionJC = 4;
            }
            else if (intDirectionJC == 4 && this.pcbBall.Left > this.Width - pcbBall.Width) //if ball hits the right side, player wins
            {
                //increment player score
                intPlayerScore++;
                this.lblPlayerScore.Text = intPlayerScore.ToString();
                dblSpeedJC = 3;
                tmrGame.Enabled = false;
                GameStarterJC();
            }
            else if (intDirectionJC == 3 && this.pcbBall.Top >= 570 - this.pcbBall.Height)
            {
                intDirectionJC = 2;
            }
            //move ball
            this.pcbBall.Left += intXMoveJC;
            this.pcbBall.Top += intYMoveJC;           
            //if user or comp scored 10, end game
            if (intPlayerScore == 5)
            {
                tmrGame.Enabled = false;
                MessageBox.Show("Congradulations!! You won");
                //make exit and reset buttons visible
                this.btnExit.Visible = true;
                this.btnRestart.Visible = true;
            }
            else if (intCompScore==5)
            {
                tmrGame.Enabled = false;
                MessageBox.Show("Unfortunately the CPU won!!");
                //make exit and reset buttons visible
                this.btnExit.Visible = true;
                this.btnRestart.Visible = true;
            }

        }
        public int intHoriMove(int intHypJC, int intDegreeJC) //horizontal move
        {
            return (int)(intHypJC * Math.Cos((double)intDegreeJC * Math.PI / 180));
        }
        public int intVertMove(int intHypJC, int intDegreeJC) //vertical move
        {
            return (int)(intHypJC * Math.Sin((double)intDegreeJC * Math.PI / 180));
        }
        private void btnExit_Click(object sender, EventArgs e) //exit from program
        {
            Application.Exit();
        }

        private void Pong_Load(object sender, EventArgs e)//default settings for when form loads
        {
            this.Height = 600;
            this.Width = 800;
            //make exit and reset buttons invisible
            this.btnExit.Visible = false;
            this.btnRestart.Visible = false;         
        }

        private void btnRestart_Click(object sender, EventArgs e)//restart the program
        {
            Application.Restart();
        }

        private void btnInstructions_Click(object sender, EventArgs e) //if user wants instructions to the game
        {
            MessageBox.Show("Welcome to Pong!! Use the up and down arrow keys to control the green paddle. Make sure the red ball does not touch your boundary (on left side). First player to 5 wins. Good Luck!!");
        }

        private void pcbPaddle_Click(object sender, EventArgs e)
        {

        }
    }
}
