using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Catch_The_Bee
{
    public partial class GameForm : Form
    {
        int score = 0;
        int bestScore = 39;
        float time = 10;
        float maxTime = 60.0f;

        public GameForm()
        {
            InitializeComponent();

            timer1.Tick += timer1_Tick;

            timer1.Interval = 100; 
            timer1.Enabled = true;
            timer1.Start();

            labelScore.Text = "Score: " + score.ToString();
            labelBestScore.Text = "Best Score: " + bestScore.ToString();
            labelTime.Text = $"Time: {time:0.0} / {maxTime:0.0}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time -= 0.1f;

            if (time <= 0.0f)
            {
                time = 0.0f;
                timer1.Stop();
                MessageBox.Show("Time's up!");
            }

            labelTime.Text = $"Time: {time:0.0} / {maxTime:0.0}";
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
