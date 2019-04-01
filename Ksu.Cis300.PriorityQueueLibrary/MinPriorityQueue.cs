/* MinPriorityQueue.cs
 * Author: Antonio Carter
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.PriorityQueueLibrary
{
    /// <summary>
    /// Instances of this class utilize a Min Priority Queue
    /// </summary>
    /// <typeparam name="TPriority"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class MinPriorityQueue<TPriority, TValue> where TPriority : IComparable<TPriority>
    {
        /// <summary>
        /// A leftist heap storing the elements and their priorities.
        /// </summary>
        private LeftistTree<KeyValuePair<TPriority, TValue>> _elements = null;

        /// <summary>
        /// Gets the number of elements.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Adds the given element with the given priority.
        /// </summary>
        /// <param name="p">The priority of the element.</param>
        /// <param name="x">The element to add.</param>
        public void Add(TPriority p, TValue x)
        {
            LeftistTree<KeyValuePair<TPriority, TValue>> node =
                new LeftistTree<KeyValuePair<TPriority, TValue>>(new KeyValuePair<TPriority, TValue>(p, x), null, null);
            _elements = Merge(_elements, node);
            Count++;
        }

        /// <summary>
        /// Merges the given leftist heaps into one leftist heap.
        /// </summary>
        /// <param name="h1">One of the leftist heaps to merge.</param>
        /// <param name="h2">The other leftist heap to merge.</param>
        /// <returns>The resulting leftist heap.</returns>
        public static LeftistTree<KeyValuePair<TPriority, TValue>> Merge(LeftistTree<KeyValuePair<TPriority, TValue>> h1,
            LeftistTree<KeyValuePair<TPriority, TValue>> h2)
        {
            if (h1 == null)
            {
                return h2;
            }

            if (h2 == null)
            {
                return h1;
            }

            TPriority h1Priority = h1.Data.Key;
            TPriority h2Priority = h2.Data.Key;
            TPriority priority;
            TValue value;
            LeftistTree<KeyValuePair<TPriority, TValue>> large;
            LeftistTree<KeyValuePair<TPriority, TValue>> small;

            if (h1Priority.CompareTo(h2Priority) <= 0)
            {
                priority = h1Priority;
                value = h1.Data.Value;
                large = h2;
                small = h1;
            }

            else
            {
                priority = h2Priority;
                value = h2.Data.Value;
                large = h1;
                small = h2;
            }

            KeyValuePair<TPriority, TValue> newRoot = new KeyValuePair<TPriority, TValue>(priority, value);
            LeftistTree<KeyValuePair<TPriority, TValue>> newTree = new LeftistTree<KeyValuePair<TPriority, TValue>>(newRoot, small.LeftChild, Merge(small.RightChild, large));
            return newTree;
        }

        /// <summary>
        /// This property gets the priority stored in the root of the leftist heap
        /// </summary>
        public TPriority MinimumPriority
        {
            get
            {
                // Code to check the error condition and return the minimum priority.
                if(Count == 0)
                {
                    throw new InvalidOperationException();
                }

                return _elements.Data.Key;
                
            }
        }

        /// <summary>
        /// This method removes the element with minimum priority and returns that element 
        /// </summary>
        /// <returns>The element with the minimum priority</returns>
        public TValue RemoveMinimumPriority()
        {
            TValue value; 
            if(Count == 0)
            {
                throw new InvalidOperationException();

            }
            else
            {
                value = _elements.Data.Value;
                _elements = Merge(_elements.LeftChild, _elements.RightChild);
                Count--;
                return value;
            }
        }


    }
}
