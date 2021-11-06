using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using Niculica_Bogdan_3132b_Lab3;
using System.IO;

namespace Niculica_Bogdan_3132b_Lab4
{
    class Cube
    {
        //Coord3D[] vertecsi;
        Coord3D[] vertecsi; // luat din lab3
        public Cube(string numeFisier)
        {
            
            vertecsi = new Coord3D[24];
            
            using (StreamReader fis = new StreamReader(numeFisier))
            {
                string[] buff = new string[20];
                for(int i=0;i<24;i++)
                {
                    buff = fis.ReadLine().Replace("f","").Replace(",", "").Split(' ');
                    vertecsi[i] = new Coord3D(float.Parse(buff[0], System.Globalization.CultureInfo.InvariantCulture), float.Parse(buff[1], System.Globalization.CultureInfo.InvariantCulture), float.Parse(buff[2], System.Globalization.CultureInfo.InvariantCulture));
            
                }


            }
        }
        public void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);
            for(int i=0;i<24;i++)
            {
                GL.Color3(vertecsi[i].getR(), vertecsi[i].getG(), vertecsi[i].getB());
                GL.Vertex3(vertecsi[i].getX(), vertecsi[i].getY(), vertecsi[i].getZ());
            }
            /*
            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.Cyan);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            */
          //  GL.End();
        }

    }
}
