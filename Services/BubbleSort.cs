using System;
using System.Threading.Tasks;
using DotNetCoreSwap.Helpers;
using DotNetCoreSwap.Interfaces;
using DotNetCoreSwap.Models;

namespace DotNetCoreSwap.Services
{
    public class BubbleSort : AbstractSort, ISort
    {
        public BubbleSort()
        {

        }

        public async Task<IntSortResults> IntSort(int[] data)
        {
            var results = await Task.Run(() => IntSortAsync(data));
            return results;
        }

        protected IntSortResults IntSortAsync(int[] data)
        {
            int i, j;
            int N = data.Length;


            for (j = N - 1; j > 0; j--)
            {
                for (i = 0; i < j; i++)
                {
                    if (data[i] > data[i + 1])
                        Exchange(data, i, i + 1);
                }
            }

            return (new IntSortResults { SortedArray = data });
        }
    }
}
