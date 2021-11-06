using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Niculica_Bogdan_3132b_Lab3
{
    public class Triunghi
    {
       public Coord3D []vertecsi;

         Triunghi(string numeFisier)
        {
            vertecsi = new Coord3D[3];
            using (StreamReader fis = new StreamReader(numeFisier))
            {
                string[] buff = new string[20];
                for (int i = 0; i < 3; i++)
                {
                    buff = fis.ReadLine().Replace("f", "").Replace(",", "").Split(' ');
                    vertecsi[i] = new Coord3D(float.Parse(buff[0], System.Globalization.CultureInfo.InvariantCulture), float.Parse(buff[1], System.Globalization.CultureInfo.InvariantCulture), float.Parse(buff[2], System.Globalization.CultureInfo.InvariantCulture));

                }


            }
        }
    }
}
