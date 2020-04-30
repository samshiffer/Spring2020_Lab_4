using System;
using System.Collections;
using System.Collections.Generic;

namespace SkipList2020
{
    public class SkipList<TKey,TValue>:
        IEnumerable<KeyValuePair<TKey,TValue>> 
        where TKey :IComparable<TKey>
    {
        Node<TKey, TValue>[] _head;
        readonly double _probability;
        readonly int _maxLevel;
        int _curLevel;
        Random _rd;
        public int Count { get; private set; }
        public SkipList(int maxLevel = 10, double p= 0.5)
        {
            _maxLevel = maxLevel;
            _probability = p;
            _head = new Node<TKey, TValue>[_maxLevel];
            for (int i = 0; i < maxLevel; i++)
            {
                _head[i] = new Node<TKey, TValue>();
                if (i == 0) continue;
                _head[i - 1].Up = _head[i];
                _head[i].Down = _head[i - 1];
            }

            _curLevel = 0;
            _rd = new Random(DateTime.Now.Millisecond);
        }

        public void Add( TKey key, TValue value)
        {
            var prevNode = new Node<TKey, TValue>[_maxLevel];
            var currentNode = _head[_curLevel];
            for (int i= _curLevel; i>=0; i--)
            {
                while(currentNode.Right!=null && 
                    currentNode.Right.Key.CompareTo(key)<0)
                {
                    currentNode = currentNode.Right;
                }
                prevNode[i] = currentNode;
                if (currentNode.Down == null)
                    break;
                currentNode = currentNode.Down;
            }
            int level = 0;
            while (_rd.NextDouble()< _probability && level<_maxLevel-1)
            {
                level++;
            }
            while (_curLevel<level)
            {
                _curLevel++;
                prevNode[_curLevel] = _head[_curLevel];
            }
            for(int i=0; i<=level; i++)
            {
                var node = new Node<TKey, TValue>(key, value) {Right = prevNode[i].Right};
                prevNode[i].Right = node;
                if (i == 0) continue;
                node.Down = prevNode[i - 1].Right;
                prevNode[i - 1].Right.Up = node;
            }
            Count++;
        }

        public void Remove(TKey key)
        {
            var level = _curLevel;
            var currentNode = _head[level];
            bool removed = false;

            while (level >= 0)
            {
                if (currentNode.Right != null && currentNode.Right.Key.Equals(key))
                {
                    currentNode.Right = currentNode.Right.Right;
                    removed = true;
                }
                else if (currentNode.Right == null || currentNode.Right.Key.CompareTo(key) >= 0)
                {
                    currentNode = currentNode.Down;
                    level--;
                }
                else
                    currentNode = currentNode.Right;
            }


            if (removed)
                Count--;
        }

        public bool Contains(TKey key)
        {
            var level = _curLevel;
            var currentNode = _head[level];

            while (level >= 0)
            {
                if (currentNode.Right != null && currentNode.Right.Key.Equals(key))
                    return true;
                else if (currentNode.Right == null || currentNode.Right.Key.CompareTo(key) > 0)
                {
                    currentNode = currentNode.Down;
                    level--;
                }
                else
                    currentNode = currentNode.Right;
            }
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for(var node = _head[0].Right; node.Right!=null; node=node.Right)
            {
                yield return new KeyValuePair<TKey,TValue>(node.Key, node.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var node = _head[0].Right; node.Right != null; node = node.Right)
            {
                yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
            }
        }

    }
}
