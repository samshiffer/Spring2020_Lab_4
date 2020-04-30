using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SkipList2020;

namespace SkipListComparision
{
    class Program
    {
        static void Main()
        {
            var randomSet = new HashSet<int>();
            var rd = new Random();

            for (var i = 0; i < 10000; i++)
                while(!randomSet.Add(rd.Next(0, 10000)));

            var randomArray = randomSet.ToArray();

            var skiplist = new SkipList<int,int>();
            var sortedlist = new SortedList<int,int>();

            var sw = new Stopwatch();
            sw.Start();

            foreach (var item in randomArray)
                skiplist.Add(item,item);

            for (var i = 5000; i <= 7000; i++) 
                skiplist.Remove(randomArray[i]);

            foreach (var item in randomArray) 
                skiplist.Contains(item); 

            sw.Stop();

            Console.WriteLine("Время работы SkipList: {0}", sw.ElapsedMilliseconds);


            sw.Start();
            foreach (var item in randomArray)
                sortedlist.Add(item, item);

            for (var i = 5000; i <= 7000; i++)
                sortedlist.Remove(randomArray[i]);

            foreach (var item in randomArray)
                sortedlist.ContainsKey(item);
            sw.Stop();

            Console.WriteLine("Время работы SortedList: {0}", sw.ElapsedMilliseconds);
        }
    }
}
