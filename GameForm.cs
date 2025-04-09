using Catch_The_Bee.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using Catch_The_Bee.Movement;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Linq;

namespace Catch_The_Bee
{
    public partial class GameForm : Form
    {
        int score = 0;
        int bestScore = 39;
        float time = 60.0f;
        int level = 1;

        List<Bee> bees;

        public GameForm()
        {
            InitializeComponent();
            bees = new List<Bee>();

            labelScore.Text = "Score: " + score;
            labelBestScore.Text = "Best Score: " + bestScore;
            labelTime.Text = $"Time: {time:0.0} / 60.0";
            labelLevel.Text = "Level: " + level;

            timer1.Interval = 100;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            SpawnRandomBee();
        }

        private Random rand = new Random();

        private void SpawnRandomBee()
        {
            string[] directions = { "left", "right", "up", "down" };
            string direction = directions[rand.Next(directions.Length)];

            int top = 0, left = 0;
            IMovement movement;
            Image beeImg;

            switch (direction)
            {
                case "left":
                    top = rand.Next(123, this.Height - 50); 
                    left = 0;
                    movement = new Horizontal(15, new Point(this.Width, this.Height), "right");
                    beeImg = Resources.beeUp;
                    break;

                case "right":
                    top = rand.Next(123, this.Height - 50); // Avoid header
                    left = this.Width - 15;
                    movement = new Horizontal(15, new Point(this.Width, this.Height), "left");
                    beeImg = Resources.beeUp;
                    break;

                case "up":
                    left = rand.Next(0, this.Width - 50);
                    top = 123;
                    movement = new Vertical(15, new Point(this.Width, this.Height), "down");
                    beeImg = Resources.beeUp;
                    break;

                case "down":
                    left = rand.Next(0, this.Width - 50);
                    top = this.Height - 50;
                    movement = new Vertical(15, new Point(this.Width, this.Height), "up");
                    beeImg = Resources.beeUp;
                    break;

                default:
                    return;
            }

            AddBee(top, left, movement, beeImg);
        }

        private void AddBee(int top, int left, IMovement controller, Image beeImg)
        {
            var bee = new Bee(beeImg, top, left, controller);

            bee.Clicked += (s, e) =>
            {
                score++;
                labelScore.Text = "Score: " + score;

                this.Controls.Remove(bee.GetPictureBox());
                bees.Remove(bee);

                SpawnRandomBee();
            };

            bees.Add(bee);
            this.Controls.Add(bee.GetPictureBox());
            bee.GetPictureBox().BringToFront();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time -= 0.1f;
            if (time <= 0)
            {
                time = 0;
                EndGame("Time's up!");
            }
            labelTime.Text = $"Time: {time:0.0} / 60.0";
            if (score >= 30)
            {
                EndGame("Level 1 Completed!");
            }
            MoveBees();
        }
        private void Bee_Click(object sender, EventArgs e)
        {
            score++;
            labelScore.Text = "Score: " + score;
        }
        private void MoveBees()
        {
            List<Bee> beesToRemove = new List<Bee>();

            foreach (var bee in bees.ToList()) // ToList() creates a snapshot of the list for safe iteration
            {
                bee.update();
                var pic = bee.GetPictureBox();

                // If the bee is outside the form or in the header
                if (pic.Top < 115 || pic.Top > this.Height+15 || pic.Left < 0 || pic.Left > this.Width + 15)
                {
                    this.Controls.Remove(pic);
                    beesToRemove.Add(bee); // Collect bees to remove
                }
            }

            // Remove bees after iteration
            foreach (var b in beesToRemove)
            {
                bees.Remove(b); // Remove bee from original list
                                // Optionally, respawn another bee or add penalty logic
                SpawnRandomBee();
            }
        }

        private void EndGame(string message)
        {
            timer1.Stop();
            MessageBox.Show(message + $"\nFinal Score: {score}\nLevel Reached: {level}");
            this.Close();
        }
    }
}
