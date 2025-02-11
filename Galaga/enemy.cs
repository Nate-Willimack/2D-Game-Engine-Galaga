using System;
using System.Drawing;
using System.Windows.Forms;

namespace Galaga
{
    public class Enemy
    {
        public PictureBox EnemyBox { get; private set; }
        private int speed;
        private Form gameForm;

        public Enemy(Form form, Point initialPosition)
        {
            EnemyBox = new PictureBox
            {
                Size = new Size(40, 40),
                BackColor = Color.Red,
                Location = initialPosition
            };

            form.Controls.Add(EnemyBox);
            gameForm = form;
            speed = 3;
        }

        public void Move()
        {
            EnemyBox.Top += speed;
        }

        public bool CollidesWith(PictureBox target)
        {
            return EnemyBox.Bounds.IntersectsWith(target.Bounds);
        }

        public void Destroy()
        {
            gameForm.Controls.Remove(EnemyBox);
            EnemyBox.Dispose();
        }
    }
}
