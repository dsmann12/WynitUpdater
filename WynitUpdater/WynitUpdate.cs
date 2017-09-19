using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WynitUpdater
{
    static class WynitUpdate
    {
        private static List<string> skus = new List<string>();
        private static Dictionary<string, string> wynit = new Dictionary<string, string>();

        public static void LoadSkus()
        {
            try
            {
                StreamReader f = new StreamReader("wynitskus.csv");
                while (!f.EndOfStream)
                {
                    string line = f.ReadLine();
                    skus.Add(line);
                }
                Console.WriteLine(skus[0]);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ReadWynitFile(string filename)
        {
            try
            {
                StreamReader f = new StreamReader(filename);
                while (!f.EndOfStream)
                {
                    string[] row = f.ReadLine().Split('\t');
                    wynit.Add(row[0].Substring(1, row[0].Length - 2), row[5]);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                wynit.Clear();
            }
            catch(ArgumentException ex)
            {
                wynit.Clear();
                ReadWynitFile(filename);
                Console.WriteLine(ex);
            }
        }

        public static void GenerateFile()
        {
            using (StreamWriter w = File.CreateText("WynitQty.csv"))
            {
                foreach (string sku in skus)
                {
                    try
                    {
                        string value = wynit[sku.Substring(4)];
                        w.WriteLine("{0}, {1}", sku, value);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        w.WriteLine("{0}, 0", sku);
                    }
                }
            }
        }
    }
}
