using StdLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algs4
{
    ///  The <tt>Queue</tt> class represents a first-in-first-out (FIFO)
    ///  queue of generic items.
    ///  It supports the usual <em>enqueue</em> and <em>dequeue</em>
    ///  operations, along with methods for peeking at the first item,
    ///  testing if the queue is empty, and iterating through
    ///  the items in FIFO order.
    ///  <p>
    ///  This implementation uses a singly-linked list with a static nested class for
    ///  linked-list nodes. See {@link LinkedQueue} for the version from the
    ///  textbook that uses a non-static nested class.
    ///  The <em>enqueue</em>, <em>dequeue</em>, <em>peek</em>, <em>size</em>, and <em>is-empty</em>
    ///  operations all take constant time in the worst case.
    ///  <p>
    ///  For additional documentation, see <a href="http://algs4.cs.princeton.edu/13stacks">Section 1.3</a> of
    ///  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
    ///
    ///  <author>Robert Sedgewick</author>
    ///  <author>Kevin Wayne</author>
    ///
    /// <typeparam name="Item">generic data type. e.g. int, string etc.</typeparam>    
    public class Queue<Item> : IEnumerable<Item>
     {
        /// <summary>
        /// subclass in order to comply with generics - helper linked list class
        /// </summary>
        private class Node
        {
            public Item Data;
            public Node Next;
        }

        private Node first;
        private Node last;

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        public int Size { get; private set;}

        /// <summary>
        /// Initializes an empty queue.
        /// </summary>
        public Queue()
        {
            Size = 0;
        }

        /// <summary>
        /// Constructor that allows us to pass array of items
        /// </summary>
        /// <param name="objectsToEnqueue">generic data passed into queue</param>
        public Queue(params Item[] objectsToEnqueue) : this()
        {
            foreach (var item in objectsToEnqueue)
            {
                this.Enqueue(item);
            }
        }

        /// <summary>
        /// Is this queue empty?
        /// </summary>
        /// <returns> true if this queue is empty; false otherwise</returns>        
        public bool IsEmpty()
        {
            return first == null;
        }

        /// <summary>
        /// Returns the item least recently added to this queue.
        /// </summary>
        /// <throws>IndexOutOfRangeException if this queue is empty</throws>
        /// <returns>the item least recently added to this queue</returns>
        public Item Peek()
        {
            if (IsEmpty())
            {
                throw new IndexOutOfRangeException("Queue underflow");
            }

            return first.Data;
        }

        /// <summary>
        /// Adds the item to this queue.
        /// </summary>
        /// <param name="data">generic data structure to add into queue</param>
        public void Enqueue(Item data)
        {
            var oldLast = last;
            last = new Node();
            last.Data = data;

            
            if (IsEmpty())
            {
                first = last;
            }
            else
            {
               oldLast.Next = last;    
            }

            Size++;
        }

        /// <summary>
        /// Removes and returns the item on this queue that was least recently added.
        /// </summary>
        /// <returns>the item on this queue that was least recently added</returns>
        public Item Dequeue()
        {
           if (IsEmpty())
           {
               throw new IndexOutOfRangeException("Queue underflow");
           }

            var oldFirst = first;
            first = first.Next;

            Size--;

            return oldFirst.Data;
        }
        
        /// <summary>
        /// Implements IEnumerable of the queue
        /// </summary>
        /// <returns>Enumerator - Iterator</returns>
         public IEnumerator<Item> GetEnumerator()
        {
            return new _ListIterator(first);
        }

         /// <summary>
         /// Implements IEnumerable of the queue
         /// </summary>
         /// <returns>Enumerator - Iterator</returns>
         IEnumerator IEnumerable.GetEnumerator()
         {
             return GetEnumerator();
         }

        /// <summary>
        /// IEnumerator implementation.
        /// 
         /// Returns an iterator to this queue that iterates through the items in FIFO order.
        /// </summary>
        private class _ListIterator : IEnumerator<Item>
        {
            private readonly Node _PointToFirst;
            private Node _CurrentNodeInListIterator;

            public _ListIterator(Node first)
            {
                _PointToFirst = new Node();
                _PointToFirst.Next = first;
                _CurrentNodeInListIterator = _PointToFirst;
            }


            public void Dispose()
            {
             //left blank
            }

            /// <summary>
            /// are we able to move to next item?
            /// If yes, move to next item and expose it in _CurrentInIterator
            /// </summary>
            /// <returns>true - moved to the next item, otherwise false</returns>
            public bool MoveNext()
            {
                if (_CurrentNodeInListIterator.Next!=null)
                {
                    _CurrentNodeInListIterator = _CurrentNodeInListIterator.Next;
                    return true;
                }

                return false;
            }

            /// <summary>
            /// reset to the initial state
            /// </summary>
            public void Reset()
            {
                _CurrentNodeInListIterator = _PointToFirst;
            }

            /// <summary>
            /// get the current available and valid generic from node
            /// </summary>
            public Item Current
            {
                get { return _CurrentNodeInListIterator.Data; }
            }

            /// <summary>
            /// IEnumerator<T> implements IEnumerator, so at the most basic level we have to fulfill the contract.
            /// </summary>
            object IEnumerator.Current
            {
                get { return Current; }
            }
        }

        /// <summary>
        /// Unit tests the Queue data type.
        /// </summary>
        /// <param name="args"></param>        
        public static void Main(String[] args)
        {
            Queue<String> q = new Queue<String>();
            while (!StdIn.IsEmpty())
            {
                String item = StdIn.ReadString();
                if (!item.Equals("-")) q.Enqueue(item);
                else if (!q.IsEmpty())
                {
                    Console.WriteLine(q.Dequeue() + " ");
                    Debug.WriteLine(q.Dequeue() + " ");
                }
            }
            Console.WriteLine("(" + q.Size + " left on queue)");
            Debug.WriteLine("(" + q.Size + " left on queue)");            
        }
     }
}
