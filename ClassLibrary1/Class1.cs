using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ClassLibrary1
{
    [Serializable]
    public class Journal
    {
        public List<Pair<string, double>> results
        {
            get; set;
        }

        public Journal() { results = new List<Pair<string, double>>(); }

        public static bool operator + (Journal obj1, Pair<string, double> obj2)
        {
            Journal a = new Journal();
            obj1.results.Add(obj2);
            return true;
        }

        public IEnumerator<Pair<string, double>> GetEnumerator()
        {
            List<Pair<string, double>> a = new List<Pair<string, double>>(results);
            a.Sort();
            a.Reverse();
            foreach (Pair<string, double> t in a) {
                yield return t;
            }
        }

        public IEnumerable<Pair<string, double>> MyItr(int end)
        {
            List<Pair<string, double>> a = new List<Pair<string, double>>(results);
            a.Sort();
            
            for (int i = 1; i <= end; i++) { 
                yield return a[i];
            }
        }
        static XmlSerializer formatter = new XmlSerializer(typeof(Journal));
        public void MySerialize(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, this);
                Console.WriteLine("Объект сериализован");
            }
        }

        public static Journal MyDeserialize(string path)
        {
            Journal newPerson;
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                newPerson = (Journal)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
            }
            return newPerson;
        }
    }

    [Serializable]
    public class Pair<T, U> : IComparable<Pair<T, U>> where U : IComparable
    {
        public T item1
        {
            get;  set;
        }
        public U item2
        {
            get;  set;
        }
        public Pair()
        {
        }
        public U Uval{
            get {
                return item2;
            }
        }
        public T Tval
        {
            get
            {
                return item1;
            }
        }
        public Pair(T i1, U i2)
        {
            item1 = i1;
            item2 = i2;
        }
        public int CompareTo(Pair<T,U> obj)
        {
            return (item2.CompareTo((obj).Uval));
        }

        public override string ToString()
        {
            return (item1.ToString() + " " + item2.ToString());
        }
    }
}
