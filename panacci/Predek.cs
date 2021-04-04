using System.Drawing;
using System.Windows.Forms;

namespace panacci
{
    abstract class Predek
    {
        public double x = 0;
        public double y = 0;
        public double dx = 0;
        public double dy = 0;
        public double w = 0;
        public double h = 0;

        public abstract void draw(PictureBox pictureBox, Graphics g);
        public abstract bool next();
        public int getScreenSize(PictureBox pictureBox)
        {
            int sz = pictureBox.Size.Width < pictureBox.Size.Height ? pictureBox.Size.Width : pictureBox.Size.Height;
            return sz;
        }

        public bool intersect(Predek p)
        {
            if (x >= p.x + p.w) return false;
            if (y >= p.y + p.h) return false;
            if (p.x >= x + w) return false;
            if (p.y >= y + h) return false;
            return true;
        }
    }
}
