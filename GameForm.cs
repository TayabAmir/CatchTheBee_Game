using Catch_The_Bee.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using Catch_The_Bee.Movement;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Linq;
using System.Media;
using NAudio.Wave;
using System.IO;

namespace Catch_The_Bee
{
    public partial class GameForm : Form
    {
        private readonly string saveFilePath = "game_data.txt";
        Label labelScore;
        Label labelBestScore;
        Label labelTime;
        Label labelLevel;
        WaveOutEvent gameMusicPlayer;
        AudioFileReader gameMusicReader;

        int score = 0;
        int totalScore = 0;

        int bestScore = 0;
        float time = 60.0f;
        int level = 1;

        List<Bee> bees;
        private Random rand = new Random();
        public GameForm()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            StartBackgroundMusic(); 
            bees = new List<Bee>();

            LoadGameData();
            createHeader();
            createCrossButton();
            SpawnRandomBee();

            timer1.Interval = 100;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void LoadGameData()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    var lines = File.ReadAllLines(saveFilePath);
                    bestScore = int.Parse(lines[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading game data: " + ex.Message);
            }
        }

        private void StartBackgroundMusic()
        {
            gameMusicReader = new AudioFileReader("Game.mp3");
            gameMusicPlayer = new WaveOutEvent();
            gameMusicPlayer.Init(gameMusicReader);
            gameMusicPlayer.Play();

            gameMusicReader.Position = 0;
            gameMusicPlayer.PlaybackStopped += (s, e) => {
                gameMusicReader.Position = 0;
                gameMusicPlayer.Play(); 
            };
        }

        private void PlayCatchBeeSound()
        {
            var catchBeeReader = new AudioFileReader("BeeClick.mp3");
            var catchBeePlayer = new WaveOutEvent();
            catchBeePlayer.Init(catchBeeReader);
            catchBeePlayer.Play();

            catchBeePlayer.PlaybackStopped += (s, e) => {
                catchBeePlayer.Dispose();
                catchBeeReader.Dispose();
            };
        }

        private void SaveGameData()
        {
            try
            {

                File.WriteAllText(saveFilePath, $"{totalScore}\n{level}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving game data: " + ex.Message);
            }
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
            labelScore.ForeColor = Color.Black;
            labelScore.BackColor = Color.Transparent;
            labelScore.Left = 77;
            labelScore.Top = 44;
            labelScore.AutoSize = true;

            labelBestScore = new Label();
            labelBestScore.Text = "Best Score: " + bestScore;
            labelBestScore.Font = labelFont;
            labelBestScore.ForeColor = Color.Black;
            labelBestScore.BackColor = Color.Transparent;
            labelBestScore.Left = 450;
            labelBestScore.Top = 44;
            labelBestScore.AutoSize = true;

            labelTime = new Label();
            labelTime.Text = "Time: 0.0 / 60.0";
            labelTime.Font = labelFont;
            labelTime.ForeColor = Color.Black;
            labelTime.BackColor = Color.Transparent;
            labelTime.Left = 850;
            labelTime.Top = 44;
            labelTime.AutoSize = true;

            labelLevel = new Label();
            labelLevel.Text = "Level: " + level;
            labelLevel.Font = labelFont;
            labelLevel.ForeColor = Color.Black;
            labelLevel.BackColor = Color.Transparent;
            labelLevel.Left = 1220;
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

        private Image GetImage(string direction)
        {
            if(level >= 6)
            {
                if (level - 5 == 1)
                    return Resources.beeUpYellow;
                if (level - 5 == 2)
                    return Resources.beeUpGreen;
                if (level - 5 == 3)
                    return Resources.beeUpRed;
                if (level - 5 == 4)
                    return  Resources.beeUpBlue;
                if (level - 5 == 5)
                    return Resources.beeUpPurple;
            }
            if(direction == "left")
            {
                if (level == 1)
                    return Resources.beeLeftYellow;
                if(level == 2)
                    return Resources.beeLeftGreen;
                if (level == 3)
                    return Resources.beeLeftRed;
                if (level == 4)
                    return Resources.beeLeftBlue;
                if (level == 5)
                    return Resources.beeLeftPurple;
            } else if(direction == "right") {
                if (level == 1)
                    return Resources.beeRightYellow;
                if (level == 2)
                    return Resources.beeRightGreen;
                if (level == 3)
                    return Resources.beeRightRed;
                if (level == 4)
                    return Resources.beeRightBlue;
                if (level == 5)
                    return Resources.beeRightPurple;
            } else if(direction == "up")
            {
                if (level == 1)
                    return Resources.beeUpYellow;
                if (level == 2)
                    return Resources.beeUpGreen;
                if (level == 3)
                    return Resources.beeUpRed;
                if (level == 4)
                    return Resources.beeUpBlue;
                if (level == 5)
                    return Resources.beeUpPurple;

            } else if(direction == "down")
            {
                if (level % 5 == 1)
                    return Resources.beeDownYellow;
                if (level % 5 == 2)
                    return Resources.beeDownGreen;
                if (level % 5 == 3)
                    return Resources.beeDownRed;
                if (level % 5 == 4)
                    return Resources.beeDownBlue;
                if (level % 5 == 0)
                    return Resources.beeDownPurple;
            }
                return null;
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
                    beeImg = GetImage("right");
                    break;

                case "right":
                    top = rand.Next(123, this.Height - 50);
                    left = this.Width - 15;
                    movement = new Horizontal(level * 12, new Point(this.Width, this.Height), "left");
                    beeImg = GetImage("left");
                    break;

                case "up":
                    left = rand.Next(0, this.Width - 50);
                    top = 123;
                    movement = new Vertical(level * 12, new Point(this.Width, this.Height), "down");
                    beeImg = GetImage("down");
                    break;

                case "down":
                    left = rand.Next(0, this.Width - 50);
                    top = this.Height - 50;
                    movement = new Vertical(level * 12, new Point(this.Width, this.Height), "up");
                    beeImg = GetImage("up");
                    break;

                default:
                    return;
            }

            if (level >= 6)
                movement = new RandomMovement((level - 5) * 12, new Point(this.Width, this.Height));

            AddBee(top, left, movement, beeImg);
        }

        private void AddBee(int top, int left, IMovement controller, Image beeImg)
        {
            var bee = new Bee(beeImg, top, left, controller);

            bee.Clicked += (s, e) =>
            {
                PlayCatchBeeSound(); 
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

            if (score >= 30)
            {
                timer1.Stop();
                gameMusicPlayer?.Pause();
                new SoundPlayer(Resources.NextLevelMusic).Play();

                if(level == 10)
                {
                    totalScore += score;
                    EndGame("You have completed all levels!");
                } else
                {
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
                        if (level <= 6)
                            SpawnRandomBee();
                        gameMusicPlayer?.Play();
                        timer1.Start();
                    }
                    else
                    {
                        timer1.Stop();
                        SaveGameData();
                        this.Hide();
                        HomePage homePage = new HomePage();
                        homePage.ShowDialog();
                    }
                }

                
            }
            MoveBees();
        }
        private void StopBackgroundMusic()
        {
            gameMusicPlayer?.Stop();
            gameMusicPlayer?.Dispose();
            gameMusicReader?.Dispose();
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
            SaveGameData();
            MessageBox.Show(message + $"\nFinal Score: {totalScore}\nLevel Reached: {level}");
            this.Close();
        }
    }
}
