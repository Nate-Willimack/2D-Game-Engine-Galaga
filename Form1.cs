using System;
using System.Collections.Generic;
using System.Drawing;
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
        List<Bullet> bullets;
        List<Enemy> enemies;
        Random enemySpawnRnd;

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
            bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            enemySpawnRnd = new Random();

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
            MoveStars();
            UpdateBullets();
            UpdateEnemies();
            CheckCollisions();
            SpawnEnemy();
        }

        private void MoveStars()
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

        private void SpawnEnemy()
        {
            if (enemySpawnRnd.Next(100) < 10)
            {
                Point spawnPoint = new Point(enemySpawnRnd.Next(0, this.ClientSize.Width - 40), -40);
                enemies.Add(new Enemy(this, spawnPoint));
            }
        }

        private void UpdateEnemies()
        {
            foreach (var enemy in enemies.ToList())
            {
                enemy.MoveDown();
                if (enemy.IsOffScreen(this.ClientSize.Height))
                {
                    enemies.Remove(enemy);
                    this.Controls.Remove(enemy.EnemyBox);
                }
            }
        }

        private void UpdateBullets()
        {
            foreach (var bullet in bullets.ToList())
            {
                bullet.MoveUp();
                if (bullet.IsOffScreen())
                {
                    bullets.Remove(bullet);
                    this.Controls.Remove(bullet.BulletBox);
                }
            }
        }

        private void CheckCollisions()
        {
            foreach (var bullet in bullets.ToList())
            {
                foreach (var enemy in enemies.ToList())
                {
                    if (bullet.BulletBox.Bounds.IntersectsWith(enemy.EnemyBox.Bounds))
                    {
                        bullets.Remove(bullet);
                        enemies.Remove(enemy);
                        this.Controls.Remove(bullet.BulletBox);
                        this.Controls.Remove(enemy.EnemyBox);
                        break;
                    }
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
            else if (keyData == Keys.Space)
            {
                ShootBullet();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
s
        private void ShootBullet()
        {
            Point bulletStart = new Point(player.PlayerBox.Left + (player.PlayerBox.Width / 2) - 2, player.PlayerBox.Top - 20);
            bullets.Add(new Bullet(this, bulletStart));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
