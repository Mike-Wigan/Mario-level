using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace final_project_start
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        Stopwatch movewatch = new Stopwatch();
        Stopwatch jumpwatch = new Stopwatch();

        Rectangle mariorec = new Rectangle(0, 440, 96, 80);
        Rectangle groundrec = new Rectangle(0, 520, 39, 60);
        Rectangle bighillrec = new Rectangle(500, 420, 300, 100);
        Rectangle smallhillrec = new Rectangle(1200, 470, 152, 50);
        Rectangle bush1rec = new Rectangle(1600, 470, 110, 50);
        Rectangle bush2rec = new Rectangle(2400, 470, 150, 50);
        Rectangle bush3rec = new Rectangle(1000, 470, 208, 50);
        Rectangle cloud1rec = new Rectangle(1000, 80, 110, 50);
        Rectangle cloud2rec = new Rectangle(1000, 100, 150, 50);
        Rectangle cloud3rec = new Rectangle(800, 110, 208, 70);
        Rectangle piperec = new Rectangle(1850, 420, 98, 113);
        Rectangle medpiperec = new Rectangle(2200, 350, 98, 178);
        Rectangle bigpiperec = new Rectangle(2600, 302, 98, 251);

        Image groundimage = Properties.Resources.groundblock;
        Image marioImage = Properties.Resources.mario_still_removebg_preview;
        Image bighill = Properties.Resources.green_thing_;
        Image smallhill = Properties.Resources.small_hill_removebg_preview;
        Image bush1 = Properties.Resources._1_light_bush;
        Image bush2 = Properties.Resources._2_light_bush;
        Image bush3 = Properties.Resources.light_green_thing;
        Image smallpipe = Properties.Resources.pipe_removebg_preview;
        Image medpipe = Properties.Resources.medpipe_removebg_preview;
        Image bigpipe = Properties.Resources.bigpipe_removebg_preview;
        Image cloud1 = Properties.Resources.smallcloud_removebg_preview;
        Image cloud2 = Properties.Resources.bigcloud_removebg_preview;
        Image cloud3 = Properties.Resources.cloud3;

        int mariospeed;
        int marioyspeed = 15;
        int test;
        int groundx;
        int round = 1;
        int start = 0;
        int medpipex = 3200;

        string began = "no";
        string direction = "right";

        bool ddown = false, hold = false;
        bool adown = false;
        bool rdown = false;
        bool spacedown = false;

        Rectangle[] floor = new Rectangle[45];
        Rectangle[] bushdark = new Rectangle[5];
        Rectangle[] bush1light = new Rectangle[4];
        Rectangle[] bush2light = new Rectangle[3];
        Rectangle[] bush3light = new Rectangle[3];
        //Rectangle[] cloud1 = new Rectangle[8];
        //Rectangle[] cloud2 = new Rectangle[8];
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < floor.Length; i++)
            {
                groundx = groundrec.Width * i;
                floor[i] = new Rectangle(groundx, groundrec.Y, groundrec.Width, groundrec.Height);
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //make map
            e.Graphics.DrawImage(bighill, bighillrec);
            e.Graphics.DrawImage(smallhill, smallhillrec);
            e.Graphics.DrawImage(bush1, bush1rec);
            e.Graphics.DrawImage(bush2, bush2rec);
            e.Graphics.DrawImage(bush3, bush3rec);
            e.Graphics.DrawImage(cloud1, cloud1rec);
            e.Graphics.DrawImage(cloud2, cloud2rec);
            e.Graphics.DrawImage(cloud3, cloud3rec);
            e.Graphics.DrawImage(smallpipe, piperec);
            e.Graphics.DrawImage(medpipe, medpiperec);
            e.Graphics.DrawImage(bigpipe, bigpiperec);
            e.Graphics.DrawImage(medpipe, medpipex, medpiperec.Y, medpiperec.Width, medpiperec.Height);
            if (bigpiperec.X <= 1200)
            {
                round = 2;
            }
            if (bighillrec.X <= 0 - bighillrec.Width && round == 2)
            {
                bighillrec.X = 1300;
            }
            if (smallhillrec.X <= 0 - smallhillrec.Width && round == 2)
            {
                smallhillrec.X = 2130;
            }
            if (bush3rec.X <= 0 - bush3rec.Width && round == 2)
            {
                bush3rec.X = 1900;
            }
            //if (bush3rec.X <= 0 - bush3rec.Width && round == 2)
            //{
            //    bighillrec.X = 1300;
            //}
            //if (bush3rec.X <= 0 - bush3rec.Width && round == 2)
            //{
            //    bighillrec.X = 1300;
            //}
            //if (bush3rec.X <= 0 - bush3rec.Width && round == 2)
            //{
            //    bighillrec.X = 1300;
            //}
            //draw mario
            e.Graphics.DrawImage(marioImage, mariorec);
            this.BackColor = Color.CornflowerBlue;
            //draw ground
            for (int i = 0; i < floor.Length; i++)
            {
                e.Graphics.DrawImage(groundimage, floor[i]);
            }

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    ddown = true;
                    break;
                case Keys.A:
                    adown = true;
                    break;
                case Keys.R:
                    rdown = true;
                    break;
                case Keys.Space:
                    jumpwatch.Start();
                    spacedown = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    marioImage = Properties.Resources.mario_still_removebg_preview;
                    ddown = false;
                    break;
                case Keys.A:
                    marioImage = Properties.Resources.mario_still_removebg_left;
                    adown = false;
                    break;
                case Keys.R:
                    rdown = false;
                    break;
                case Keys.Space:
                    spacedown = false;
                    break;
            }
        }
        private void gametimer_Tick(object sender, EventArgs e)
        {
            //mario - right
            if (ddown == true)
            {
                began = "yes";
                direction = "right";
                if (mariorec.X >= 530)
                {
                    mariospeed = 0;
                    for (int i = 0; i < floor.Length; i++)
                    {
                        floor[i].X -= 10;
                    }
                    bighillrec.X -= 10;
                    smallhillrec.X -= 10;
                    bush1rec.X -= 10;
                    bush2rec.X -= 10;
                    bush3rec.X -= 10;
                    cloud1rec.X -= 10;
                    cloud2rec.X -= 10;
                    cloud3rec.X -= 10;
                    piperec.X -= 10;
                    medpiperec.X -= 10;
                    medpipex -= 10;
                    bigpiperec.X -= 10;
                    start -= 10;
                }
                else
                {
                    mariospeed = 10;
                }
                mariorec.X += mariospeed;
                marioImage = Properties.Resources.mario_running_2;
                movewatch.Start();
                if (movewatch.ElapsedMilliseconds >= 150)
                {
                    marioImage = Properties.Resources.mario_running_4;
                }
                if (movewatch.ElapsedMilliseconds >= 300)
                {
                    marioImage = Properties.Resources.mario_runnning_3_removebg_preview;
                }
                if (movewatch.ElapsedMilliseconds >= 450)
                {
                    marioImage = Properties.Resources.mario_running_2;
                    movewatch.Reset();
                }
            }
            //mario - left
            if (adown == true)
            {
                began = "yes";
                direction = "left";
                if (mariorec.X <= 0)
                {
                    mariospeed = 0;
                }
                else if (mariorec.X >= 530 && start != 0)
                {
                    mariospeed = 0;
                    for (int i = 0; i < floor.Length; i++)
                    {
                        floor[i].X -= -10;
                    }
                    bighillrec.X += 10;
                    smallhillrec.X += 10;
                    bush1rec.X += 10;
                    bush2rec.X += 10;
                    bush3rec.X += 10;
                    cloud1rec.X += 10;
                    cloud2rec.X += 10;
                    cloud3rec.X += 10;
                    piperec.X += 10;
                    medpiperec.X += 10;
                    bigpiperec.X += 10;
                    start += 10;
                }
                else
                {
                    mariospeed = -10;
                }
                mariorec.X += mariospeed;
                marioImage = Properties.Resources.mario_running_2_left;
                movewatch.Start();
                if (movewatch.ElapsedMilliseconds >= 150)
                {
                    marioImage = Properties.Resources.mario_running_4_left;
                }
                if (movewatch.ElapsedMilliseconds >= 300)
                {
                    marioImage = Properties.Resources.mario_runnning_3_removebg_left;
                }
                if (movewatch.ElapsedMilliseconds >= 450)
                {
                    marioImage = Properties.Resources.mario_running_2_left;
                    movewatch.Reset();
                }
            }
            //mario - jump
            if (jumpwatch.IsRunning)
            {
                began = "yes";
                marioImage = Properties.Resources.mario_jumping;
                mariorec.Y -= marioyspeed;
                if (direction == "left")
                {
                    marioImage = Properties.Resources.mario_jumping_left;
                }
                if (direction == "right")
                {
                    marioImage = Properties.Resources.mario_jumping;
                }
                if (mariorec.Y >= 435)
                {
                    marioyspeed = 0;

                    if (direction == "right")
                    {
                        marioImage = Properties.Resources.mario_still_removebg_preview;
                    }
                    else if (direction == "left")
                    {
                        marioImage = Properties.Resources.mario_still_removebg_left;
                    }
                    marioyspeed = 15;
                    mariorec.Y = 439;
                    jumpwatch.Reset();
                    jumpwatch.Stop();
                }
                if (jumpwatch.ElapsedMilliseconds >= 200)
                {
                    marioyspeed = 9;
                }
                if (jumpwatch.ElapsedMilliseconds >= 300)
                {
                    marioyspeed = 6;
                }
                if (jumpwatch.ElapsedMilliseconds >= 400)
                {
                    marioyspeed = 3;
                }
                if (jumpwatch.ElapsedMilliseconds >= 500)
                {
                    marioyspeed = 0;
                }
                if (jumpwatch.ElapsedMilliseconds >= 600)
                {
                    marioyspeed = -3;
                }
                if (jumpwatch.ElapsedMilliseconds >= 700)
                {
                    marioyspeed = -6;
                }
                if (jumpwatch.ElapsedMilliseconds >= 800)
                {
                    marioyspeed = -9;
                }
                if (jumpwatch.ElapsedMilliseconds >= 900)
                {
                    marioyspeed = -15;
                }


            }
            if (start > -10 || start <= 10 /*&& began == "yes"*/)
            {
                mariospeed = 10;
                for (int i = 0; i < floor.Length; i++)
                {
                    floor[i].X -= 0;
                }
                bighillrec.X -= 0;
                smallhillrec.X -= 0;
                bush1rec.X -= 0;
                bush2rec.X -= 0;
                bush3rec.X -= 0;
                cloud1rec.X -= 0;
                cloud2rec.X -= 0;
                cloud3rec.X -= 0;
                piperec.X -= 0;
                medpiperec.X -= 0;
                bigpiperec.X -= 0;
                start -= 0;
            }
            //replace and remove floor blocks
            if (round == 1)
            {
                for (int i = 0; i < floor.Length; i++)
                {
                    if (floor[i].X <= -100 && start != 0)
                    {
                        floor[i].X = 1539;
                    }
                    if (floor[i].X >= 1600 && start != 0)
                    {
                        floor[i].X = -30;
                    }
                }
            }
            else if (round == 2)
            {
                for (int i = 0; i < floor.Length; i++)
                {
                    if (floor[i].X <= -100 && start != 0 &&)
                    {
                        floor[i].X = 1539;
                    }
                    if (floor[i].X >= 1600 && start != 0)
                    {
                        floor[i].X = -30;
                    }
                }
            }
            //intersections


            Refresh();
        }
    }
}
