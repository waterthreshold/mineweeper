using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        Random r = new Random();
        List<int> x = new List<int>();
        List<int> y = new List<int>();
        int[,] a = new int[17, 17];
        bool[,] visible = new bool[17, 17];
        bool[,] delei = new bool[17, 17];
        PictureBox[,] pic = new PictureBox[17, 17];
        Bitmap none = new Bitmap(30, 30);
        Bitmap but = new Bitmap(30, 30);
        Bitmap[] num = new Bitmap[10];
        Bitmap boom = new Bitmap(@"F:\SPower\Projects\MineSweeper\MineSweeper\5a8dccb220de5c6775c873ead6ff2e43.jpg");
        private void Form1_Load(object sender, EventArgs e)
        {
            drawbitmap();

            for (int i = 1; i <= 15; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    pic[i, j] = new PictureBox();
                    tableLayoutPanel1.Controls.Add(pic[i, j]);
                    this.pic[i, j].MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_mouseclick);
                    visible[i, j] = false;
                    delei[i, j] = false;
                }
            }
            for (int i = 1; i <= 17; i++)
            {
                randomMine();
            }
            draw_state();
        }

        private void randomMine()
        {

            int rx = r.Next(14) + 1;
            int ry = r.Next(14) + 1;
            if (x.Contains(rx) && y.Contains(ry))
            {
                randomMine();
            }
            else
            {
                delei[rx, ry] = true;
                x.Add(rx);
                y.Add(ry);
            }
        }
        private void drawbitmap()
        {
            num[0] = new Bitmap(30, 30);
            Graphics g = Graphics.FromImage(but), g1 = Graphics.FromImage(num[0]);
            g.Clear(Color.Gold);

            g1.Clear(Color.Gray);

            for (int i = 1; i <= 9; i++)
            {
                num[i] = new Bitmap(30, 30);
                Graphics shzi = Graphics.FromImage(num[i]); ;
                shzi.Clear(Color.White);
                shzi.DrawString("" + i, new Font("Arial", 16), new SolidBrush(Color.Black), new PointF(1.0F, 1.0F));
            }

        }


        private void draw_state()
        {
            for (int i = 1; i <= 15; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    if (!visible[i, j])
                    {
                        pic[i, j].BackgroundImage = but;
                    }
                    else if (visible[i, j] && !delei[i, j])
                    {

                        pic[i, j].BackgroundImage = num[a[i, j]];
                        pic[i, j].Enabled = false;
                    }
                    else if (visible[i, j] && delei[i, j])
                    {
                        pic[i, j].BackgroundImage = boom;
                    }
                }
            }
            isgameover();

        }

        private void isgameover()
        {
            bool flag = true;
            bool gameover = false;
            for (int i = 1; i <= 15; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    if (delei[i, j] && visible[i, j])
                    {
                        if (MessageBox.Show("GameOver!!", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            gameover = true;

                            Application.Restart();


                        }
                        else
                        {
                            gameover = true;
                            Application.Exit();

                        }

                    }
                    else if (!delei[i, j] && !visible[i, j])
                    {
                        flag = false;
                    }
                    if (gameover) break;
                }
                if (gameover) break;
            }
            if (flag)
            {
                if (MessageBox.Show("Win game!!", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
        private void pic_mouseclick(Object sender, MouseEventArgs e)
        {
            int tmp_x = 0; int tmp_y = 0;
            for (int i = 1; i <= 15; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    if (sender.Equals(pic[i, j]))
                    {
                        tmp_x = i; tmp_y = j;
                        break;
                    }
                }
            }
            stamp(tmp_x, tmp_y);
        }

        private void stamp(int x, int y)
        {
            if (delei[x, y])
            {
                alltrue();
            }
            if (nine(x, y) == 0)
            {
                visible[x, y] = true;
                a[x, y] = 0;
                if (x - 1 >= 1 && !visible[x - 1, y] && !delei[x - 1, y])
                {
                    stamp(x - 1, y);
                }

                if (x - 1 >= 1 && y + 1 <= 15 && !visible[x - 1, y + 1] && !delei[x - 1, y + 1])
                {
                    stamp(x - 1, y + 1);
                }

                if (x - 1 >= 1 && y - 1 >= 1 && !visible[x - 1, y - 1] && !delei[x - 1, y - 1])
                {
                    stamp(x - 1, y - 1);
                }

                if (x + 1 <= 15 && !visible[x + 1, y] && !delei[x + 1, y])
                {
                    stamp(x + 1, y);
                }

                if (x + 1 <= 15 && y + 1 <= 15 && !visible[x + 1, y + 1] && !delei[x + 1, y + 1])
                {
                    stamp(x + 1, y + 1);
                }

                if (x + 1 <= 15 && y - 1 >= 1 && !visible[x + 1, y - 1] && !delei[x + 1, y - 1])
                {
                    stamp(x + 1, y - 1);
                }

                if (y + 1 <= 15 && !visible[x, y + 1] && !delei[x, y + 1])
                {
                    stamp(x, y + 1);
                }
                if (y - 1 >= 1 && !visible[x, y - 1] && !delei[x, y - 1])
                {
                    stamp(x, y - 1);
                }


            }
            else
            {
                visible[x, y] = true;  //被翻開
                a[x, y] = nine(x, y);
            }
            draw_state();
        }

        private void alltrue()
        {
            for (int i = 1; i <= 15; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    visible[i, j] = true;
                }
            }
        }

        private int nine(int x, int y)
        {
            int sum = 0;
            if (delei[x - 1, y] && x - 1 >= 1)
            {
                sum++;
            }
            if (x + 1 <= 15 && delei[x + 1, y])
            {
                sum++;
            }
            if (x - 1 >= 1 && y + 1 <= 15 && delei[x - 1, y + 1])
            {
                sum++;
            }
            if (x - 1 >= 1 && y + 1 <= 15 && delei[x - 1, y - 1])
            {
                sum++;
            }
            if (y - 1 >= 1 && delei[x, y - 1])
            {
                sum++;
            }
            if (y + 1 <= 15 && delei[x, y + 1])
            {
                sum++;
            }
            if (x + 1 <= 15 && y - 1 >= 0 && delei[x + 1, y - 1])
            {
                sum++;
            }
            if (x + 1 <= 15 && y - 1 >= 0 && delei[x + 1, y + 1])
            {
                sum++;
            }
            return sum;
        }




    }
}
