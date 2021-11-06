using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Niculica_Bogdan_3132b_Lab3
{
    public class Coord3D
    {
        private double x, y, z;
        private float[] color;
        public Coord3D(double _x, double _y, double _z)
        {
            x = _x; y = _y; z = _z;

            color = new float[4];
            color[0] = 1;
            color[1] = color[2] = color[3] = 0.5f;
        }
        public double getX()
        {
            return x;
        }
        public double getY()
        {
            return y;
        }
        public double getZ()
        {
            return z;
        }
        public float getR()
        {
            return color[1];
        }
        public float getG()
        {
            return color[2];
        }
        public float getB()
        {
            return color[3];
        }
        public void changeR(float offset)
        {
            if(offset<1)
            color[1] += offset;
        }
        public void changeG(float offset)
        {
            if (offset < 1)
                color[2] += offset;
        }
        public void changeB(float offset)
        {
            if (offset < 1)
                color[3] += offset;
        }
        public void changeColor(float offR, float offG, float offB)
        {
            color[1] = offR;
            color[2] = offG;
            color[3] = offB;
        }
        
    }
}
