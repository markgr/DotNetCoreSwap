using System.Threading.Tasks;
using DotNetCoreSwap.Models;

namespace DotNetCoreSwap.Interfaces
{
    /// <summary>
    /// ISort interface
    /// </summary>
    public interface ISort
    {
        Task<IntSortResults> IntSort(int[] data);
    }
}
