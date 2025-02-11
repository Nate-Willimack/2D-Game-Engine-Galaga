using System;
using System.Drawing;
using System.Windows.Forms;

namespace Galaga
{
    public class PowerUp
    {
        public PictureBox PowerUpBox { get; private set; }
        private int speed;
        private Form gameForm;

        public PowerUp(Form form, Point initialPosition)
        {
            PowerUpBox = new PictureBox
            {
                Size = new Size(30, 30),
                BackColor = Color.Green,
                Location = initialPosition
            };

            form.Controls.Add(PowerUpBox);
            gameForm = form;
            speed = 2;
        }

        public void Move()
        {
            PowerUpBox.Top += speed;
        }

        public bool CollidesWith(PictureBox target)
        {
            return PowerUpBox.Bounds.IntersectsWith(target.Bounds);
        }

        public void Destroy()
        {
            gameForm.Controls.Remove(PowerUpBox);
            PowerUpBox.Dispose();
        }
    }
}
