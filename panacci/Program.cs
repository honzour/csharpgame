using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
using System.Collections.Generic;


namespace panacci
{

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
                        strely.Add(new Strela(levy.x, levy.y + 3 * levy.w, true, true));
                        levy.casStrely = ted;
                    }
                }

                if (e.KeyCode == Keys.E)
                {
                    double ted = getTime();
                    if (ted > levy.casStrely + 1000)
                    {
                        strely.Add(new Strela(levy.x, levy.y + 3 * levy.w, false, true));
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
                        strely.Add(new Strela(pravy.x, pravy.y + 3 * pravy.w, true, false));
                        pravy.casStrely = ted;
                    }
                }
                if (e.KeyCode == Keys.NumPad9)
                {
                    double ted = getTime();
                    if (ted > pravy.casStrely + 1000)
                    {
                        strely.Add(new Strela(pravy.x, pravy.y + 3 * pravy.w, false, false));
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
                if (pravy != null && s.leveho && s.intersect(pravy))
                {
                    pravy = null;
                }
                if (levy != null && !s.leveho && s.intersect(levy))
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


        private void onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(0, 0), pictureBox.Size));
            if (levy != null) levy.draw(pictureBox, g);
            if (pravy != null) pravy.draw(pictureBox, g);
            foreach (Strela s in strely)
            {
                s.draw(pictureBox, g);
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
