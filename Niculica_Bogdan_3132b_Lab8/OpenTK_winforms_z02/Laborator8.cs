using System;

using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTK_winforms_z02
{
    public partial class Laborator8 : Form
    { 

        //Stări de control cameră.
        private int eyePosX, eyePosY, eyePosZ;

        /// <summary>
        /// Sa incercat restructurarea proiectului astfel incat sursele de lumina sa fie implementate in maniera POO; atat sursa0(cea deja definita in proiect,
        /// cat si sursa 1 au fost implementate prin clase
        /// </summary>
        public SursaLumina sursa0 = new SursaLumina();
        public SursaLumina sursa1 = new SursaLumina();


        private Point mousePos;
        private float camDepth;

        //Stări de control mouse.
        private bool statusControlMouse2D, statusControlMouse3D, statusMouseDown;

        //Stări de control axe de coordonate.
        private bool statusControlAxe;

        //Stări de control iluminare.
        private bool lightON;


        //Stări de control obiecte 3D.
        private string statusCube;



        //Structuri de stocare a vertexurilor și a listelor de vertexuri.
        private int[,] arrVertex = new int[50, 3];         //Stocam matricea de vertexuri; 3 coloane vor reține informația pentru X, Y, Z. Nr. de linii definește nr. de vertexuri.
        private int nVertex;

        private int[] arrQuadsList = new int[100];        //Lista de vertexuri pentru construirea cubului prin intermediul quadurilor. Ne bazăm pe lista de vertexuri.
        private int nQuadsList;

        private int[] arrTrianglesList = new int[100];    //Lista de vertexuri pentru construirea cubului prin intermediul triunghiurilor. Ne bazăm pe lista de vertexuri.
        private int nTrianglesList;

        //Fișiere de in/out pentru manipularea vertexurilor.
        private string fileVertex = "vertexList.txt";
        private string fileQList = "quadsVertexList.txt";
        private string fileTList = "trianglesVertexList.txt";
        private bool statusFiles;



       
        //# SET 2
        //private float[] valuesAmbientTemplate1 = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
        //private float[] valuesDiffuseTemplate1 = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
        //private float[] valuesSpecularTemplate1 = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
        //private float[] valuesPositionTemplate1 = new float[] { 1.0f, 1.0f, 1.0f, 0.0f };

        



        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   ON_LOAD
        public Laborator8() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

            SetupValues();
            SetupWindowGUI();
        }

        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   SETARI INIȚIALE
        private void SetupValues() {
            eyePosX = 100;
            eyePosY = 100;
            eyePosZ = 50;

            camDepth = 1.04f;



            //Dim valuesAmbientTemplate0() As Single = {255, 0, 0, 1.0}      //Valori alternative ambientale(lumină colorată)
            //# SET 1
            sursa0.valuesAmbientTemplate0 = new float[] { 0.1f, 0.1f, 0.1f, 1.0f };
            sursa0.valuesDiffuseTemplate0 = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
            sursa0.valuesSpecularTemplate0 = new float[] { 0.1f, 0.1f, 0.1f, 1.0f };
            sursa0.valuesPositionTemplate0 = new float[] { 0.0f, 0.0f, 5.0f, 1.0f };

            sursa1.valuesAmbientTemplate0 = new float[] { 0.1f, 0.1f, 0.1f, 1.0f };
            sursa1.valuesDiffuseTemplate0 = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
            sursa1.valuesSpecularTemplate0 = new float[] { 0.1f, 0.1f, 0.1f, 1.0f };
            sursa1.valuesPositionTemplate0 = new float[] { 10.0f, 10.0f, 7.0f, 1.0f };




            sursa0.valuesAmbient0 = new float[4];
            sursa0.valuesDiffuse0 = new float[4];
            sursa0.valuesSpecular0 = new float[4];
            sursa0.valuesPosition0 = new float[4];

            sursa1.valuesAmbient0 = new float[4];
            sursa1.valuesDiffuse0 = new float[4];
            sursa1.valuesSpecular0 = new float[4];
            sursa1.valuesPosition0 = new float[4];


            sursa0.setLightValues();
            sursa1.setLightValues();

            numericXeye.Value = eyePosX;
            numericYeye.Value = eyePosY;
            numericZeye.Value = eyePosZ;





        }


        private void SetupWindowGUI() {

            MessageBox.Show("Controlul celei de-a doua sursa de lumina se va face: W,S(axa OZ);A,D(axa OX);Q,E(axa OY) sau tinand click stanga apasat pe mouse");

            setControlMouse2D(false);
            setControlMouse3D(false);

            numericCameraDepth.Value = (int)camDepth;

            setControlAxe(true);

            setCubeStatus("OFF");
            setIlluminationStatus(false);
            setSource0Status(false);
            setSource1Status(false);

            setTrackLigh0Default();
            setColorAmbientLigh0Default();
            setColorDifuseLigh0Default();
            setColorSpecularLigh0Default();
        }


        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   MANIPULARE VERTEXURI ȘI LISTE DE COORDONATE.
        //Încărcarea coordonatelor vertexurilor și lista de compunere a obiectelor 3D.
        private void loadVertex() {

            //Testăm dacă fișierul există
            try {
                StreamReader fileReader = new StreamReader((fileVertex));
                nVertex = Convert.ToInt32(fileReader.ReadLine().Trim());
                Console.WriteLine("Vertexuri citite: " + nVertex.ToString());

                string tmpStr = "";
                string[] str = new string[3];
                for (int i = 0; i < nVertex; i++) {
                    tmpStr = fileReader.ReadLine();
                    str = tmpStr.Trim().Split(' ');
                    arrVertex[i, 0] = Convert.ToInt32(str[0].Trim());
                    arrVertex[i, 1] = Convert.ToInt32(str[1].Trim());
                    arrVertex[i, 2] = Convert.ToInt32(str[2].Trim());
                }
                fileReader.Close();

            } catch (Exception) {
                statusFiles = false;
                Console.WriteLine("Fisierul cu informații vertex <" + fileVertex + "> nu exista!");
                MessageBox.Show("Fisierul cu informații vertex <" + fileVertex + "> nu exista!");
            }
        }

        private void loadQList() {

            //Testăm dacă fișierul există
            try {
                StreamReader fileReader = new StreamReader(fileQList);

                int tmp;
                string line;
                nQuadsList = 0;

                while ((line = fileReader.ReadLine()) != null) {
                    tmp = Convert.ToInt32(line.Trim());
                    arrQuadsList[nQuadsList] = tmp;
                    nQuadsList++;
                }

                fileReader.Close();
            } catch (Exception) {
                statusFiles = false;
                MessageBox.Show("Fisierul cu informații vertex <" + fileQList + "> nu exista!");
            }

        }

        private void loadTList() {

            //Testăm dacă fișierul există
            try {
                StreamReader fileReader = new StreamReader(fileTList);

                int tmp;
                string line;
                nTrianglesList = 0;

                while ((line = fileReader.ReadLine()) != null) {
                    tmp = Convert.ToInt32(line.Trim());
                    arrTrianglesList[nTrianglesList] = tmp;
                    nTrianglesList++;
                }

                fileReader.Close();
            } catch (Exception) {
                statusFiles = false;
                MessageBox.Show("Fisierul cu informații vertex <" + fileTList + "> nu exista!");
            }

        }

        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   CONTROL CAMERĂ

        //Controlul camerei după axa X cu spinner numeric (un cadran).
        private void numericXeye_ValueChanged(object sender, EventArgs e) {
            eyePosX = (int)numericXeye.Value;
            GlControl1.Invalidate(); //Forțează redesenarea întregului control OpenGL. Modificările vor fi luate în considerare (actualizare).
        }
        //Controlul camerei după axa Y cu spinner numeric (un cadran).
        private void numericYeye_ValueChanged(object sender, EventArgs e) {
            eyePosY = (int)numericYeye.Value;
            GlControl1.Invalidate(); //Forțează redesenarea întregului control OpenGL. Modificările vor fi luate în considerare (actualizare).
        }
        //Controlul camerei după axa Z cu spinner numeric (un cadran).
        private void numericZeye_ValueChanged(object sender, EventArgs e) {
            eyePosZ = (int)numericZeye.Value;
            GlControl1.Invalidate(); //Forțează redesenarea întregului control OpenGL. Modificările vor fi luate în considerare (actualizare).
        }
        //Controlul adâncimii camerei față de (0,0,0).
        private void numericCameraDepth_ValueChanged(object sender, EventArgs e) {
            camDepth = 1 + ((float)numericCameraDepth.Value) * 0.1f;
            GlControl1.Invalidate();
        }


        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   CONTROL MOUSE
        //Setăm variabila de stare pentru rotația în 2D a mouseului.
        private void setControlMouse2D(bool status) {
            if (status == false) {
                statusControlMouse2D = false;
                btnMouseControl2D.Text = "2D mouse control OFF";
            } else {
                statusControlMouse2D = true;
                btnMouseControl2D.Text = "2D mouse control ON";
            }
        }
        //Setăm variabila de stare pentru rotația în 3D a mouseului.
        private void setControlMouse3D(bool status) {
            if (status == false) {
                statusControlMouse3D = false;
                btnMouseControl3D.Text = "3D mouse control OFF";
            } else {
                statusControlMouse3D = true;
                btnMouseControl3D.Text = "3D mouse control ON";
            }
        }

        //Controlul mișcării setului de coordonate cu ajutorul mouseului (în plan 2D/3D)
        private void btnMouseControl2D_Click(object sender, EventArgs e) {
            if (statusControlMouse2D == true) {
                setControlMouse2D(false);
            } else {
                setControlMouse3D(false);
                setControlMouse2D(true);
            }
        }
        private void btnMouseControl3D_Click(object sender, EventArgs e) {
            if (statusControlMouse3D == true) {
                setControlMouse3D(false);
            } else {
                setControlMouse2D(false);
                setControlMouse3D(true);
            }
        }



        //Mișcarea lumii 3D cu ajutorul mouselui (click'n'drag de mouse).
        private void GlControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (statusMouseDown == true)
            {
                mousePos = new Point(e.X, e.Y);
                GlControl1.Invalidate();     //Forțează redesenarea întregului control OpenGL. Modificările vor fi luate în considerare (actualizare).
            }
            if (lightON == true)
            {
                sursa1.valuesPosition0[1] = e.X;
                sursa1.valuesPosition0[2] = e.Y;
            }
        }
/**/
        private void GlControl1_MouseDown(object sender, MouseEventArgs e) {
            statusMouseDown = true;
        }
        private void GlControl1_MouseUp(object sender, MouseEventArgs e) {
            statusMouseDown = false;
        }


        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   CONTROL ILUMINARE
        //Setăm variabila de stare pentru iluminare.
        private void setIlluminationStatus(bool status) {
            if (status == false) {
                lightON = false;
                btnLights.Text = "Iluminare OFF";
            } else {
                lightON = true;
                btnLights.Text = "Iluminare ON";
            }
        }

        

        //Activăm/dezactivăm iluminarea.
        private void btnLights_Click(object sender, EventArgs e) {
            if (lightON == false) {
                setIlluminationStatus(true);
            } else {
                setIlluminationStatus(false);
            }
            GlControl1.Invalidate();
        }

        //Identifică numărul maxim de lumini pentru implementarea curentă a OpenGL.
        private void btnLightsNo_Click(object sender, EventArgs e) {
            int nr = GL.GetInteger(GetPName.MaxLights);
            MessageBox.Show("Nr. maxim de luminii pentru aceasta implementare este <" + nr.ToString() + ">.");
        }

        //Setăm variabila de stare pentru sursa de lumină 0.
        private void setSource0Status(bool status) {
            if (status == false) {
                sursa0.lightON = false;
                btnLight0.Text = "Sursa 0 OFF";
            } else {
                sursa0.lightON = true;
                btnLight0.Text = "Sursa 0 ON";
            }
        }

        private void setSource1Status(bool status)
        {
            if (status == false)
            {
                sursa1.lightON = false;
                btnLight1.Text = "Sursa 1 OFF";
            }
            else
            {
                sursa1.lightON = true;
                btnLight1.Text = "Sursa 1 ON";
            }
        }

        //Activăm/dezactivăm sursa 0 de iluminare (doar dacă iluminarea este activă).
        private void btnLight0_Click(object sender, EventArgs e) {
            if (lightON == true) {
                if (sursa0.lightON == false) {
                    setSource0Status(true);
                } else {
                    setSource0Status(false);
                }
                GlControl1.Invalidate();
            }
        }



        //Schimbăm poziția sursei 0 de iluminare după axele XYZ.
        private void setTrackLigh0Default() {
            trackLight0PositionX.Value = (int)sursa0.valuesPosition0[0];
            trackLight0PositionY.Value = (int)sursa0.valuesPosition0[1];
            trackLight0PositionZ.Value = (int)sursa0.valuesPosition0[2];
        }

        private void trackLight0PositionX_Scroll(object sender, EventArgs e) {
            sursa0.valuesPosition0[0] = trackLight0PositionX.Value;
            GlControl1.Invalidate();
        }
        private void trackLight0PositionY_Scroll(object sender, EventArgs e) {
            //MessageBox.Show(Convert.ToString(trackLight0PositionY.Value));
            sursa0.valuesPosition0[1] = trackLight0PositionY.Value;
            GlControl1.Invalidate();
        }
        private void trackLight0PositionZ_Scroll(object sender, EventArgs e) {
            sursa0.valuesPosition0[2] = trackLight0PositionZ.Value;
            GlControl1.Invalidate();
        }

        private void GlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lightON == true)
            {
                if (e.KeyCode == Keys.W)
                {
                    
                    sursa1.valuesPosition0[2] += 1;
                    
                }
                if (e.KeyCode == Keys.S)
                {
                    sursa1.valuesPosition0[2] -= 1;
                }
                if (e.KeyCode == Keys.A)
                {
                    sursa1.valuesPosition0[0] += 1;
                }
                if (e.KeyCode == Keys.D)
                {
                    sursa1.valuesPosition0[0] -= 1;
                }
                if (e.KeyCode == Keys.Q)
                {
                    sursa1.valuesPosition0[1] += 1;
                }
                if (e.KeyCode == Keys.R)
                {
                    sursa1.valuesPosition0[1] -= 1;
                }
                GlControl1.Invalidate();
            }
        }

        //Schimbăm culoarea sursei de lumină 0 (ambiental) în domeniul RGB.
        private void setColorAmbientLigh0Default() {
            numericLight0Ambient_Red.Value = (decimal)sursa0.valuesAmbient0[0];
            numericLight0Ambient_Green.Value = (decimal)sursa0.valuesAmbient0[1];
            numericLight0Ambient_Blue.Value = (decimal)sursa0.valuesAmbient0[2];
        }
        
        private void numericLight0Ambient_Red_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesAmbient0[0] = (float)numericLight0Ambient_Red.Value / 100;
            GlControl1.Invalidate();
        }
        private void numericLight0Ambient_Green_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesAmbient0[1] = (float)numericLight0Ambient_Green.Value / 100;
            GlControl1.Invalidate();
        }
        private void numericLight0Ambient_Blue_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesAmbient0[2] = (float)numericLight0Ambient_Blue.Value / 100;
            GlControl1.Invalidate();
        }

        //Schimbăm culoarea sursei de lumină 0 (difuză) în domeniul RGB.
        private void setColorDifuseLigh0Default() {
            numericLight0Difuse_Red.Value = (decimal)sursa0.valuesDiffuse0[0];
            numericLight0Difuse_Green.Value = (decimal)sursa0.valuesDiffuse0[1];
            numericLight0Difuse_Blue.Value = (decimal)sursa0.valuesDiffuse0[2];
        }
        private void numericLight0Difuse_Red_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesDiffuse0[0] = (float)numericLight0Difuse_Red.Value / 100;
            GlControl1.Invalidate();
        }
        private void numericLight0Difuse_Green_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesDiffuse0[1] = (float)numericLight0Difuse_Green.Value / 100;
            GlControl1.Invalidate();
        }
        private void numericLight0Difuse_Blue_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesDiffuse0[2] = (float)numericLight0Difuse_Blue.Value / 100;
            GlControl1.Invalidate();
        }

        //Schimbăm culoarea sursei de lumină 0 (specular) în domeniul RGB.
        private void setColorSpecularLigh0Default() {
            numericLight0Specular_Red.Value = (decimal)sursa0.valuesSpecular0[0];
            numericLight0Specular_Green.Value = (decimal)sursa0.valuesSpecular0[1];
            numericLight0Specular_Blue.Value = (decimal)sursa0.valuesSpecular0[2];
        }
        private void numericLight0Specular_Red_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesSpecular0[0] = (float)numericLight0Specular_Red.Value / 100;
            GlControl1.Invalidate();
        }
        private void numericLight0Specular_Green_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesSpecular0[1] = (float)numericLight0Specular_Green.Value / 100;
            GlControl1.Invalidate();
        }
        private void numericLight0Specular_Blue_ValueChanged(object sender, EventArgs e) {
            sursa0.valuesSpecular0[2] = (float)numericLight0Specular_Blue.Value / 100;
            GlControl1.Invalidate();
        }

        //Resetare stare sursă de lumină nr. 0.
        private void setLight0Values() {
            for (int i = 0; i < sursa0.valuesAmbientTemplate0.Length; i++) {
                sursa0.valuesAmbient0[i] = sursa0.valuesAmbientTemplate0[i];
            }
            for (int i = 0; i < sursa0.valuesDiffuseTemplate0.Length; i++) {
                sursa0.valuesDiffuse0[i] = sursa0.valuesDiffuseTemplate0[i];
            }
            for (int i = 0; i < sursa0.valuesPositionTemplate0.Length; i++) {
                sursa0.valuesPosition0[i] = sursa0.valuesPositionTemplate0[i];
            }
        }
        private void btnLight0Reset_Click(object sender, EventArgs e) {
            setLight0Values();
            setLight1Values();
            setTrackLigh0Default();
            setColorAmbientLigh0Default();
            setColorDifuseLigh0Default();
            setColorSpecularLigh0Default();
            GlControl1.Invalidate();
        }

        private void setLight1Values()
        {
            for (int i = 0; i < sursa1.valuesAmbientTemplate0.Length; i++)
            {
                sursa1.valuesAmbient0[i] = sursa1.valuesAmbientTemplate0[i];
            }
            for (int i = 0; i < sursa1.valuesDiffuseTemplate0.Length; i++)
            {
                sursa1.valuesDiffuse0[i] = sursa1.valuesDiffuseTemplate0[i];
            }
            for (int i = 0; i < sursa1.valuesPositionTemplate0.Length; i++)
            {
                sursa1.valuesPosition0[i] = sursa1.valuesPositionTemplate0[i];
            }
        }

        

        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   CONTROL OBIECTE 3D
        //Setăm variabila de stare pentru afișarea/scunderea sistemului de coordonate.
        private void setControlAxe(bool status) {
            if (status == false) {
                statusControlAxe = false;
                btnShowAxes.Text = "Axe Oxyz OFF";
            } else {
                statusControlAxe = true;
                btnShowAxes.Text = "Axe Oxyz ON";
            }
        }

        //Controlul axelor de coordonate (ON/OFF).
        private void btnShowAxes_Click(object sender, EventArgs e) {
            if (statusControlAxe == true) {
                setControlAxe(false);
            } else {
                setControlAxe(true);
            }
            GlControl1.Invalidate();
        }

        //Setăm variabila de stare pentru desenarea cubului. Valorile acceptabile sunt:
        //TRIANGLES = cubul este desenat, prin triunghiuri.
        //QUADS = cubul este desenat, prin quaduri.
        //OFF (sau orice altceva) = cubul nu este desenat.
        private void setCubeStatus(string status) {
            if (status.Trim().ToUpper().Equals("TRIANGLES")) {
                statusCube = "TRIANGLES";
            } else if (status.Trim().ToUpper().Equals("QUADS")) {
                statusCube = "QUADS";
            } else {
                statusCube = "OFF";
            }
        }
        private void btnCubeQ_Click(object sender, EventArgs e) {
            statusFiles = true;
            loadVertex();
            loadQList();
            setCubeStatus("QUADS");
            GlControl1.Invalidate();
        }
        private void btnCubeT_Click(object sender, EventArgs e) {
            statusFiles = true;
            loadVertex();
            loadTList();
            setCubeStatus("TRIANGLES");
            GlControl1.Invalidate();
        }
        private void btnResetObjects_Click(object sender, EventArgs e) {
            setCubeStatus("OFF");
            GlControl1.Invalidate();
        }

        private void btnLight1_Click(object sender, EventArgs e)
        {
            if (lightON == true)
            {
                if (sursa1.lightON == false)
                {
                    setSource1Status(true);
                }
                else
                {
                    setSource1Status(false);
                }
                GlControl1.Invalidate();
            }
        }

        private void GlControl1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }




        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   ADMINISTRARE MOD 3D (METODA PRINCIPALĂ)
        private void GlControl1_Paint(object sender, PaintEventArgs e) {
            //Resetează buffer-ele la valori default.
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            //Culoarea default a mediului.
            GL.ClearColor(Color.Black);

            //Setări preliminară pentru mediul 3D.
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(camDepth, 4 / 3, 1, 10000);    //Declararea perspectivei spatiale.
            Matrix4 lookat = Matrix4.LookAt(eyePosX, eyePosY, eyePosZ, 0, 0, 0, 0, 1, 0);             //Declararea camerei (stare inițială).
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);                                                             //Încărcarea modelului camerei.
            GL.LoadIdentity();
            GL.LoadMatrix(ref lookat);
            GL.Viewport(0, 0, GlControl1.Width, GlControl1.Height);                                      //Mărimea suprafeței randate (scena 3D este proiectată pe aceasta).
            GL.Enable(EnableCap.DepthTest);                                                                //Corecții de adâncime.
            GL.DepthFunc(DepthFunction.Less);                                                                //Corecții de adâncime.

            //Pornim iluminarea (daca avem permisiunea să o facem).
            if (lightON == true) {
                //Permite utilizarea iluminării. Fara aceasta corecție iluminarea nu functioneaza.
                GL.Enable(EnableCap.Lighting);
            } else {
                //Dezactivează utilizarea iluminării.
                GL.Disable(EnableCap.Lighting);
            }

            //Se creeaza sursa de iluminare. In acest caz folosim o singura sursa.
            //Numarul de surse de lumini depinde de implementarea OpenGL, dar de obicei cel putin 8 surse sunt posibile simultan.
            sursa0.createSource();

            sursa1.createSource();

            if ((lightON == true) && (sursa0.lightON == true)) {
                //Activam sursa 0 de lumina. Fara aceasta actiune nu avem iluminare.
                sursa0.enableSource();
            } else {
                //Dezactivam sursa 0 de lumina.
                sursa0.disableSource();
            }

            ///================LABORATOR 8 PROBLEMA 3, IMPLEMENTAREA UNEI A DOUA SURSE DE LUMINA ============================



            

            if ((lightON == true) && (sursa1.lightON == true))
            {
                sursa1.enableSource();
            }
            else
            {
                sursa1.disableSource();
            }



            


            /// ----------------------------------------



            //Controlul rotației cu mouse-ul (2D).
            if (statusControlMouse2D == true) {
                GL.Rotate(mousePos.X, 0, 1, 0);
            }

            //Controlul rotației cu mouse-ul (3D).
            if (statusControlMouse3D == true) {
                GL.Rotate(mousePos.X, 0, 1, 1);
            }

            //---------------------------
            //---------------------------
            //Descrierea obiectelor 3D!!! Axe de coordonate.
            if (statusControlAxe == true) {
                DeseneazaAxe();
            }

            //Desenăm obiectele 3D (cub format din quads sau triunghiuri).
            if (statusCube.ToUpper().Equals("QUADS")) {
                DeseneazaCubQ();
            } else if (statusCube.ToUpper().Equals("TRIANGLES")) {
                DeseneazaCubT();
            }

            //Limitează viteza de randare pentru a nu supraîncarca unitatea GPU (în caz contrar randarea se face cât de rapid este posibil, pe 100% din resurse). 
            //Dezavantajul este că o viteză prea mică poate afecta negativ cursivitatea animației!
            //GraphicsContext.CurrentContext.SwapInterval = 1;                                         //Testati cu valori din 10 in 10!!!
            //GraphicsContext.CurrentContext.VSync = True

            GlControl1.SwapBuffers();
        }


        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //   DESENARE OBIECTE 3D
        //Desenează axe XYZ.
        private void DeseneazaAxe() {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(75, 0, 0);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 75, 0);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 75);
            GL.End();
        }
        //Desenează cubul - quads.
        private void DeseneazaCubQ() {
            GL.Begin(PrimitiveType.Quads);
            for (int i = 0; i < nQuadsList; i++) {
                switch (i % 4) {
                    case 0:
                        GL.Color3(Color.Blue);
                        break;
                    case 1:
                        GL.Color3(Color.Red);
                        break;
                    case 2:
                        GL.Color3(Color.Green);
                        break;
                    case 3:
                        GL.Color3(Color.Yellow);
                        break;
                }
                int x = arrQuadsList[i];
                GL.Vertex3(arrVertex[x, 0], arrVertex[x, 1], arrVertex[x, 2]);
            }
            GL.End();
        }
        //Desenează cubul - triunghuri.
        private void DeseneazaCubT() {
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < nTrianglesList; i++) {
                switch (i % 3) {
                    case 0:
                        GL.Color3(Color.Blue);
                        break;
                    case 1:
                        GL.Color3(Color.Red);
                        break;
                    case 2:
                        GL.Color3(Color.Green);
                        break;
                }
                int x = arrTrianglesList[i];
                GL.Vertex3(arrVertex[x, 0], arrVertex[x, 1], arrVertex[x, 2]);
            }
            GL.End();
        }

    }

}
