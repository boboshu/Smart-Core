using System;
using System.Collections.Generic;

namespace Smart.Extensions
{
    public static class LinkedListExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static void Add<T>(this LinkedList<T> list, T value)
        {
            list.AddFirst(value);
        }

        public static void Remove<T>(this LinkedList<T> list, Func<T, bool> filter)
        {
            var node = list.First;
            while (node != null)
            {
                if (filter(node.Value)) { list.Remove(node); return; }
                node = node.Next;
            }
        }

        public static void RemoveAll<T>(this LinkedList<T> list, Func<T, bool> filter)
        {
            var node = list.First;
            while (node != null)
            {
                var next = node.Next;
                if (filter(node.Value)) list.Remove(node);
                node = next;
            }
        }

        public static void SafeDo<T>(this LinkedList<T> list, Action<T> action)
        {
            var node = list.First;
            while (node != null)
            {
                action(node.Value);
                node = node.Next;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}