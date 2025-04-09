using Catch_The_Bee.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using Catch_The_Bee.Movement;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

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

            AddBee();
        }

        private void AddBee()
        {
            var bee = new Bee(Resources.beeUp, 100, 100,
                              new Horizontal(20, new Point(this.Width, this.Height), "right"));

            bee.Clicked += (s, e) =>
            {
                score++;
                labelScore.Text = "Score: " + score;

                this.Controls.Remove(bee.GetPictureBox());
                bees.Remove(bee);
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
            MoveBees();
        }
        private void Bee_Click(object sender, EventArgs e)
        {
            score++;
            labelScore.Text = "Score: " + score;
        }
        private void MoveBees()
        {
            var beesToRemove = new List<Bee>();

            foreach (var bee in bees)
            {
                bee.update();

                if (bee.IsOutOfBounds(this.Width, this.Height))
                {
                    timer1.Stop();
                    MessageBox.Show($"Game Over!! Failed to Catch Bee\nFinal Score: {score}\nLevel Reached: {level}");
                    beesToRemove.Add(bee);
                }
            }

            foreach (var b in beesToRemove)
            {
                this.Controls.Remove(b.GetPictureBox());
                bees.Remove(b);
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
