using System.Drawing;
using System.Windows.Forms;

namespace SpaceShooter
{
    public class Player
    {
        public PictureBox PlayerBox { get; private set; }
        private int speed;

        public Player(Form form)
        {
            PlayerBox = new PictureBox
            {
                Size = new Size(50, 50),
                BackColor = Color.Blue,
                Location = new Point(form.ClientSize.Width / 2, form.ClientSize.Height - 60) // Start at bottom center
            };

            form.Controls.Add(PlayerBox);
            speed = 10;
        }

        public void MoveLeft()
        {
            if (PlayerBox.Left > 0)
                PlayerBox.Left -= speed;
        }

        public void MoveRight(int formWidth)
        {
            if (PlayerBox.Right < formWidth)
                PlayerBox.Left += speed;
        }
    }
}
