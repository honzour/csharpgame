using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
using System.Collections.Generic;


namespace panacci
{
    class Strela {
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
    }


    class Panacek {
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
            } else
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
    }


    class MujForm : Form
    {
        private PictureBox pictureBox;
        private System.Timers.Timer timer;
        Panacek levy = new Panacek(true);
        Panacek pravy = new Panacek(false);
        List<Strela> strely = new List<Strela>();
        public MujForm()
        {

            WindowState = FormWindowState.Maximized;

            pictureBox = new PictureBox();
            pictureBox.Size = ClientSize;
            pictureBox.Paint += new PaintEventHandler(onPaint);
            Resize += new EventHandler(Form_Resize);
            KeyDown += new KeyEventHandler(OnKeyDown);
            KeyUp += new KeyEventHandler(OnKeyUp);

            Controls.Add(pictureBox);

            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(40);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

        }

        double getTime()
        {
            DateTime dt1970 = new DateTime(1970, 1, 1);
            DateTime current = DateTime.Now;//DateTime.UtcNow for unix timestamp
            TimeSpan span = current - dt1970;
            return span.TotalMilliseconds;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && (levy == null || pravy == null))
            {
                levy = new Panacek(true);
                pravy = new Panacek(false);
                strely.Clear();
            }

            if (levy != null)
            {
                if (e.KeyCode == Keys.Q)
                {
                    double ted = getTime();
                    if (ted > levy.casStrely + 1000)
                    {
                        strely.Add(new Strela(levy.x, levy.y, true, true));
                        levy.casStrely = ted;
                    }
                }

                if (e.KeyCode == Keys.E)
                {
                    double ted = getTime();
                    if (ted > levy.casStrely + 1000)
                    {
                        strely.Add(new Strela(levy.x, levy.y, false, true));
                        levy.casStrely = ted;
                    }
                }
                if (e.KeyCode == Keys.D)
                {
                    levy.pressX = 1;
                }
                if (e.KeyCode == Keys.A)
                {
                    levy.pressX = -1;
                }
                if (e.KeyCode == Keys.W)
                {
                    levy.up = true;
                }
            }


            if (pravy != null)
            {
                if (e.KeyCode == Keys.NumPad7)
                {
                    double ted = getTime();
                    if (ted > pravy.casStrely + 1000)
                    {
                        strely.Add(new Strela(pravy.x, pravy.y, true, false));
                        pravy.casStrely = ted;
                    }
                }
                if (e.KeyCode == Keys.NumPad9)
                {
                    double ted = getTime();
                    if (ted > pravy.casStrely + 1000)
                    {
                        strely.Add(new Strela(pravy.x, pravy.y, false, false));
                        pravy.casStrely = ted;
                    }
                }

                if (e.KeyCode == Keys.NumPad6)
                {
                    pravy.pressX = 1;
                }
                if (e.KeyCode == Keys.NumPad4)
                {
                    pravy.pressX = -1;
                }
                if (e.KeyCode == Keys.NumPad8)
                {
                    pravy.up = true;
                }
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (pravy != null)
            {
                if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.NumPad4)
                {
                    pravy.pressX = 0;
                }
                if (e.KeyCode == Keys.NumPad8)
                {
                    pravy.up = false;
                }
            }


            if (levy != null)
            {
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
                {
                    levy.pressX = 0;
                }
                if (e.KeyCode == Keys.W)
                {
                    levy.up = false;
                }
            }
        }



        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Refresh();
            if (levy != null) levy.next();
            if (pravy != null) pravy.next();
            List<Strela> tmp = new List<Strela>();
            foreach (Strela s in strely)
            {
                if (s.next())
                {
                    tmp.Add(s);
                }
                if (pravy != null && s.leveho 
                    && s.x >= pravy.x && s.x < pravy.x + 1 / 24.0 
                    && s.y >= pravy.y - 1/48.0 && s.y < pravy.y + 1/12.0)
                {
                    pravy = null;
                }
                if (levy != null && !s.leveho && s.x >= levy.x && s.x < levy.x + 1 / 24.0
                    && s.y >= levy.y - 1 / 48.0 && s.y < levy.y + 1 / 12.0)
                {
                    levy = null;
                }
            }
            strely = tmp;
        }

        private void Form_Resize(object sender, System.EventArgs e)
        {
            pictureBox.Size = ClientSize;
        }

        private void drawPanacek(double x, double y, Color c, Graphics g)
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

        private void drawStrela(double x, double y, Graphics g)
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


        private void onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(0, 0), pictureBox.Size));
            if (levy != null) drawPanacek(levy.x, levy.y, levy.c, g);
            if (pravy != null) drawPanacek(pravy.x, pravy.y, pravy.c, g);
            foreach (Strela s in strely)
            {
                drawStrela(s.x, s.y, g);
            }
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Run(new MujForm());
        }
    }
}
