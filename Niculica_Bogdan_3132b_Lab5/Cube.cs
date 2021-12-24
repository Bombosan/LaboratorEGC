//Maruneac Cosmin, 3132B

using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator5Tema
{
    
    public class Cube
    {
        private bool newVisibility;

        private const int DIM = 10;
        public Cube()
        {
            newVisibility = false;
        }

        public void Hide()
        {
            newVisibility = false;
        }

        public void Show()
        {
            newVisibility = true;
        }
        public void ToggleVisibility()
        {
            newVisibility = !newVisibility;
        }
        public void DrawCube()
        {
            if (newVisibility)
            {
                GL.Begin(PrimitiveType.Quads);

                GL.Color3(Color.Silver);
                GL.Vertex3(-1.0f*DIM, -1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, -1.0f * DIM);

                GL.Color3(Color.Silver);
                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, -1.0f * DIM) ;
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, 1.0f * DIM);

                GL.Color3(Color.Silver);

                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, 1.0f * DIM) ;
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, -1.0f * DIM);

                GL.Color3(Color.Silver);
                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, 1.0f * DIM);

                GL.Color3(Color.Silver);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, -1.0f * DIM);

                GL.Color3(Color.Silver);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, 1.0f * DIM);

                GL.End();
            }   
        }
    }
}
