using System;
namespace DotNetCoreSwap.Models
{
    /// <summary>
    /// Sorted results model.
    /// </summary>
    public class IntSortResults
    {
        /// <summary>
        /// Gets or sets the unsorted array.
        /// </summary>
        /// <value>The unsorted array.</value>
        public int[] UnsortedArray { get; set; }

        /// <summary>
        /// Gets or sets the sorted array.
        /// </summary>
        /// <value>The sorted array.</value>
        public int[] SortedArray { get; set; }

        /// <summary>
        /// Gets or sets the milliseconds.
        /// </summary>
        /// <value>The milliseconds.</value>
        public long Milliseconds{ get; set; }
    }
}
