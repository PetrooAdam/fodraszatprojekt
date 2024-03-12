using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Generálás
{
    class Program
    {
        static Random r = new Random();
        static List<string> telefonszamok = new List<string>();

        static void Main(string[] args)
        {
            TelefonszamKigeneralas();
            Console.ReadLine();
        }

        private static void TelefonszamKigeneralas()
        {
            StreamWriter f = new StreamWriter("valami.txt");
            while(telefonszamok.Count < 200)
            { 
                string ujsz = r.Next(0, 3) == 1 ? "+36" : "06";
                int vl = r.Next(0, 3);
                ujsz += vl == 0 ? "20" : vl == 1 ? "30" : "70";
                for (int i = 0; i < 7; i++)
                {
                    ujsz += r.Next(0, 10);
                }
                if (!VaneIlyen(ujsz))
                {
                    telefonszamok.Add(ujsz);
                    f.WriteLine(ujsz);
                }
            }
            f.Close();
        }

        private static bool VaneIlyen(string ujsz)
        {
            int i = 0;
            while (i < telefonszamok.Count && ujsz != telefonszamok[i])
                i++;
            return i < telefonszamok.Count;
        }
    }


}
