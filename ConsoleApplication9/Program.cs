using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using System.IO;
namespace ConsoleApplication9
{
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            int counter = 0;
            string line;
            
            StreamReader file = new StreamReader("1.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                string[] words = line.Split(new char[] { ' ' });
                double sred = 0;
                for (int j=1; j<words.Length; j++)
                {
                    sred += double.Parse(words[j]);
                }
                sred = sred / (words.Length - 1);
                Pair<string, double> p = new Pair<string, double>(words[0], sred);
                bool a = journal + p;
                counter++;
            }

            journal.MySerialize("out.ser");
            Journal spisok = Journal.MyDeserialize("out.ser");
            foreach (Pair<string, double> t in spisok)
            {
                Console.WriteLine(t);
            }
            int n = 3;
            Console.WriteLine();
            foreach (Pair<string, double> t in spisok.MyItr(n))
            {
                Console.WriteLine(t);
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);

            Console.ReadKey();
        }
    }
}
