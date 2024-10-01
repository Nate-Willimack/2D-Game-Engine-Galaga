using System.Drawing;
using System.Windows.Forms;

namespace SpaceShooter
{
    public class Enemy
    {
        public PictureBox EnemyBox { get; private set; }
        private int speed;

        public Enemy(Form form, Point initialPosition)
        {
            EnemyBox = new PictureBox
            {
                Size = new Size(40, 40),
                BackColor = Color.Red,
                Location = initialPosition
            };

            form.Controls.Add(EnemyBox);
            speed = 5;
        }

        public void MoveDown()
        {
            EnemyBox.Top += speed;
        }

        public bool IsOffScreen(int formHeight)
        {
            return EnemyBox.Bottom > formHeight;
        }
    }
}
