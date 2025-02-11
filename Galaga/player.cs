using System;
using System.Drawing;
using System.Windows.Forms;

namespace Galaga
{
    public class Player
    {
        public PictureBox PlayerBox { get; private set; }
        private int speed = 5;
        private Form gameForm;

        public Player(Form form, Point startPosition)
        {
            PlayerBox = new PictureBox
            {
                Size = new Size(50, 50),
                BackColor = Color.Blue,
                Location = startPosition
            };

            form.Controls.Add(PlayerBox);
            gameForm = form;
        }

        public void MoveLeft()
        {
            if (PlayerBox.Left > 0)
                PlayerBox.Left -= speed;
        }

        public void MoveRight()
        {
            if (PlayerBox.Right < gameForm.ClientSize.Width)
                PlayerBox.Left += speed;
        }

        public bool CollidesWith(PictureBox target)
        {
            return PlayerBox.Bounds.IntersectsWith(target.Bounds);
        }

        public Point Position => PlayerBox.Location;
    }
}
