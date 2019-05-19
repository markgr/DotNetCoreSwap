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
    public class BubbleSort : AbstractSort, ISort
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public BubbleSort()
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
            int i, j;
            int N = data.Length;

            var output = new IntSortResults ();
            Array.Copy(data, output.UnsortedArray = new int[data.Length], data.Length);

            // Start a stopwatch so we can time this
            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();


            for (j = N - 1; j > 0; j--)
            {
                for (i = 0; i < j; i++)
                {
                    if (data[i] > data[i + 1])
                        Exchange(data, i, i + 1);
                }
            }

            // Now stop
            watch.Stop();

            output.Milliseconds = watch.ElapsedMilliseconds;
            output.SortedArray = data;

            // Return the sorted array
            return (output);
        }
    }
}
