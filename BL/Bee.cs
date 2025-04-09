using Catch_The_Bee.Movement;
using System.Drawing;
using System.Windows.Forms;
using System;

public class Bee
{
    private PictureBox pictureBox;
    private IMovement controller;

    public event EventHandler Clicked;

    public Bee(Image img, int top, int left, IMovement controller)
    {
        this.pictureBox = new PictureBox();
        this.pictureBox.Image = img;
        this.pictureBox.Width = img.Width;
        this.pictureBox.Height = img.Height;
        this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        this.pictureBox.BackColor = Color.Transparent;
        this.pictureBox.Top = top;
        this.pictureBox.Left = left;
        this.controller = controller;

        this.pictureBox.Click += (s, e) => Clicked?.Invoke(this, EventArgs.Empty);
    }

    public void update()
    {
        pictureBox.Location = controller.move(pictureBox.Location);
    }

    public PictureBox GetPictureBox()
    {
        return this.pictureBox;
    }

    public bool IsOutOfBounds(int formWidth, int formHeight)
    {
        return pictureBox.Left < -pictureBox.Width ||
               pictureBox.Top < -pictureBox.Height ||
               pictureBox.Left > formWidth ||
               pictureBox.Top > formHeight;
    }
}
