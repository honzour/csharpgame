using System.Drawing;
using System.Windows.Forms;

namespace panacci
{
    class Panacek : Predek
    {

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
        public override bool next()
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

            return true;
        }


        public override void draw(PictureBox pictureBox, Graphics g)
        {
            int penWidth = 10;
            Pen p = new Pen(c, penWidth);
            int sz = getScreenSize(pictureBox);
            int length = sz / 24;
 
            int ix = (int)(x * pictureBox.Size.Width);
            int iy = (int)(y * pictureBox.Size.Height);

            // hlava
            g.DrawEllipse(p, new Rectangle(ix, iy, length, length));
            // tělo
            g.DrawEllipse(p, new Rectangle(ix, iy + length, length, 2 * length));
            // nohy
            g.DrawLine(p, new Point(ix + length / 2, iy + 3 * length), new Point(ix, iy + 5 * length));
            g.DrawLine(p, new Point(ix + length / 2, iy + 3 * length), new Point(ix + length, iy + 5 * length));


            w = length / (double)pictureBox.Size.Width;
            h = 5 * length / (double)pictureBox.Size.Height;
        }
    }
}
