using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShooter
{
    public partial class Form1 : Form
    {
        PictureBox[] stars;
        int backgroundspeed;
        Random rnd;
        Timer timer;
        Player player;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            backgroundspeed = 4;
            stars = new PictureBox[50];
            rnd = new Random();
            InitializeStars();

            player = new Player(this);

            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += new EventHandler(UpdateScreen);
            timer.Start();
        }

        private void InitializeStars()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].Size = new Size(2, 2);
                stars[i].BackColor = Color.White;
                stars[i].Location = new Point(rnd.Next(this.ClientSize.Width), rnd.Next(this.ClientSize.Height));
                this.Controls.Add(stars[i]);
            }
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            foreach (PictureBox star in stars)
            {
                star.Top += backgroundspeed;

                if (star.Top > this.ClientSize.Height)
                {
                    star.Top = 0;
                    star.Left = rnd.Next(this.ClientSize.Width);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                player.MoveLeft();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                player.MoveRight(this.ClientSize.Width);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }
    }
}
