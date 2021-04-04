using System.Drawing;
using System.Windows.Forms;

namespace panacci
{
    class Strela
    {
        public bool leveho;
        public double x;
        public double y;
        double dx;

        public Strela(double x, double y, bool left, bool leveho)
        {
            this.x = x;
            this.y = y;
            this.leveho = leveho;
            dx = left ? -0.01 : 0.01;
        }

        public bool next()
        {
            x += dx;
            if (x > 1 || x < 0)
            {
                return false;
            }
            return true;
        }

        public void draw(PictureBox pictureBox, Graphics g)
        {
            Pen p = new Pen(Color.Red, 7);
            int sz = pictureBox.Size.Width < pictureBox.Size.Height ? pictureBox.Size.Width : pictureBox.Size.Height;
            if (sz < 100)
            {
                sz = 100;
            }
            sz /= 2;
            int ix = (int)(x * pictureBox.Size.Width);
            int iy = (int)(y * pictureBox.Size.Height);

            g.DrawLine(p, new Point(ix, iy), new Point(ix + sz / 24, iy));
        }

    }
}
