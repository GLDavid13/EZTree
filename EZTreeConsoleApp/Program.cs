using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace db.EZTreeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var json = Properties.Resources.ZeTree;
            var jsonTree = JArray.Parse(Encoding.Default.GetString(json));
            watch.Stop();
            Console.WriteLine("Init: " + watch.ElapsedMilliseconds + " ms");
            var times1 = new List<long>();
            var times2 = new List<long>();
            for(int i = 0; i < 10; i++)
            {
                watch.Start();
                var eztree = MyTree.BuildTree(jsonTree);
                watch.Stop();
                times1.Add(watch.ElapsedMilliseconds);
                Console.WriteLine("Building tree N: " + watch.ElapsedMilliseconds + " ms");
                watch.Reset();
            }
            for(int i = 0; i < 10; i++)
            {
                watch.Start();
                var eztree = MyTree.BuildTreeFast(jsonTree);
                watch.Stop();
                times2.Add(watch.ElapsedMilliseconds);
                Console.WriteLine("Building tree F: " + watch.ElapsedMilliseconds + " ms");
                watch.Reset();
            }
            Console.WriteLine("Average time usual: "+times1.Average()+" ms");
            Console.WriteLine("Average time usual: "+times2.Average()+" ms");
        }
    }
}
