using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catch_The_Bee.Movement
{
    public class RandomMovement : IMovement
    {
        private int speed;
        private Point boundary;
        private PointF direction;
        private Random rand = new Random();

        public RandomMovement(int speed, Point boundary)
        {
            this.speed = speed;
            this.boundary = boundary;

            // Initial random direction (normalized)
            direction = GetRandomDirection();
        }

        public Point move(Point location)
        {
            // Occasionally change direction slightly
            if (rand.NextDouble() < 0.1) // 10% chance to alter direction
            {
                direction.X += (float)(rand.NextDouble() - 0.5); // -0.5 to +0.5
                direction.Y += (float)(rand.NextDouble() - 0.5);
                NormalizeDirection();
            }

            location.X += (int)(direction.X * speed);
            location.Y += (int)(direction.Y * speed);

            return location;
        }

        private PointF GetRandomDirection()
        {
            float dx = (float)(rand.NextDouble() * 2 - 1);
            float dy = (float)(rand.NextDouble() * 2 - 1);
            var vec = new PointF(dx, dy);
            Normalize(ref vec);
            return vec;
        }

        private void NormalizeDirection()
        {
            Normalize(ref direction);
        }

        private void Normalize(ref PointF vec)
        {
            float length = (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            if (length > 0)
            {
                vec.X /= length;
                vec.Y /= length;
            }
        }
    }

}
