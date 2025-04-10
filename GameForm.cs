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

        Label labelScore;
        Label labelBestScore;
        Label labelTime;
        Label labelLevel;

        int score = 0;
        int totalScore = 0;

        int bestScore = 0;
        float time = 60.0f;
        int level = 6;

        List<Bee> bees;
        private Random rand = new Random();
        public GameForm()
        {
            InitializeComponent();
            bees = new List<Bee>();

            createHeader();
            createCrossButton();
            SpawnRandomBee();

            timer1.Interval = 100;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void createHeader()
        {
            PictureBox header = new PictureBox();
            header.Width = this.ClientSize.Width;
            header.Height = 123;
            header.Top = 0;
            header.Left = 0;
            header.Image = Resources.header;
            header.SizeMode = PictureBoxSizeMode.StretchImage;
            header.BackColor = Color.Transparent;

            this.Controls.Add(header);

            Font labelFont = new Font("Arial Black", 19.8f, FontStyle.Bold);

            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Font = labelFont;
            labelScore.ForeColor = Color.White;
            labelScore.BackColor = Color.Transparent;
            labelScore.Left = 77;
            labelScore.Top = 44;
            labelScore.AutoSize = true;

            labelBestScore = new Label();
            labelBestScore.Text = "Best Score: 0";
            labelBestScore.Font = labelFont;
            labelBestScore.ForeColor = Color.White;
            labelBestScore.BackColor = Color.Transparent;
            labelBestScore.Left = 450;
            labelBestScore.Top = 44;
            labelBestScore.AutoSize = true;

            labelTime = new Label();
            labelTime.Text = "Time: 0.0 / 60.0";
            labelTime.Font = labelFont;
            labelTime.ForeColor = Color.White;
            labelTime.BackColor = Color.Transparent;
            labelTime.Left = 980;
            labelTime.Top = 44;
            labelTime.AutoSize = true;

            labelLevel = new Label();
            labelLevel.Text = "Level: 1";
            labelLevel.Font = labelFont;
            labelLevel.ForeColor = Color.White;
            labelLevel.BackColor = Color.Transparent;
            labelLevel.Left = 1640;
            labelLevel.Top = 44;
            labelLevel.AutoSize = true;

            this.Controls.Add(labelScore);
            this.Controls.Add(labelBestScore);
            this.Controls.Add(labelTime);
            this.Controls.Add(labelLevel);

            header.BringToFront();
            labelScore.BringToFront();
            labelBestScore.BringToFront();
            labelTime.BringToFront();
            labelLevel.BringToFront();

        }

        private void createCrossButton()
        {
            Button buttonCross = new Button();
            buttonCross.Size = new Size(79, 35);
            buttonCross.Location = new Point(this.ClientSize.Width - buttonCross.Width, 0);
            buttonCross.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCross.Text = "X";
            buttonCross.Font = new Font("Arial", 11, FontStyle.Bold);
            buttonCross.BackColor = Color.Red;
            buttonCross.ForeColor = Color.Green;
            buttonCross.FlatStyle = FlatStyle.Flat;
            buttonCross.FlatAppearance.BorderSize = 0;
            buttonCross.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show(
                "Are you sure you want to exit the application?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };

            this.Controls.Add(buttonCross);
        }

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
                        movement = new Horizontal(level * 12, new Point(this.Width, this.Height), "right");
                        beeImg = Resources.beeUp;
                        break;

                    case "right":
                        top = rand.Next(123, this.Height - 50);
                        left = this.Width - 15;
                        movement = new Horizontal(level * 12, new Point(this.Width, this.Height), "left");
                        beeImg = Resources.beeUp;
                        break;

                    case "up":
                        left = rand.Next(0, this.Width - 50);
                        top = 123;
                        movement = new Vertical(level * 12, new Point(this.Width, this.Height), "down");
                        beeImg = Resources.beeUp;
                        break;

                    case "down":
                        left = rand.Next(0, this.Width - 50);
                        top = this.Height - 50;
                        movement = new Vertical(level * 12, new Point(this.Width, this.Height), "up");
                        beeImg = Resources.beeUp;
                        break;

                    default:
                        return;
                }
            
            if(level >= 6)
                movement = new RandomMovement((level - 5) * 12, new Point(this.Width, this.Height));

            AddBee(top, left, movement, beeImg);
        }

        private void AddBee(int top, int left, IMovement controller, Image beeImg)
        {
            var bee = new Bee(beeImg, top, left, controller);

            bee.Clicked += (s, e) =>
            {
                score += 3;
                labelScore.Text = "Score: " + score;

                this.Controls.Remove(bee.GetPictureBox());
                bees.Remove(bee);

                SpawnRandomBee();
            };

            bees.Add(bee);
            this.Controls.Add(bee.GetPictureBox());
            bee.GetPictureBox().SendToBack();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time -= 0.1f;
            if (time <= 0)
            {
                time = 0;
                totalScore += score;
                EndGame("Time's up!");
            }
            labelTime.Text = $"Time: {time:0.0} / 60.0";

            if (level >= 6)
            {
                while (bees.Count < 3)
                {
                    SpawnRandomBee();
                }
            }

            if(level >= 10)
            {
                totalScore += score;
                EndGame("Congratulations! Game Ended");
            }


            if (score >= 30)
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show(
                    $"Congratulations! Level {level} Completed. Press OK to start Level {level + 1}",
                    "Level Completion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2
                );

                if (result == DialogResult.Yes)
                {
                    level++;
                    totalScore += score;
                    score = 0;
                    time = 60.0f;
                    labelScore.Text = "Score: " + score;
                    labelLevel.Text = "Level: " + level;
                    foreach (var bee in bees)
                    {
                        this.Controls.Remove(bee.GetPictureBox());
                    }
                    bees.Clear();
                    timer1.Start();
                }
                else
                {
                    this.Hide();
                    HomePage homePage = new HomePage();
                    homePage.ShowDialog();
                }
            }
            MoveBees();
        }

        private void MoveBees()
        {
            List<Bee> beesToRemove = new List<Bee>();

            foreach (var bee in bees.ToList())
            {
                bee.update();
                var pic = bee.GetPictureBox();

                if (pic.Top < 1 || pic.Top > this.Height + 15 || pic.Left < -125 || pic.Left > this.Width + 15)
                {
                    this.Controls.Remove(pic);
                    beesToRemove.Add(bee);
                }
            }

            foreach (var b in beesToRemove)
            {
                bees.Remove(b);
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
