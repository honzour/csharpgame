using System.Drawing;
using System.Windows.Forms;

namespace panacci
{
    class Strela : Predek
    {
        public bool leveho;

        public Strela(double x, double y, bool left, bool leveho)
        {
            this.x = x;
            this.y = y;
            this.leveho = leveho;
            dx = left ? -0.01 : 0.01;
        }

        public override bool next()
        {
            x += dx;
            if (x > 1 || x < 0)
            {
                return false;
            }
            return true;
        }

        public override void draw(PictureBox pictureBox, Graphics g)
        {
            
            int sz = getScreenSize(pictureBox);
           
            int ix = (int)(x * pictureBox.Size.Width);
            int iy = (int)(y * pictureBox.Size.Height);

            int length = sz / 48;
            int height = 7;
            Pen p = new Pen(Color.Red, height);

            g.DrawLine(p, new Point(ix, iy), new Point(ix + length, iy));

            w = length / (double)pictureBox.Size.Width;
            h = height / (double)pictureBox.Size.Height;
        }

    }
}
