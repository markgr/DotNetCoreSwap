using System;
using System.Threading.Tasks;
using DotNetCoreSwap.Models;

namespace DotNetCoreSwap.Interfaces
{
    public interface ISort
    {
        Task<IntSortResults> IntSort(int[] data);
    }
}
