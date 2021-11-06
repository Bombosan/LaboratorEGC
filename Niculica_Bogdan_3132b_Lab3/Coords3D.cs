using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niculica_Bogdan_3132b_Lab3
{
    class Coords3D
    {
        private float x, y, z;
        public Coords3D(int _x, int _y, int _z)
        {
            x = _x; y = _y;z = _z;
        }
        public float getX()
        {
            return x;
        }
    }
}
