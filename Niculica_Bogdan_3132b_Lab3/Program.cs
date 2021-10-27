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
        float R1,R2,R3, G1,G2,G3, B1,B2,B3;
        bool pressedFirst, pressedSecond, pressedThird;

        public TriunghiColor() : base(800, 600,OpenTK.Graphics.GraphicsMode.Default,"Schimba culoare triunghi")
        {
            KeyDown += Keyboard_KeyDown;
            R1 = R2 = R3 = 0.9f;
            G1 = G2 = G3 = 0.8f;
            B1 = B2 = B3 = 0.7f;
            pressedFirst = pressedSecond = pressedThird = false;
            
            
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
            GL.ClearColor(Color.Gray);
            
        }


        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            //Rezolvare problema 8+9; memorarea culorii fiecarui vertex separat in 9 variabile nu este frumoasa! Revin cu modificari
            //Scad culoare
            if (keyboard[Key.Left] && keyboard[Key.R])
            {
                if(pressedFirst)
                    R1 = R1 - 0.005f;
                else if(pressedSecond)
                    R2 = R2 - 0.005f;
                else if(pressedThird)
                    R3 = R3 - 0.005f;
                else
                {
                    R1 = R1 - 0.005f;
                    R2 = R2 - 0.005f;
                    R3 = R3 - 0.005f;
                }
                Log($"Decrease - R1={R1};R2={R2};R3={R3}");
            }
            if (keyboard[Key.Left] && keyboard[Key.G])
            {
                if (pressedFirst)
                    G1 = G1 - 0.005f;
                else if (pressedSecond)
                    G2 = G2 - 0.005f;
                else if (pressedThird)
                    G3 = G3 - 0.005f;
                else
                {
                    G1 = G1 - 0.005f;
                    G2 = G2 - 0.005f;
                    G3 = G3 - 0.005f;
                }
                Log($"Decrease - G1={G1};G2={G2};G3={G3}");
            }
            if (keyboard[Key.Left] && keyboard[Key.B])
            {
                if (pressedFirst)
                    B1 = B1 - 0.005f;
                else if (pressedSecond)
                    B2 = B2 - 0.005f;
                else if (pressedThird)
                    B3 = B3 - 0.005f;
                else
                {
                    B1 = B1 - 0.005f;
                    B2 = B2 - 0.005f;
                    B3 = B3 - 0.005f;
                }
                Log($"Decrease - B1={B1};B2={B2};B3={B3}");
            }
            //cresc culoare
            if (keyboard[Key.Right] && keyboard[Key.R])
            {
                if (pressedFirst)
                    R1 = R1 + 0.005f;
                else if (pressedSecond)
                    R2 = R2 + 0.005f;
                else if (pressedThird)
                    R3 = R3 + 0.005f;
                else
                {
                    R1 = R1 + 0.005f;
                    R2 = R2 + 0.005f;
                    R3 = R3 + 0.005f;
                }
                Log($"Increase - R1={R1};R2={R2};R3={R3}");
            }
            if (keyboard[Key.Right] && keyboard[Key.G])
            {
                if (pressedFirst)
                    G1 = G1 + 0.005f;
                else if (pressedSecond)
                    G2 = G2 + 0.005f;
                else if (pressedThird)
                    G3 = G3 + 0.005f;
                else
                {
                    G1 = G1 + 0.005f;
                    G2 = G2 + 0.005f;
                    G3 = G3 + 0.005f;
                }
                Log($"Increase - G1={G1};G2={G2};G3={G3}");
            }
            if (keyboard[Key.Right] && keyboard[Key.B])
            {
                if (pressedFirst)
                    B1 = B1 + 0.005f;
                else if (pressedSecond)
                    B2 = B2 + 0.005f;
                else if (pressedThird)
                    B3 = B3 + 0.005f;
                else
                {
                    B1 = B1 + 0.005f;
                    B2 = B2 + 0.005f;
                    B3 = B3 + 0.005f;
                }
                Log($"Increase - B1={B1};B2={B2};B3={B3}");
            }


            
            if (keyboard[Key.Number1])
            {
                Log("vertex 1!");
                pressedFirst = true;
                pressedSecond = pressedThird = false;

            }
            if (keyboard[Key.Number2])
            {
                Log("vertex 2!");
                pressedSecond = true;
                pressedFirst = pressedThird = false;
            }
            if (keyboard[Key.Number3])
            {
                Log("vertex 3!");
                pressedThird = true;
                pressedFirst = pressedSecond = false;
            }

        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);


            GL.Begin(PrimitiveType.Triangles);
            
            GL.Color3(R1, G1, B1);
            GL.Vertex2(0.0f, 1.0f);
            GL.Color3(R2, G2, B2);
            GL.Vertex2(-1.0f, -1.0f);
            GL.Color3(R3, G3, B3);
            GL.Vertex2(1.0f, -1.0f);

            GL.End();


            this.SwapBuffers();
        }
        void Log(string msg, string file="log.txt")
        {
            Console.WriteLine(msg);
            using (StreamWriter writetext = new StreamWriter(file))
            {
                writetext.WriteLine(msg);
            }
        }

        [STAThread]
        static void Main(string[] args)
        {


            using (TriunghiColor example = new TriunghiColor())
            {


                example.Run(30.0, 0.0);

            }
        }


    }




}

