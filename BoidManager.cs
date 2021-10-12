using System;
using System.Collections.Generic;

namespace Boids
{
    public class BoidManager
    {
        public List<Boid> boids = new List<Boid>();
        private (int, int) window_size;
        private const int max_velocity = 10;
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
                boid.CalculatePosition(random.Next(-max_velocity, max_velocity), random.Next(-max_velocity, max_velocity));
            }
        }
    }
}
