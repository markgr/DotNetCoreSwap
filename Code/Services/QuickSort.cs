using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DotNetCoreSwap.Helpers;
using DotNetCoreSwap.Interfaces;
using DotNetCoreSwap.Models;

namespace DotNetCoreSwap.Services
{
    /// <summary>
    /// Class for handling QuickSort of array of ints
    /// </summary>
    public class QuickSort : AbstractSort, ISort
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public QuickSort()
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
            var output = new IntSortResults ();
            Array.Copy(data, output.UnsortedArray = new int[data.Length], data.Length);

            // Start a stopwatch so we can time this
            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();


            IntArrayQuickSort(data, 0, data.Length - 1);

            // Now stop
            watch.Stop();

            output.Milliseconds = watch.ElapsedMilliseconds;
            output.SortedArray = data;

            // Return the sorted array
            return (output);
        }

        /// <summary>
        /// Recursive sort
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="l">Left hand position</param>
        /// <param name="r">Right hand position</param>
        protected void IntArrayQuickSort(int[] data, int l, int r)
        {
            int i, j;
            int x;

            i = l;
            j = r;

            x = data[(l + r) / 2]; /* find pivot item */
            while (true)
            {
                while (data[i] < x)
                    i++;
                while (x < data[j])
                    j--;
                if (i <= j)
                {
                    Exchange(data, i, j);
                    i++;
                    j--;
                }
                if (i > j)
                    break;
            }
            if (l < j)
                IntArrayQuickSort(data, l, j);
            if (i < r)
                IntArrayQuickSort(data, i, r);
        }
    }
}
