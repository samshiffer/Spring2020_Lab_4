using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkipList2020;

namespace SkipListUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountIncreaseAfterAdding()
        {
            int n = 10;
            var lib = new SkipList<int, int>();

            for(int i=0; i<n; i++)
            {
                lib.Add(i, i);
            }
            Assert.AreEqual(n, lib.Count);
        }
        [TestMethod]
        public void ItemsInSkipListAreSortedProperly()
        {
            var lib = new SkipList<int, int>();
            var nums = new List<int>(new[] { 44, 22, 1 , 56, 3, 90, 31, 15, 26 });
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                lib.Add(nums[i], i);
            }
            nums.Sort();
            int j = 0;
            foreach(var pair in lib)
            {
                Assert.AreEqual(nums[j], pair.Key);
                j++;
            }
            Assert.AreEqual(n, lib.Count);
        }

        [TestMethod]
        public void NewSkipListCountIsZero()
        {
            var lib = new SkipList<int, int>();
            Assert.AreEqual(0,lib.Count);
        }

        [TestMethod]
        public void ItemsExistAfterAdding()
        {
            var lib = new SkipList<int,int>();
            for (int i = 0; i < 10; i++)
            {
                lib.Add(i,i);
            }

            for (int i = 9; i >= 0; i--)
            {
                Assert.IsTrue(lib.Contains(i));
            }
        }

        [TestMethod]
        public void ItemsDoesntExistAfterRemoving()
        {
            var lib = new SkipList<int, int>();
            for (int i = 0; i < 10; i++)
            {
                lib.Add(i, i);
            }
            for (var i = 0; i < 10; i ++)
            {
                lib.Remove(i);
            }
            for (int i = 9; i >= 0; i--)
            {
                Assert.IsTrue(!lib.Contains(i));
            }
        }

        [TestMethod]
        public void CountChangesAfterRemoving()
        {
            var lib = new SkipList<int, int>();
            for (var i = 0; i < 10; i++)
            {
                lib.Add(i, i);
            }

            for (var i = 0; i < 10; i += 2)
            {
                lib.Remove(i);
            }

            Assert.AreEqual(lib.Count, 5);
        }
        [TestMethod]
        public void ItemsExistAfterOthersAreRemoved()
        {
            var lib = new SkipList<int, int>();
            for (var i = 0; i < 10; i++)
            {
                lib.Add(i, i);
            }

            for (var i = 0; i < 10; i += 2)
            {
                lib.Remove(i);
            }

            for (var i = 0; i < 10; i += 2)
            {
                Assert.IsFalse(lib.Contains(i));
                Assert.IsTrue(lib.Contains(i + 1));
            }
        }
        [TestMethod]
        public void ItemsExistAfterOthersAreRemoved2()
        {
            var a = new List<int> {3, 82, 17, 2, 100, 65, 23};
            var lib = new SkipList<int, int>();
            for (var i = 0; i < a.Count; i++)
            {
                lib.Add(a[i], i);
            }

            var b = new List<int>();
            
            for (var i = 0; i < a.Count; i += 2)
            {
                lib.Remove(a[i]);
                b.Add(a[i]);
            }

            foreach (var item in b)
            {
                a.Remove(item);
            }
            a.Sort();
            int j = 0;
            foreach (var item in lib )
            {   
                Assert.AreEqual(item.Key, a[j]);
                j++;
            }
            //for (var i = 0; i < a.Count; i++)
            //{
            //    Assert.IsTrue();
            //    Assert.IsFalse(lib.Contains(i));
            //    Assert.IsTrue(lib.Contains(i + 1));
            //}
        }
    }
}
