using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KA3_Iceland
{
    class Bridge
    {
        public Bridge(ushort island1, ushort island2, string date)
        {
            Island1 = island1;
            Island2 = island2;
            Date = date;
        }
        public ushort Island1 { get; private set; }
        public ushort Island2 { get; private set; }
        public string Date { get; private set; }
    }
    class Program
    {
        static List<ushort> Name = new List<ushort>();
        static List<ushort> Next = new List<ushort>();
        static List<ushort> Size = new List<ushort>();
        static Queue<Bridge> Bridges = new Queue<Bridge>();
        static ushort n; // кол-во островов
        static ushort k; // кол-во мостов

        static void Main(string[] args)
        {
            ReadData();
            var dates_in_st = BoruvkaKraskl();
            OutputData(dates_in_st.Last().Date);
            //Console.ReadKey();
        }

        static List<Bridge> BoruvkaKraskl()
        {
            List<Bridge> MinSpanningTree = new List<Bridge>();
            for (ushort i = 0; i < k; ++i)
            {
                Name.Add(i);
                Size.Add(1);
                Next.Add(i);
            }
            while(MinSpanningTree.Count < n - 1)
            {
                var vw = Bridges.Dequeue();
                var p = Name[vw.Island1];
                var q = Name[vw.Island2];
                if (p != q)
                {
                    if (Size[q] < Size[p])
                    {
                        Merge(vw.Island1, vw.Island2, p, q);
                    }
                    else
                    {
                        Merge(vw.Island2, vw.Island1, q, p);
                    }
                    MinSpanningTree.Add(vw);
                }
            }
            return MinSpanningTree;
        }

        static void Merge(ushort v, ushort w, ushort p, ushort q)
        {
            Name[w] = p;
            var u = Next[w];
            while(Name[u] == q)
            {
                Name[u] = p;
                u = Next[u];
            }
            Size[p] += Size[q];
            var buf = Next[v];
            Next[v] = Next[w];
            Next[w] = buf;
        }

        static void ReadData()
        {
            var data = File.ReadAllLines("in.txt");
            var splitter = new Char[] { ' ' };
            var temp = data[0].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            n = UInt16.Parse(temp[0]);
            k = UInt16.Parse(temp[1]);
            for(ushort i = 1; i <= k; ++i)
            {
                temp = data[i].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                Bridges.Enqueue(new Bridge(UInt16.Parse(temp[0]), UInt16.Parse(temp[1]), temp[2]));
            }
        }
        static void OutputData(string output)
        {
            var sw = new StreamWriter("out.txt");
            sw.Write(output);
            sw.Close();
        }
    }
}
