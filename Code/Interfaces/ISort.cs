using System.Threading.Tasks;
using DotNetCoreSwap.Models;

namespace DotNetCoreSwap.Interfaces
{
    /// <summary>
    /// ISort interface
    /// </summary>
    public interface ISort
    {
        /// <summary>
        /// Sort ints
        /// </summary>
        /// <returns>The sorted array</returns>
        /// <param name="data">Data.</param>
        Task<IntSortResults> IntSort(int[] data);
    }
}
