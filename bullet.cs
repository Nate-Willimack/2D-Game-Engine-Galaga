using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Bullet
    {
        public PictureBox BulletBox { get; private set; }
        private int speed;
        private List<PictureBox> trail;
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
            speed = 15;
            trail = new List<PictureBox>();
        }

        public void MoveUp()
        {
            BulletBox.Top -= speed;

            AddTrail(BulletBox.Location);

            UpdateTrail();
        }

        public bool IsOffScreen()
        {
            return BulletBox.Bottom < 0;
        }

        private void AddTrail(Point position)
        {
            PictureBox trailPiece = new PictureBox
            {
                Size = new Size(3, 15),
                BackColor = Color.FromArgb(128, Color.Yellow),
                Location = new Point(position.X, position.Y + 20)
            };

            gameForm.Controls.Add(trailPiece);
            trail.Add(trailPiece);
        }

        private void UpdateTrail()
        {
            for (int i = 0; i < trail.Count; i++)
            {
                var piece = trail[i];
                piece.Top -= speed / 2;

                piece.BackColor = Color.FromArgb(Math.Max(piece.BackColor.A - 15, 0), piece.BackColor.R, piece.BackColor.G, piece.BackColor.B);

                if (piece.BackColor.A == 0)
                {
                    gameForm.Controls.Remove(piece);
                    trail.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
