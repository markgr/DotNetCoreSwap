using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSwap.Controllers;
using TestSwap.Mocks;
using Xunit;

namespace TestSwap
{
    /// <summary>
    /// Runs tests agains the various sort services
    /// </summary>
    public class SortServiceTests
    {
        /// <summary>
        /// Tests the length of the bubble sort.
        /// </summary>
        /// <returns>The bubble sort length.</returns>
        [Fact(DisplayName = "Bubble sort return length is same as sent")]
        public async Task TestBubbleSortLength()
        {
            Random r = new Random();
            var items = Enumerable.Range(0, r.Next(500)).Select(x => r.Next(1000)).ToArray();
            var sort = new DotNetCoreSwap.Services.BubbleSort();
            var response = await sort.IntSort(items);
            Assert.True(response.SortedArray.Length == items.Length);
        }

        /// <summary>
        /// Tests the length of the insertion sort.
        /// </summary>
        /// <returns>The insertion sort length.</returns>
        [Fact(DisplayName = "Insertion sort return length is same as sent")]
        public async Task TestInsertionsSortLength()
        {
            Random r = new Random();
            var items = Enumerable.Range(0, r.Next(500)).Select(x => r.Next(1000)).ToArray();
            var sort = new DotNetCoreSwap.Services.InsertionSort();
            var response = await sort.IntSort(items);
            Assert.True(response.SortedArray.Length == items.Length);
        }

        /// <summary>
        /// Tests the length of the quick sort.
        /// </summary>
        /// <returns>The quick sort length.</returns>
        [Fact(DisplayName = "Quick sort return length is same as sent")]
        public async Task TestQuickSortLength()
        {
            Random r = new Random();
            var items = Enumerable.Range(0, r.Next(500)).Select(x => r.Next(1000)).ToArray();
            var sort = new DotNetCoreSwap.Services.QuickSort();
            var response = await sort.IntSort(items);
            Assert.True(response.SortedArray.Length == items.Length);
        }

        /// <summary>
        /// Tests the length of the selection sort.
        /// </summary>
        /// <returns>The selection sort length.</returns>
        [Fact(DisplayName = "Selection sort return length is same as sent")]
        public async Task TestSelectionSortLength()
        {
            Random r = new Random();
            var items = Enumerable.Range(0, r.Next(500)).Select(x => r.Next(1000)).ToArray();
            var sort = new DotNetCoreSwap.Services.SelectionSort();
            var response = await sort.IntSort(items);
            Assert.True(response.SortedArray.Length == items.Length);
        }

        /// <summary>
        /// Tests the empty array bubble sort.
        /// </summary>
        /// <returns>Assert for the exception.</returns>
        [Fact(DisplayName = "Test empty array bubble sort")]
        public async Task TestEmptyArrayBubbleSort()
        {
            var sort = new DotNetCoreSwap.Services.BubbleSort();
            await Assert.ThrowsAsync<ArgumentNullException>(() => sort.IntSort(null));
        }

        /// <summary>
        /// Tests the empty array insertion sort.
        /// </summary>
        /// <returns>Assert for the exception.</returns>
        [Fact(DisplayName = "Test empty array insertion sort")]
        public async Task TestEmptyArrayInsertionSort()
        {
            var sort = new DotNetCoreSwap.Services.InsertionSort();
            await Assert.ThrowsAsync<ArgumentNullException>(() => sort.IntSort(null));
        }

        /// <summary>
        /// Tests the empty array quick sort.
        /// </summary>
        /// <returns>Assert for the exception.</returns>
        [Fact(DisplayName = "Test empty array quick sort")]
        public async Task TestEmptyArrayQuickSort()
        {
            var sort = new DotNetCoreSwap.Services.QuickSort();
            await Assert.ThrowsAsync<ArgumentNullException>(() => sort.IntSort(null));
        }

    }
}
