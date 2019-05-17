using System;
namespace DotNetCoreSwap.Models
{
    public class IntSortResults
    {
        public int[] UnsortedArray { get; set; }
        public int[] SortedArray { get; set; }
        public long Milliseconds{ get; set; }
    }
}
