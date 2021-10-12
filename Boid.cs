using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Boids
{
    public class Boid
    {
        public Vector2 position;
        public Vector2 velocity;
        public int index;
        private const int max_velocity = 10;

        public Boid(float _x, float _y)
        {
            position.X = _x;
            position.Y = _y;
            velocity.X = 0;
            velocity.Y = 0;
        }

        public double GetDistance(Boid _other_boid)
        {
            double dX = _other_boid.position.X - position.X;
            double dY = _other_boid.position.Y - position.Y;
            double dist = Math.Sqrt(dX * dX + dY * dY);
            return dist;
        }

        public void Move(float _min_speed = 1, float _max_speed = 5)
        {
            position.X += velocity.X;
            position.Y += velocity.Y;

            float speed = GetSpeed();
            if (speed > _max_speed)
            {
                velocity.X = (velocity.X / speed) * _max_speed;
                velocity.Y = (velocity.Y / speed) * _max_speed;
            }
            else if (speed < _min_speed)
            {
                velocity.X = (velocity.X / speed) * _min_speed;
                velocity.Y = (velocity.Y / speed) * _min_speed;
            }

            if (double.IsNaN(velocity.X))
                velocity.X = 0;
            if (double.IsNaN(velocity.Y))
                velocity.Y = 0;
        }

        public float GetSpeed()
        {
            return (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
        }
    }
}
