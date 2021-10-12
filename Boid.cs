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
        public int index;

        public Boid(float _x, float _y)
        {
            position.X = _x;
            position.Y = _y;
        }

        public void CalculatePosition(float _x, float _y)
        {
            position += new Vector2(_x, _y);
        }
    }
}
