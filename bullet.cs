using System.Drawing;
using System.Windows.Forms;

namespace SpaceShooter
{
    public class Bullet
    {
        public PictureBox BulletBox { get; private set; }
        private int speed;

        public Bullet(Form form, Point initialPosition)
        {
            BulletBox = new PictureBox
            {
                Size = new Size(5, 20),
                BackColor = Color.Yellow,
                Location = initialPosition
            };

            form.Controls.Add(BulletBox);
            speed = 15;
        }

        public void MoveUp()
        {
            BulletBox.Top -= speed;
        }

        public bool IsOffScreen()
        {
            return BulletBox.Bottom < 0;
        }
    }
}
