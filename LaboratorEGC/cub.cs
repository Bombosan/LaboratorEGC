using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;



namespace LaboratorEGC
{
    class SimpleWindow3D : GameWindow
    {
        static int currentX, currentY;
        //
        float xrot = 0.0f;
        float yrot = 0.0f;
        float xdiff = 0.0f;
        float ydiff = 0.0f;
        // bool MouseApasat = false;
        //
        const float rotation_speed = 180.0f;
        float angle;
        bool showCube = true;
        KeyboardState lastKeyPress;


        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.White);

            GL.Enable(EnableCap.DepthTest);

            //retinem valorile initiale ale cursorului Mouse-ului in momentul incarcarii programului
            MouseState mouse = OpenTK.Input.Mouse.GetState();
            currentX = mouse.X;
            currentY = mouse.Y;
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();


            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
                return;
            }
            else if (keyboard[OpenTK.Input.Key.P] && !keyboard.Equals(lastKeyPress))
            {

                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }
            lastKeyPress = keyboard;


            if (mouse[OpenTK.Input.MouseButton.Left])
            {

                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            //Laborator 2 - problema 2

            MouseState mouse = OpenTK.Input.Mouse.GetState();
            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();



            if (!mouse[MouseButton.Right])
            {
                // Rotirea din mouse se realizeaza doar atunci cand click dreapta este tinut apasat; astfel cand este apasat tasta X sau Y 
                //pozitia obiectului ramane aceeasi, chiar daca pozitia cursorului din fereastra aplicatiei a fost modificata
                xdiff = mouse.X - yrot;
                ydiff = -mouse.Y + xrot;
            }


            if ((mouse.X != currentX || mouse.Y != currentY))
            {
                yrot = mouse.X - xdiff;
                xrot = mouse.Y + ydiff;
                // controlul obiectului prin 2 taste: se tine apasat tasta X sau Y impreuna cu butoanele A sau D pentru a misca pozitia cubului
                if (keyboard[Key.X])
                    if (keyboard[Key.A])
                    {
                        xrot += 1;
                        GL.Rotate(xrot, 1.0f, 0.0f, 0.0f);
                    }
                    else if (keyboard[Key.D])
                    {
                        xrot -= 1;
                        GL.Rotate(xrot, 1.0f, 0.0f, 0.0f);
                    }
                    else
                if (keyboard[Key.Y])
                        if (keyboard[Key.A])
                        {
                            yrot += 1;
                            GL.Rotate(yrot, 0.0f, 1.0f, 0.0f);
                        }
                        else if (keyboard[Key.D])
                        {
                            yrot -= 1;
                            GL.Rotate(yrot, 0.0f, 1.0f, 0.0f);
                        }
                //este realizata rotirea cubului din mouse
                GL.Rotate(xrot, 1.0f, 0.0f, 0.0f);
                GL.Rotate(yrot, 0.0f, 1.0f, 0.0f);

            }

            if (showCube == true)
            {
                DrawCube();
                DrawAxes_OLD();



            }

            SwapBuffers();

        }

        private void DrawAxes_OLD()
        {
            GL.Begin(PrimitiveType.Lines);

            // X
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(20, 0, 0);

            // Y
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 20, 0);

            // Z
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 20);


            GL.End();
        }


        public void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

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

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {


            using (SimpleWindow3D example = new SimpleWindow3D())
            {


                example.Run(30.0, 0.0);

            }
        }
    }

}
