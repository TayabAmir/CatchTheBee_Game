using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catch_The_Bee
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
            this.Text = "Garden: Catch Bees";

            Label title = new Label();
            title.Text = "Garden: Catch Bees";
            title.Font = new Font("Comic Sans MS", 36, FontStyle.Bold);
            title.ForeColor = Color.Teal;
            title.BackColor = Color.Transparent;
            title.AutoSize = true;
            title.Location = new Point(80, 30);
            this.Controls.Add(title);

            Button startButton = new Button();
            startButton.Text = "Start";
            startButton.Font = new Font("Arial", 16, FontStyle.Bold);
            startButton.Size = new Size(150, 50);
            startButton.Location = new Point(100, 175);
            startButton.BackColor = Color.FromArgb(200, 255, 255, 150);
            startButton.Cursor = Cursors.Hand;
            startButton.Click += new EventHandler((sender, e) =>
            {
                this.Hide();
                GameForm gameForm = new GameForm();
                gameForm.ShowDialog();
                this.Close();
            });
            this.Controls.Add(startButton);

        }
    }
}
