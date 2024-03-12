using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace Generálás
{
    class Program
    {
        static Random r = new Random();
        static List<string> telefonszamok = new List<string>();
        static List<string> vnev = new List<string>();
        static List<string> knev = new List<string>();
        static List<string> nevek = new List<string>();
        static List<string> uzletvez = new List<string>();
        static List<string> kiszolgalok = new List<string>();
        static List<string> szolgaltatasok = new List<string>();
        static List<string> cimek = new List<string>();


        static void Main(string[] args)
        {
            CimBe();
            SzolgBe();
            kiszolgalox();
            TelefonszamKigeneralas();
            NevGeneralas(200,true,nevek);
            NevGeneralas(100,false,uzletvez);
            SqlFajlKi();
            Console.ReadLine();
        }

        private static void SqlFajlKi()
        {
            StreamWriter sql = new StreamWriter("fodraszat_adatok.sql");
            sql.WriteLine("use fodraszat_adatbazis;");
            sql.WriteLine();
            sql.WriteLine("insert into helyszin\nvalues");
            for (int i = 0; i < cimek.Count; i++)
            {
                string[] st = cimek[i].Split(';');
                if (i == cimek.Count - 1)
                {
                    sql.WriteLine($"({i + 1},{st[0]},'{st[1]}');");
                }
                else sql.WriteLine($"({i + 1},{st[0]},'{st[1]}'),");
            }

            sql.WriteLine();
            sql.WriteLine("insert into uzlet\nvalues");
            for (int i = 0; i < uzletvez.Count; i++)
            {
                if (i == uzletvez.Count - 1)
                {
                    sql.WriteLine($"({i + 1},'{uzletvez[i]}',{Math.Abs(i-100)});");
                }
                else sql.WriteLine($"({i + 1},'{uzletvez[i]}',{Math.Abs(i - 100)}),");
            }

            sql.WriteLine();
            sql.WriteLine("insert into fodrasz\nvalues");
            for (int i = 0; i < nevek.Count/2; i++)
            {
                string[] st = nevek[i].Split(';');
                if (i == nevek.Count/2 - 1)
                {
                    sql.WriteLine($"('{telefonszamok[i]}','{st[0]}','{st[1]}');");
                }
                else sql.WriteLine($"('{telefonszamok[i]}','{st[0]}','{st[1]}'),");
            }

            sql.WriteLine();
            sql.WriteLine("insert into vendeg\nvalues");
            for (int i = 100; i < nevek.Count; i++)
            {
                string[] st = nevek[i].Split(';');
                if (i == nevek.Count - 1)
                {
                    sql.WriteLine($"('{telefonszamok[i]}','{st[0]}','{st[1]}');");
                }
                else sql.WriteLine($"('{telefonszamok[i]}','{st[0]}','{st[1]}'),");
            }

            sql.WriteLine();
            sql.WriteLine("insert into szolgaltatas\nvalues");
            for (int i = 0; i < szolgaltatasok.Count; i++)
            {
                string[] st = szolgaltatasok[i].Split(';');
                if (i == szolgaltatasok.Count - 1)
                {
                    sql.WriteLine($"('{st[0]}',{st[1]},'{st[2]}');");
                }
                else sql.WriteLine($"('{st[0]}',{st[1]},'{st[2]}'),");
            }


            // teljesen veletlen
            sql.WriteLine();
            sql.WriteLine("insert into foglalas\nvalues");
            for (int i = 0; i < 100;i++)
            {
                if (i == 99)
                {
                    string[] st = szolgaltatasok[r.Next(0, szolgaltatasok.Count)].Split(';');
                    sql.WriteLine($"({i + 1},'{st[0]}','{telefonszamok[r.Next(100, telefonszamok.Count)]}','{telefonszamok[r.Next(0, telefonszamok.Count / 2)]}',{r.Next(1, 101)});");
                }
                else {

                    string[] st = szolgaltatasok[r.Next(0, szolgaltatasok.Count)].Split(';');
                    sql.WriteLine($"({i + 1},'{st[0]}','{telefonszamok[r.Next(100, telefonszamok.Count)]}','{telefonszamok[r.Next(0, telefonszamok.Count / 2)]}',{r.Next(1, 101)}),");
                }

            }



            sql.Close();

        }


        private static void SzolgBe()
        {
            StreamReader r = new StreamReader("szolgaltatasok.txt");
            while (!r.EndOfStream) szolgaltatasok.Add(r.ReadLine());
            r.Close();
        }

        private static void CimBe()
        {
            StreamReader r = new StreamReader("cimek.txt");
            while(!r.EndOfStream) cimek.Add(r.ReadLine());
            r.Close();
        }

        private static void kiszolgalox()
        {
            StreamReader r = new StreamReader("kiszolgalok.txt");
            while (!r.EndOfStream)
            {
                kiszolgalok.Add(r.ReadLine());
            }
            r.Close();
        }

        private static void NevGeneralas(int db, bool kellemail,List<string> lista)
        {
            VnevKnevBe();
            while (lista.Count != db)
            {
                bool sv = r.Next(0, 100) <= 20;
                bool sv2 = r.Next(0,2) == 0 ? true : false;
                if (sv)
                {
                    if (sv2)
                    {
                        lista.Add(vnev[r.Next(0, vnev.Count)] + " " + knev[r.Next(0, knev.Count / 2)] + " " + knev[r.Next(0, knev.Count / 2)]);
                    }
                    else lista.Add(vnev[r.Next(0, vnev.Count)] + " " + knev[r.Next(49, knev.Count)] + " " + knev[r.Next(49, knev.Count)]);
                }
                else lista.Add(vnev[r.Next(0, vnev.Count)] + " " + knev[r.Next(0, knev.Count)]);

                if (kellemail)
                {
                    StringBuilder str = new StringBuilder(";" + lista[lista.Count - 1] + "@" + kiszolgalok[r.Next(0, kiszolgalok.Count)]);
                    str.Replace(" ", "");
                    lista[lista.Count - 1] = lista[lista.Count - 1] + str.ToString().ToLower();
                }
            }
        }

        private static void VnevKnevBe()
        {
            StreamReader r = new StreamReader("knev.txt");
            StreamReader f = new StreamReader("vnev.txt");
            while (!r.EndOfStream)
            {
                knev.Add(r.ReadLine());
            }
            r.Close();

            while (!f.EndOfStream)
            {
                vnev.Add(f.ReadLine());
            }
            f.Close();
            
        }

        private static void TelefonszamKigeneralas()
        {
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
                }
            }
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
