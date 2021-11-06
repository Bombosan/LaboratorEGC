using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using OpenTK.Platform;
using System.IO;
//Problema 8 + 9
namespace Niculica_Bogdan_3132b_Lab3
{
    class TriunghiColor : GameWindow
    {
        static double angle = 0;
        
        float[,] vertexuri;
        const float defaultColor = 0.5f;
        int choice;
        
        

     //   static Vector3 position = new Vector3(0.0f, 0.0f, -3.0f);
      //  static Vector3 front = new Vector3(0.0f, 0.0f, 0.0f);
      //  static Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

        public TriunghiColor(string fileName) : base(800, 600,OpenTK.Graphics.GraphicsMode.Default,"Schimba culoare triunghi")
        { vertexuri = new float[3,5];
            choice = 3; // 3 = toate triunghiurile; <3 fiecare vertex in parte
            try
            {
                using (StreamReader rd = new StreamReader(fileName))
                {
                    
                    for(int i=0;i<3;i++)
                    {
                        
                        vertexuri[i, 0] = float.Parse(rd.ReadLine());
                        vertexuri[i, 1] = float.Parse(rd.ReadLine());
                        vertexuri[i, 2] = vertexuri[i, 3] = vertexuri[i, 4] = defaultColor;
                        
                    }


                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Eroare la deschiderea fisierului! " + e);
            }

            // KeyDown += Keyboard_KeyDown;

            Vector3d eye = new Vector3d(0f, 0f, -3.0f);
            Vector3d target = new Vector3d(0f, 0f, 0f);
            Vector3d up_vector = new Vector3d(0f, 1f, 0f);

        }


        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;
        }


        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.White);
            
        }


        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection); 
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            MouseState mouse = Mouse.GetCursorState();
            initialX = mouse.X;
            initialY = mouse.Y;




        }

        static double[] orientation = { 0,0,0 };
        static int initialX, initialY;
        
        void MoveCamera(int cX, int cY)
        {
            if (cX > initialX)
            {
                orientation[0] = 1;
                angle += 2;
            }
            if (cY >initialY)
            {
                orientation[1] = 1;
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {   //schimbarea culorii fiecarui vertex individual se face in felul urmator:
            //se selecteaza din keypad vertexul dorit(1 = varf, 2,3 = baza de la stanga spre dreapta)
            //pentru a reveni la modificarea intregului triunghi se mai apasa odata ultimul vertex selectat
            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();
            if (keyboard[Key.Keypad1])
            {
                choice = (choice == 0) ? 3 : 0;
                Log("Vertexul curent ales este: " + choice);
            }
            else
            if (keyboard[Key.Keypad2])
            {
                choice = (choice == 1) ? 3 : 1;
                Log("Vertexul curent ales este: " + choice);
            }
            else
            if (keyboard[Key.Keypad3])
            {
                choice = (choice == 2) ? 3 : 2;
                Log("Vertexul curent ales este: " + choice);
            }
            //cum as putea face sa limitez valorile maxime/minime la -1 respectiv +1?
            //Rezolvare problema 8+9;
            if (keyboard[Key.Left] && keyboard[Key.R])
                if (choice != 3)
                    vertexuri[choice, 2] -= 0.05f;
                else
                {
                    vertexuri[0, 2] -= 0.05f;
                    vertexuri[1, 2] -= 0.05f;
                    vertexuri[2, 2] -= 0.05f;
                }
            if (keyboard[Key.Left] && keyboard[Key.G])
                if (choice != 3)
                    vertexuri[choice, 3] -= 0.05f;
                else
                {
                    vertexuri[0, 3] -= 0.05f;
                    vertexuri[1, 3] -= 0.05f;
                    vertexuri[2, 3] -= 0.05f;
                }
            if (keyboard[Key.Left] && keyboard[Key.B])
                if (choice != 3)
                    vertexuri[choice, 4] -= 0.05f;
                else
                {
                    vertexuri[0, 4] -= 0.05f;
                    vertexuri[1, 4] -= 0.05f;
                    vertexuri[2, 4] -= 0.05f;
                }
            if (keyboard[Key.Right] && keyboard[Key.R])
                if (choice != 3)
                    vertexuri[choice, 2] += 0.05f;
                else
                {
                    vertexuri[0, 2] += 0.05f;
                    vertexuri[1, 2] += 0.05f;
                    vertexuri[2, 2] += 0.05f;
                }
            if (keyboard[Key.Right] && keyboard[Key.G])
                if (choice != 3)
                    vertexuri[choice, 3] += 0.05f;
                else
                {
                    vertexuri[0, 3] += 0.05f;
                    vertexuri[1, 3] += 0.05f;
                    vertexuri[2, 3] += 0.05f;
                }
            if (keyboard[Key.Right] && keyboard[Key.B])
                if (choice != 3)
                    vertexuri[choice, 4] += 0.05f;
                else
                {
                    vertexuri[0, 4] += 0.05f;
                    vertexuri[1, 4] += 0.05f;
                    vertexuri[2, 4] += 0.05f;
                }



            //rotatia camerei pentru problema 8
            //Keyboard movement...
            Matrix4 camera = Matrix4.LookAt(0.0f, 0.0f, -3.0f, 0.0f, 0.0f, 0.5f, 0.0f, 1.0f, 0.0f);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);

            
            GL.Rotate(angle, new Vector3d(0,1,1));
            angle += 1;
            base.OnUpdateFrame(e);

            //



        }
        
        void DrawTriangle()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);


            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(vertexuri[0,2], vertexuri[0, 3], vertexuri[0, 4]);
            GL.Vertex2(vertexuri[0, 0], vertexuri[0, 1]);
            GL.Color3(vertexuri[1, 2], vertexuri[1, 3], vertexuri[1, 4]);
            GL.Vertex2(vertexuri[1, 0], vertexuri[1, 1]);
            GL.Color3(vertexuri[2, 2], vertexuri[2, 3], vertexuri[2, 4]);
            GL.Vertex2(vertexuri[2, 0], vertexuri[2, 1]);
            GL.End();

        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            DrawTriangle();
            this.SwapBuffers();
        }
        void Log(string msg, string file="log.txt")
        {
            Console.WriteLine(msg);
            if (!File.Exists(file))
                File.Create(file);
            using (StreamWriter writetext = File.AppendText(file))
            {
                writetext.WriteLine(msg);
            }
        }

        [STAThread]
        static void Main(string[] args)
        {


            using (TriunghiColor example = new TriunghiColor("incarca.txt"))
            {


                example.Run(30.0, 0.0);

            }
        }


    }




}

