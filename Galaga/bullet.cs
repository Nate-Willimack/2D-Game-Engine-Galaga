using System;
using System.Drawing;
using System.Windows.Forms;

namespace Galaga
{
    public class Bullet
    {
        public PictureBox BulletBox { get; private set; }
        private int speed;
        private Form gameForm;

        public Bullet(Form form, Point initialPosition)
        {
            BulletBox = new PictureBox
            {
                Size = new Size(5, 20),
                BackColor = Color.Yellow,
                Location = initialPosition
            };

            form.Controls.Add(BulletBox);
            gameForm = form;
            speed = 10;
        }

        public void MoveUp()
        {
            BulletBox.Top -= speed;
        }

        public bool IsOffScreen()
        {
            return BulletBox.Bottom < 0;
        }

        public void Destroy()
        {
            gameForm.Controls.Remove(BulletBox);
            BulletBox.Dispose();
        }
    }
}
