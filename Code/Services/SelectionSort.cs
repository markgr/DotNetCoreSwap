using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DotNetCoreSwap.Helpers;
using DotNetCoreSwap.Interfaces;
using DotNetCoreSwap.Models;

namespace DotNetCoreSwap.Services
{
    /// <summary>
    /// Class for handling Bubblesort of array of ints
    /// </summary>
    public class SelectionSort : AbstractSort, ISort
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public SelectionSort()
        {

        }

        /// <summary>
        /// Async handler for the class
        /// </summary>
        /// <returns>A refference to an instance of IntSortResults</returns>
        /// <param name="data">Input data</param>
        public async Task<IntSortResults> IntSort(int[] data)
        {
            // Throw if data is null
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            // Perform the sort in a background thread
            var results = await Task.Run(() => IntSortAsync(data));

            // Return reults
            return results;
        }

        /// <summary>
        /// The actual sort routine itself
        /// </summary>
        /// <returns>A refference to an instance of IntSortResults</returns>
        /// <param name="data">Input data</param>
        protected IntSortResults IntSortAsync(int[] data)
        {
            int i;
            int N = data.Length;

            var output = new IntSortResults ();
            Array.Copy(data, output.UnsortedArray = new int[data.Length], data.Length);

            // Start a stopwatch so we can time this
            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();
                                  
            for (i = 0; i < N - 1; i++)
            {
                int k = IntArrayMin(data, i);
                if (i != k)
                    Exchange(data, i, k);
            }

            // Now stop
            watch.Stop();

            output.Milliseconds = watch.ElapsedMilliseconds;
            output.SortedArray = data;

            // Return the sorted array
            return (output);
        }

        /// <summary>
        /// Find the smallest item in the remainder of the array from start
        /// </summary>
        /// <returns>The position of the smallest number</returns>
        /// <param name="data">Initial data</param>
        /// <param name="start">Start position</param>
        protected int IntArrayMin(int[] data, int start)
        {
            int minPos = start;
            for (int pos = start + 1; pos < data.Length; pos++)
                if (data[pos] < data[minPos])
                    minPos = pos;
            return minPos;
        }
    }
}
