/*************************************************************************
 *  Execution:    algs4 IndexMinPQ
 *
 *  Minimum-oriented indexed PQ implementation using a binary heap.
 *
 * > algs4 IndexMinPQ
 * 3 best
 * 0 it
 * 6 it
 * 4 of
 * 8 the
 * 2 the
 * 5 times
 * 7 was
 * 1 was
 * 9 worst
 * 
 * 3 best
 * 0 it
 * 6 it
 * 4 of
 * 8 the
 * 2 the
 * 5 times
 * 7 was
 * 1 was
 * 9 worst
 * 
 *********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Algs4
{
    /// <summary>
    /// The <c>IndexMinPQ</c> class represents an indexed priority queue of generic keys.
    /// It supports the usual insert and delete-the-minimum
    /// operations, along with delete and change-the-key 
    /// methods. In order to let the client refer to items on the priority queue,
    /// an integer between 0 and NMAX-1 is associated with each key the client
    /// uses this integer to specify which key to delete or change.
    /// It also supports methods for peeking at the minimum key,
    /// testing if the priority queue is empty, and iterating through
    /// the keys.
    /// The insert, delete-the-minimum, delete,
    /// change-key, decrease-key, and increase-key
    /// operations take logarithmic time.
    /// The is-empty, size, min-index, min-key, and key-of
    /// operations take constant time.
    /// Construction takes time proportional to the specified capacity.
    /// This implementation uses a binary heap along with an array to associate
    /// keys with integers in the given range.
    /// For additional documentation, see <a href="http://algs4.cs.princeton.edu/24pq">Section 2.4</a> of
    /// <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
    /// </summary>
    public class IndexMinPQ<Key> : IEnumerable<int> where Key : IComparable<Key>
    {
        private readonly int NMAX; // maximum number of elements on PQ
        private int N; // number of elements on PQ
        private readonly int[] pq; // binary heap using 1-based indexing
        private readonly int[] qp; // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
        private readonly Key[] keys; // keys[i] = priority of i

        /// <summary>
        /// Create an empty indexed priority queue with indices between 0 and NMAX-1.
        /// </summary>
        public IndexMinPQ(int NMAX)
        {
            if (NMAX < 0) throw new ArgumentException();

            this.NMAX = NMAX;

            keys = new Key[NMAX + 1]; // make this of length NMAX??
            pq = new int[NMAX + 1];
            qp = new int[NMAX + 1]; // make this of length NMAX??

            for (int i = 0; i <= NMAX; i++)
            {
                qp[i] = -1;
            }
        }

        /// <summary>
        /// Is the priority queue empty?
        /// </summary>
        public bool IsEmpty()
        {
            return N == 0;
        }

        /// <summary>
        /// Is i an index on the priority queue?
        /// </summary>
        public bool Contains(int i)
        {
            if (i < 0 || i >= NMAX) throw new ArgumentOutOfRangeException();

            return qp[i] != -1;
        }

        /// <summary>
        /// Return the number of keys on the priority queue.
        /// </summary>
        public int Size()
        {
            return N;
        }

        /// <summary>
        /// Associate key with index i.
        /// </summary>
        public void Insert(int i, Key key)
        {
            if (i < 0 || i >= NMAX) throw new ArgumentOutOfRangeException();
            if (Contains(i)) throw new ArgumentException("index is already in the priority queue");
            N++;
            qp[i] = N;
            pq[N] = i;
            keys[i] = key;
            Swim(N);
        }

        /// <summary>
        /// Return the index associated with a minimal key.
        /// </summary>
        public int MinIndex()
        {
            if (N == 0) throw new KeyNotFoundException("Priority queue underflow");
            return pq[1];
        }

        /// <summary>
        /// Return a minimal key.
        /// </summary>
        public Key MinKey()
        {
            if (N == 0) throw new KeyNotFoundException("Priority queue underflow");
            return keys[pq[1]];
        }

        /// <summary>
        /// Delete a minimal key and return its associated index.
        /// </summary>
        public int DelMin()
        {
            if (N == 0) throw new KeyNotFoundException("Priority queue underflow");
            int min = pq[1];
            Exch(1, N--);
            Sink(1);
            qp[min] = -1; // delete
            keys[pq[N + 1]] = default(Key); // to help with garbage collection
            pq[N + 1] = -1; // not needed
            return min;
        }

        /// <summary>
        /// Return the key associated with index i.
        /// </summary>
        public Key KeyOf(int i)
        {
            if (i < 0 || i >= NMAX) throw new ArgumentOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            return keys[i];
        }

        /// <summary>
        /// Change the key associated with index i to the specified value.
        /// </summary>
        [Obsolete]
        public void Change(int i, Key key)
        {
            ChangeKey(i, key);
        }

        /// <summary>
        /// Change the key associated with index i to the specified value.
        /// </summary>
        public void ChangeKey(int i, Key key)
        {
            if (i < 0 || i >= NMAX) throw new ArgumentOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            keys[i] = key;
            Swim(qp[i]);
            Sink(qp[i]);
        }

        /// <summary>
        /// Decrease the key associated with index i to the specified value.
        /// </summary>
        public void DecreaseKey(int i, Key key)
        {
            if (i < 0 || i >= NMAX) throw new ArgumentOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            if (keys[i].CompareTo(key) <= 0) throw new ArgumentException("Calling decreaseKey() with given argument would not strictly decrease the key");
            keys[i] = key;
            Swim(qp[i]);
        }

        /// <summary>
        /// Increase the key associated with index i to the specified value.
        /// </summary>
        public void IncreaseKey(int i, Key key)
        {
            if (i < 0 || i >= NMAX) throw new ArgumentOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            if (keys[i].CompareTo(key) >= 0) throw new ArgumentException("Calling increaseKey() with given argument would not strictly increase the key");
            keys[i] = key;
            Sink(qp[i]);
        }

        /// <summary>
        /// Delete the key associated with index i.
        /// </summary>
        public void Delete(int i)
        {
            if (i < 0 || i >= NMAX) throw new ArgumentOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            int index = qp[i];
            Exch(index, N--);
            Swim(index);
            Sink(index);
            keys[i] = default(Key);
            qp[i] = -1;
        }


        /**************************************************************
         * General helper functions
         **************************************************************/

        private bool Greater(int i, int j)
        {
            return keys[pq[i]].CompareTo(keys[pq[j]]) > 0;
        }

        private void Exch(int i, int j)
        {
            int swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
            qp[pq[i]] = i;
            qp[pq[j]] = j;
        }


        /**************************************************************
         * Heap helper functions
         **************************************************************/

        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exch(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && Greater(j, j + 1)) j++;
                if (!Greater(k, j)) break;
                Exch(k, j);
                k = j;
            }
        }


        /***********************************************************************
         * Iterators
         **********************************************************************/

        /// <summary>
        /// Return an iterator that iterates over all of the elements on the
        /// priority queue in ascending order.
        /// The iterator doesn't implement <c>remove()</c> since it's optional.
        /// </summary>
        public IEnumerator<int> GetEnumerator()
        {
            return new HeapIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class HeapIterator : IEnumerator<int>
        {
            // create a new pq
            private readonly IndexMinPQ<Key> copy;

            // add all elements to copy of heap
            // takes linear time since already in heap order so no keys move
            public HeapIterator(IndexMinPQ<Key> minPQ)
            {
                copy = new IndexMinPQ<Key>(minPQ.pq.Length - 1);

                for (int i = 1; i <= minPQ.N; i++)
                    copy.Insert(minPQ.pq[i], minPQ.keys[minPQ.pq[i]]);
            }

            public bool MoveNext()
            {
                if (!copy.IsEmpty())
                {
                    Current = copy.DelMin();
                    return true;
                }

                return false;
            }

            public int Current { get; private set; }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void Dispose() { }
        }
    }

    public class IndexMinPQExample
    {
        public static void Main(string[] args)
        {
            // insert a bunch of strings
            string[] strings = { "it", "was", "the", "best", "of", "times", "it", "was", "the", "worst" };

            var pq = new IndexMinPQ<string>(strings.Length);
            for (int i = 0; i < strings.Length; i++)
            {
                pq.Insert(i, strings[i]);
            }

            // delete and print each key
            while (!pq.IsEmpty())
            {
                int i = pq.DelMin();
                Console.WriteLine(i + " " + strings[i]);
            }
            Console.WriteLine();

            // reinsert the same strings
            for (int i = 0; i < strings.Length; i++)
            {
                pq.Insert(i, strings[i]);
            }

            // print each key using the iterator
            foreach (int i in pq)
            {
                Console.WriteLine(i + " " + strings[i]);
            }

            while (!pq.IsEmpty())
            {
                pq.DelMin();
            }
        }
    }
}