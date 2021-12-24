using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace OpenTK_winforms_z02
{
    public class SursaLumina
    {   //Am regandit programul dupa maniera POO astfel incat atunci cand se creeaza o noua instanta a clasei SursaLumina, sa i se aloce automat
        //urmatoarea sursa de lumina disponibila din cele 8 posibile in OpenTK immediate mode;
        private static int surseActive = 0;
        private static LightName[] sursa = { LightName.Light0, LightName.Light1, LightName.Light2, LightName.Light3 };
        private static EnableCap[] sursaEnabler = { EnableCap.Light0, EnableCap.Light1, EnableCap.Light2, EnableCap.Light3 };
        private int sursaCurenta; 

        public SursaLumina()
        {
            sursaCurenta = surseActive++;
            
        }
        public void createSource()
        {
            GL.Light(sursa[sursaCurenta], LightParameter.Ambient, valuesAmbient0);
            GL.Light(sursa[sursaCurenta], LightParameter.Diffuse, valuesDiffuse0);
            GL.Light(sursa[sursaCurenta], LightParameter.Specular, valuesSpecular0);
            GL.Light(sursa[sursaCurenta], LightParameter.Position, valuesPosition0);
        }
        public void enableSource()
        {
            
            GL.Enable(sursaEnabler[sursaCurenta]);
        }
        public void disableSource()
        {
            
                GL.Disable(sursaEnabler[sursaCurenta]);
        }
        public bool lightON { get; set; }

        public float[] valuesAmbientTemplate0;
        public float[] valuesDiffuseTemplate0 { get; set; }
        public float[] valuesSpecularTemplate0 { get; set; }
        public float[] valuesPositionTemplate0 { get; set; }

        public float[] valuesAmbient0 { get; set; }
        public float[] valuesDiffuse0 { get; set; }
        public float[] valuesSpecular0 { get; set; }
        public float[] valuesPosition0 { get; set; }



        public void setLightValues()
        {
            for (int i = 0; i < valuesAmbientTemplate0.Length; i++)
            {
                valuesAmbient0[i] = valuesAmbientTemplate0[i];
            }
            for (int i = 0; i < valuesDiffuseTemplate0.Length; i++)
            {
                valuesDiffuse0[i] = valuesDiffuseTemplate0[i];
            }
            for (int i = 0; i < valuesPositionTemplate0.Length; i++)
            {
                valuesPosition0[i] = valuesPositionTemplate0[i];
            }
        }

    }
}
