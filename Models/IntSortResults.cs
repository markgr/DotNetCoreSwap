using System;
namespace DotNetCoreSwap.Models
{
    /// <summary>
    /// Sorted results model.
    /// </summary>
    public class IntSortResults
    {
        public int[] UnsortedArray { get; set; }
        public int[] SortedArray { get; set; }
        public long Milliseconds{ get; set; }
    }
}
