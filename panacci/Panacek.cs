using System.Drawing;
using System.Windows.Forms;

namespace panacci
{
    class Panacek
    {
        public double x;
        public double y;
        public double dx;
        public double dy;
        public Color c;
        public double casStrely = 0;

        public bool up;
        public int pressX;

        public Panacek(bool levy)
        {
            if (levy)
            {
                c = Color.White;
                x = 0.333;
            }
            else
            {
                c = Color.Yellow;
                x = 0.666;
            }
            y = 0.1;
            dx = 0;
            dy = 0;

            up = false;
            pressX = 0;
        }
        public void next()
        {
            if (pressX > 0)
            {
                dx = 0.01;
            }
            else
            if (pressX < 0)
            {
                dx = -0.01;
            }
            else
            {
                dx = 0;
            }
            if (up && y > 0.79)
            {
                dy = -0.03;
            }
            x += dx;
            y += dy;
            dy += 0.001;
            if (y > 0.8)
            {
                y = 0.8;
                dy = 0;
            }
            if (y < 0.1)
            {
                y = 0.1;
                dy = 0;
            }
            if (x > 0.9)
            {
                x = 0.9;
            }
            if (x < 0.1)
            {
                x = 0.1;
            }
        }


        public void draw(PictureBox pictureBox, Graphics g)
        {
            Pen p = new Pen(c, 10);
            int sz = pictureBox.Size.Width < pictureBox.Size.Height ? pictureBox.Size.Width : pictureBox.Size.Height;
            if (sz < 100)
            {
                sz = 100;
            }
            sz /= 2;
            int ix = (int)(x * pictureBox.Size.Width);
            int iy = (int)(y * pictureBox.Size.Height);

            g.DrawEllipse(p, new Rectangle(ix, iy - sz / 12, sz / 12, sz / 12));
            g.DrawEllipse(p, new Rectangle(ix, iy, sz / 12, sz / 6));
            g.DrawLine(p, new Point(ix + sz / 24, iy + sz / 6), new Point(ix, iy + sz / 6 + sz / 6));
            g.DrawLine(p, new Point(ix + sz / 24, iy + sz / 6), new Point(ix + sz / 12, iy + sz / 6 + sz / 6));
        }
    }
}
