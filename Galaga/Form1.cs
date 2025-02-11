using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Galaga
{
    public partial class Form1 : Form
    {
        private Player player;
        private List<Bullet> bullets = new List<Bullet>();
        private List<Enemy> enemies = new List<Enemy>();
        private System.Windows.Forms.Timer gameTimer;

        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            player = new Player(this, new Point(ClientSize.Width / 2 - 25, ClientSize.Height - 60));

            gameTimer = new System.Windows.Forms.Timer { Interval = 20 };
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            this.Paint += DrawGame;
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;

            SpawnEnemies();
        }

        private void UpdateScreen(object? sender, EventArgs e)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].MoveUp();
                if (bullets[i].IsOffScreen())
                {
                    bullets[i].Destroy();
                    bullets.RemoveAt(i);
                }
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Move();
                if (enemies[i].EnemyBox.Bottom >= ClientSize.Height)
                {
                    enemies[i].Destroy();
                    enemies.RemoveAt(i);
                }
            }

            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (bullets[i].BulletBox.Bounds.IntersectsWith(enemies[j].EnemyBox.Bounds))
                    {
                        bullets[i].Destroy();
                        enemies[j].Destroy();
                        bullets.RemoveAt(i);
                        enemies.RemoveAt(j);
                        break;
                    }
                }
            }

            foreach (var enemy in enemies)
            {
                if (player.PlayerBox.Bounds.IntersectsWith(enemy.EnemyBox.Bounds))
                {
                    GameOver();
                    return;
                }
            }

            Invalidate();
        }

        private void DrawGame(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var bullet in bullets)
                g.FillRectangle(Brushes.Yellow, bullet.BulletBox.Bounds);

            foreach (var enemy in enemies)
                g.FillRectangle(Brushes.Red, enemy.EnemyBox.Bounds);

            g.FillRectangle(Brushes.Blue, player.PlayerBox.Bounds);
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                player.MoveLeft();
            else if (e.KeyCode == Keys.Right)
                player.MoveRight();
            else if (e.KeyCode == Keys.Space)
                bullets.Add(new Bullet(this, new Point(player.PlayerBox.Location.X + 22, player.PlayerBox.Location.Y)));
        }

        // **Fixed OnKeyUp method**
        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
           
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < 5; i++)
            {
                int xPos = random.Next(0, ClientSize.Width - 40);
                enemies.Add(new Enemy(this, new Point(xPos, random.Next(-200, -50))));
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show("Game Over! You lost!", "Game Over", MessageBoxButtons.OK);
            Application.Exit();
        }
    }
}
