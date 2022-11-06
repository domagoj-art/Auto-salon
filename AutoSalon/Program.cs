using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutoSalon
{
    internal class Program
    {
        public class Auto
        {
            public string Marka;
            public string Model;
            public string PrviKvartal;
            public double Cijena;
        }
        static void Main(string[] args)
        {
            Izbornik();
        }
        public static void Izbornik()
        {
            Console.WriteLine();
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Lista Auta");
            Console.WriteLine("2. Unesi Auto");
            Console.WriteLine("3. Ažuriraj Auto");
            Console.WriteLine("4. Prodajna Statistika");
            Console.WriteLine();

            try
            {
                int izbor;
                izbor = int.Parse(Console.ReadLine());
                if (izbor == 1)
                {
                    ListaAuta();
                }
                else if (izbor == 2)
                {
                    UnesiAuto();
                    Izbornik();
                }
                else if (izbor == 3)
                {
                    AzurirajAuto();
                    Izbornik();
                }
                else if (izbor == 4)
                {
                    Statistika();
                    Izbornik();
                }
                else if(izbor == 0)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Unesite broj od 1 do 4");
                    Izbornik();
                }
            }
            catch
            {
                Console.WriteLine("nešto ste krivo upisali -> unesi broj od 1 do 4");
                Izbornik();
            }
        }
        public static void ListaAuta()
        {
            Console.WriteLine();
            Console.WriteLine("1. Izlistaj Auta");
            Console.WriteLine("2. Sortiraj po cijeni");
            Console.WriteLine("3. Natrag na glavni izbornik");
            Console.WriteLine();

            try
            {
                int izbor;
                izbor = int.Parse(Console.ReadLine());
                if (izbor == 1)
                {
                    string put = @"C:\Users\domag\source\repos\AutoSalon\AutoSalon\bin\Debug\BazaPodataka.txt";
                    using (StreamReader sr = new StreamReader(put))
                    {
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] Lista = line.Split(',');
                            Console.WriteLine($"{Lista[0]} {Lista[1]}, {Lista[2]}kn, {Lista[3]}");
                        }
                    }
                    ListaAuta();
                }
                else if (izbor == 2)
                {
                    string put = @"C:\Users\domag\source\repos\AutoSalon\AutoSalon\bin\Debug\BazaPodataka.txt";
                    using (StreamReader sr = new StreamReader(put))
                    {
                        var auti = DohvatiAute().OrderByDescending(x => x.Cijena);
                        foreach (var auto in auti)
                        {
                            Console.WriteLine($"{auto.Marka} {auto.Model}, {auto.Cijena}kn, {auto.PrviKvartal}");
                        }
                    }
                    ListaAuta();
                }
                else if (izbor == 3)
                {
                    Izbornik();
                }
                else
                {
                    Console.WriteLine("nešto ste krivo unjeli, molim vas unesite brojeve od 1 do 3 ");
                    Console.WriteLine();
                    ListaAuta();
                }
            }
            catch
            {
                Console.WriteLine("nešto ste krivo upisali -> unesi broj od 1 do 3");
                Console.WriteLine();
                ListaAuta();
            }
        }
        public static void UnesiAuto()
        {
            try
            {
                Auto NoviAuto = new Auto();
                Console.WriteLine();
                Console.WriteLine("Unesi marku");
                NoviAuto.Marka = Console.ReadLine();
                Console.WriteLine("Unesi Model");
                NoviAuto.Model = Console.ReadLine();
                Console.WriteLine("Unesi Cijenu Auta");
                NoviAuto.Cijena = double.Parse(Console.ReadLine());
                Console.WriteLine("Unesi \"prodano\" ako je auto prodan ili \"ne\" ako auto nije prodan");
                NoviAuto.PrviKvartal = Console.ReadLine();

                StreamWriter sw = new StreamWriter($"BazaPodataka.txt", true);
                {
                    sw.Write(NoviAuto.Marka + ",");
                    sw.Write(NoviAuto.Model + ",");
                    sw.Write(NoviAuto.Cijena + ",");
                    sw.Write(NoviAuto.PrviKvartal);
                    sw.WriteLine();
                    sw.Close();
                }
            }
            catch
            {
                Console.WriteLine("nešto ste krivo unijeli, molim vas pokušajte ponovo");
                Console.WriteLine();
                Izbornik();
            }
        }
        public static void AzurirajAuto()
        {
            try
            {
                var auti = DohvatiAute();
                var marke = auti.Select(x => x.Marka + " " + x.Model);
                var popis = marke.ToList();


                for (int i = 0; i < popis.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {popis[i]}");
                }

                Console.WriteLine("\n Izaberi broj za ažuriranje ");
                Console.WriteLine();

                int index = int.Parse(Console.ReadLine());

                var autiZaAzurirat = auti.Where(x => x.Marka + " " + x.Model == popis[index - 1]).First();
                Console.WriteLine($"novi model");
                auti[index - 1].Marka = Console.ReadLine();
                Console.WriteLine("nova marka");
                auti[index - 1].Model = Console.ReadLine();
                Console.WriteLine("nova cijena");
                auti[index - 1].Cijena = double.Parse(Console.ReadLine());
                Console.WriteLine("Unesi \"prodano\" ako je auto prodan ili \"ne\" ako auto nije prodan");
                auti[index - 1].PrviKvartal = Console.ReadLine();
                SpremiPromjene(auti);
                Izbornik();
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("nešto ste krivo unijeli, molim vas pokušajte ponovo");
                AzurirajAuto();
            }
        }
        public static void Statistika()
        {
            string put = @"C:\Users\domag\source\repos\AutoSalon\AutoSalon\bin\Debug\BazaPodataka.txt";
            using(StreamReader sr = new StreamReader(put))
            {
                string line = "";
                string ToCheck = "prodano";
                int brojac = 0;
                while((line = sr.ReadLine()) != null)
                {
                    string[] lista = line.Split(',');
                    if (lista.Contains(ToCheck))
                    {
                        brojac++;
                        
                    }
                }
                Console.WriteLine();
                Console.WriteLine("U prvom kvartalu je prodano " + brojac + " auta");
            }
        }
        public static List<Auto> DohvatiAute()
        {
            string put = @"C:\Users\domag\source\repos\AutoSalon\AutoSalon\bin\Debug\BazaPodataka.txt";
            string line = "";
            using(StreamReader sr = new StreamReader(put))
            {
                List <Auto> auti  = new List<Auto>();
                while((line = sr.ReadLine()) != null)
                {
                    string[] list = line.Split(',');
                    Auto a = new Auto()
                    {
                    Marka = list[0],
                    Model = list[1],
                    Cijena = double.Parse(list[2]),
                    PrviKvartal = list[3]
                    };
                    auti.Add(a);
                }
                return auti;
            }
        }
        public static void SpremiPromjene(List<Auto> auti)
        {
            string put = @"C:\Users\domag\source\repos\AutoSalon\AutoSalon\bin\Debug\BazaPodataka.txt";
            File.WriteAllText(put, string.Empty);
            using (StreamWriter sw = File.AppendText(put))
            {
                foreach (var auto in auti)
                {
                    sw.WriteLine($"{auto.Marka},{auto.Model},{auto.Cijena},{auto.PrviKvartal}");
                }
                sw.Close();
            }  
        }
 
    }
}//bule sort
