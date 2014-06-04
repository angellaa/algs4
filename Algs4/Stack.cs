using StdLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace algs
{
    /// <summary>
    ///  Implements the stack data structure with generic
    /// 
    ///  The <tt>Stack</tt> class represents a last-in-first-out (LIFO) stack of generic items.
    ///  It supports the usual <em>push</em> and <em>pop</em> operations, along with methods
    ///  for peeking at the top item, testing if the stack is empty, and iterating through
    ///  the items in LIFO order.
    ///  <p>
    ///  This implementation uses a singly-linked list with a static nested class for
    ///  linked-list nodes. See {@link LinkedStack} for the version from the
    ///  textbook that uses a non-static nested class.
    ///  The <em>push</em>, <em>pop</em>, <em>peek</em>, <em>size</em>, and <em>is-empty</em>
    ///  operations all take constant time in the worst case.
    ///  <p>
    ///  For additional documentation, see <a href="/algs4/13stacks">Section 1.3</a> of
    ///  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
    ///
    ///  <author>Robert Sedgewick</author>
    ///  <author>Kevin Wayne</author>
    ///
    /// </summary>
    /// <typeparam name="Item">generic data type. e.g. int, string etc.</typeparam>
    public class Stack<Item> : IEnumerable<Item>
    {
        /// <summary>
        /// subclass in order to comply with generics - helper linked list class
        /// </summary>
        private class Node
        {
            public Item Data;
            public Node Next;
        }
        
        /// <summary>
        /// Returns the number of items in the stack.
        /// </summary>
        public int Size { get; private set; }
        private Node first;

        /// <summary>
        /// Constructor that allows us to pass array of items
        /// </summary>
        /// <param name="objectsToPush">generic data passed into stack</param>
        public Stack(params Item[] objectsToPush)
        {
            foreach (var item in objectsToPush)
            {
                Push(item);
            }
        }

        /// <summary>
        /// Initializes an empty stack.
        /// </summary>
        public Stack()
        {
            first = null;
            Size = 0;
        }

        /// <summary>
        /// Is this stack empty?
        /// </summary>
        /// <returns> true if this stack is empty; false otherwise</returns>
        public bool IsEmpty()
        {
            return first == null;
        }

        /// <summary>
        /// Removes and returns the item most recently added to this stack.
        /// </summary>
        /// <throws>IndexOutOfRangeException if this stack is empty</throws>
        /// <returns>the item most recently added</returns>
        public Item Pop()
        {
            if (IsEmpty())
            {
                throw new IndexOutOfRangeException("Stack underflow");
            }

            var poped = first;
            first = first.Next;

            return poped.Data;
        }

        /// <summary>
        /// Adds the item to this stack.
        /// </summary>
        /// <param name="data">data to add</param>
        public void Push(Item data)
        {
            var oldNode = first;
            first = new Node();
            first.Data = data;
            first.Next = oldNode;

            Size++;
        }


        /// <summary>
        /// Peek should get the value from the top but don't POP!
        /// 
        /// 1 -> 3 -> 4 -> null
        ///           ^
        ///           | first
        /// 
        /// Peek will just return 4 but leave the node in Stack!    
        /// <summary>
        /// Returns (but does not remove) the item most recently added to this stack.
        /// </summary>
        /// <throws>IndexOutOfRangeException if the stack is empty</throws>
        /// <returns>return the item most recently added to this stack</returns>
        public Item Peek()
        {
            if (IsEmpty())
            {
                throw new IndexOutOfRangeException("Stack underflow");
            }

            return first.Data;
        }

      
        /// <summary>
        /// Implements IEnumerable of the stack
        /// </summary>
        /// <returns>Enumerator - Iterator</returns>
        public IEnumerator<Item> GetEnumerator()
        {
            return new _ListIterator(first);
        }

        /// <summary>
        /// Implements IEnumerable of the stack
        /// </summary>
        /// <returns>Enumerator - Iterator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// IEnumerator implementation.
        /// 
        /// Returns an iterator to this stack that iterates through the items in LIFO order.
        /// </summary>
        private class _ListIterator : IEnumerator<Item> 
        {
            private Node _CurrentInIterator;
            private Node _PointToFirst;

            /// <summary>
            /// Enumerator(Iterator) for stack.
            /// </summary>
            /// <param name="first">last added item in the stack</param>
            public _ListIterator(Node first)
            {
                _PointToFirst = new Node {Next = first};
                _CurrentInIterator = _PointToFirst;
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
                    if (_CurrentInIterator.Next != null)
                    {
                        _CurrentInIterator = _CurrentInIterator.Next;
                        return true;
                    }

                    return false;
                }

                /// <summary>
                /// reset to the initial state
                /// </summary>
                public void Reset()
                {
                    _CurrentInIterator = _PointToFirst;
                }

                /// <summary>
                /// get the current available and valid generic from node
                /// </summary>
                public Item Current
                {
                    get { return (_CurrentInIterator != null) ? _CurrentInIterator.Data : default(Item); }
                }

                /// <summary>
                /// IEnumerator<T> implements IEnumerator, so at the most basic level we have to fulfill the contract.
                /// </summary>
                object IEnumerator.Current
                {
                    get { return this.Current; }
                }
        }

        
        /// <summary>
        /// Unit tests the <tt>Stack</tt> data type.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {
            Stack<String> s = new Stack<String>();
            while (!StdIn.IsEmpty())
            {
                String item = StdIn.ReadString();
                if (!item.Equals("-")) s.Push(item);
                else if (!s.IsEmpty())
                {
                  Console.WriteLine(s.Pop() + " ");
                  Debug.WriteLine(s.Pop() + " ");
                }
            }
            Console.WriteLine("(" + s.Size + " left on stack)");
            Debug.WriteLine("(" + s.Size + " left on stack)");
        }
    }
}
