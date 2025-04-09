using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catch_The_Bee.Movement
{
    public class Horizontal : IMovement
    {
        private int speed;
        private System.Drawing.Point boundary;
        private string direction;
        public Horizontal(int speed, System.Drawing.Point boundary, string direction)
        {
            this.speed = speed;
            this.boundary = boundary;
            this.direction = direction;
        }
        public System.Drawing.Point move(System.Drawing.Point location)
        {
            if (direction == "left")
            {
                location.X -= speed;
            }
            else
            {
                location.X += speed;
            }
            return location;
        }

    }
}
