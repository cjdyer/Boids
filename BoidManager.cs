using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Boids
{
    public class BoidManager
    {
        public List<Boid> boids = new List<Boid>();
        private (int, int) window_size;
        private int vision_distance = 50;
        private Random random = new Random();

        public BoidManager(int _amount, (int,int) _window_size)
        {
            window_size = _window_size;
            for (int i = 0; i < _amount; i++)
            {
                boids.Add(new Boid(random.Next(0, window_size.Item1), random.Next(0, window_size.Item2)));
            }
        }

        public void RunCycle()
        {
            foreach (Boid boid in boids)
            {
                Vector2 flock = Flock(boid);
                Vector2 align = Align(boid);
                Vector2 avoid = Avoid(boid);
                boid.velocity += flock + align + avoid;
            }

            foreach (Boid boid in boids)
            {
                boid.Move();
                BounceOffWalls(boid);
            }
        }

        private Vector2 Flock(Boid _boid)
        {
            var neighbors = boids.Where(x => x.GetDistance(_boid) < vision_distance);
            double mean_x = neighbors.Sum(x => x.position.X) / neighbors.Count();
            double mean_y = neighbors.Sum(x => x.position.Y) / neighbors.Count();
            double delta_center_x = mean_x - _boid.position.X;
            double delta_center_y = mean_y - _boid.position.Y;
            return new Vector2((float)(delta_center_x * 0.003), (float)(delta_center_y * 0.003));
        }

        private Vector2 Align(Boid _boid)
        {
            var neighbors = boids.Where(x => x.GetDistance(_boid) < vision_distance);
            double mean_x_velocity= neighbors.Sum(x => x.velocity.X) / neighbors.Count();
            double mean_y_velocity = neighbors.Sum(x => x.velocity.Y) / neighbors.Count();
            double delta_x_velocity = mean_x_velocity - _boid.velocity.X;
            double delta_y_velocity = mean_y_velocity - _boid.velocity.Y;
            return new Vector2((float)(delta_x_velocity * 0.01), (float)(delta_y_velocity * 0.01));
        }

        private Vector2 Avoid(Boid _boid)
        {
            var neighbors = boids.Where(x => x.GetDistance(_boid) < 20);
            Vector2 sum = new Vector2(0, 0);
            foreach (var neighbor in neighbors)
            {
                double closeness = vision_distance - _boid.GetDistance(neighbor);
                sum.X += (float)((_boid.position.X - neighbor.position.X) * closeness);
                sum.Y += (float)((_boid.position.Y - neighbor.position.Y) * closeness);
            }
            sum.X *= 0.001f;
            sum.Y *= 0.001f;
            return sum;
        }

        private void BounceOffWalls(Boid _boid)
        {
            float pad = 50;
            float turn = .5f;
            if (_boid.position.X < pad)
                _boid.velocity.X += turn;
            if (_boid.position.X > window_size.Item1 - pad)
                _boid.velocity.X -= turn;
            if (_boid.position.Y < pad)
                _boid.velocity.Y += turn;
            if (_boid.position.Y > window_size.Item2 - pad)
                _boid.velocity.Y -= turn;
        }
    }
}
