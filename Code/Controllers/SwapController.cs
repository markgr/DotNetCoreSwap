using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSwap.Interfaces;
using DotNetCoreSwap.Models;
using DotNetCoreSwap.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetCoreSwap.Controllers
{
    /// <summary>
    /// Controller for the swap API
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class SwapController : Controller
    {
        IServiceProvider _serviceProvider;

        /// <summary>
        /// C'Tor
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public SwapController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Bubble Sort Random API.
        /// </summary>
        /// <returns>Sorted array</returns>
        /// <param name="numberItems">Number items to sort</param>
        // GET: api/values
        //[AllowAnonymous]
        [HttpGet("bubbleSortRandom")]
        [SwaggerResponse(200, Type = typeof(IntSortResults))]
        [SwaggerResponse(400, Description = "Invalid", Type = typeof(Exception))]
        [SwaggerResponse(401, Description = "Not Auth", Type = typeof(Exception))]
        [SwaggerResponse(403, Description = "Not Au", Type = typeof(Exception))]
        [SwaggerOperation(description:"This will return a random array of ints - sorted using a bubble sort")]
        public async Task<IActionResult> BubbleSortRandom([Required] string numberItems)
        {
            int items = -1;
            if (!Int32.TryParse(numberItems, out items))
                return BadRequest("Error parsing numberOfItems");

            Random r = new Random();

            var services = _serviceProvider.GetServices<ISort>();
            var bubbleSort = services.FirstOrDefault(o => o.GetType() == typeof(InsertionSort));
            var output = await bubbleSort.IntSort(Enumerable.Range(0, items).Select(x => r.Next(1000)).ToArray());
            return Ok(output);
        }

        /// <summary>
        /// Selection Sort Random API.
        /// </summary>
        /// <returns>Sorted array</returns>
        /// <param name="numberItems">Number items to sort</param>
        // GET: api/values
        //[AllowAnonymous]
        [HttpGet("selectionSortRandom")]
        [SwaggerResponse(200, Type = typeof(IntSortResults))]
        [SwaggerResponse(400, Description = "Invalid", Type = typeof(Exception))]
        [SwaggerResponse(401, Description = "Not Auth", Type = typeof(Exception))]
        [SwaggerResponse(403, Description = "Not Au", Type = typeof(Exception))]
        [SwaggerOperation(description: "This will return a random array of ints - sorted using a selection sort")]
        public async Task<IActionResult> SelectionSortRandom([Required] string numberItems)
        {
            int items = -1;
            if (!Int32.TryParse(numberItems, out items))
                return BadRequest("Error parsing numberOfItems");

            Random r = new Random();

            var services = _serviceProvider.GetServices<ISort>();
            var selectionSort = services.FirstOrDefault(o => o.GetType() == typeof(SelectionSort));
            var output = await selectionSort.IntSort(Enumerable.Range(0, items).Select(x => r.Next(1000)).ToArray());
            return Ok(output);
        }

        /// <summary>
        /// Insertion Sort Random API.
        /// </summary>
        /// <returns>Sorted array</returns>
        /// <param name="numberItems">Number items to sort</param>
        //[AllowAnonymous]
        [HttpGet("insertionSortRandom")]
        [SwaggerResponse(200, Type = typeof(IntSortResults))]
        [SwaggerResponse(400, Description = "Invalid", Type = typeof(Exception))]
        [SwaggerResponse(401, Description = "Not Auth", Type = typeof(Exception))]
        [SwaggerResponse(403, Description = "Not Au", Type = typeof(Exception))]
        [SwaggerOperation(description: "This will return a random array of ints - sorted using a insertion sort")]
        public async Task<IActionResult> InsertionSortRandom([Required] string numberItems)
        {
            int items = -1;
            if (!Int32.TryParse(numberItems, out items))
                return BadRequest("Error parsing numberOfItems");

            Random r = new Random();

            var services = _serviceProvider.GetServices<ISort>();
            var selectionSort = services.FirstOrDefault(o => o.GetType() == typeof(InsertionSort));
            var output = await selectionSort.IntSort(Enumerable.Range(0, items).Select(x => r.Next(1000)).ToArray());
            return Ok(output);
        }

        /// <summary>
        /// Quick Sort Random API.
        /// </summary>
        /// <returns>Sorted array</returns>
        /// <param name="numberItems">Number items to sort</param>
        //[AllowAnonymous]
        [HttpGet("quickSortRandom")]
        [SwaggerResponse(200, Type = typeof(IntSortResults))]
        [SwaggerResponse(400, Description = "Invalid", Type = typeof(Exception))]
        [SwaggerResponse(401, Description = "Not Auth", Type = typeof(Exception))]
        [SwaggerResponse(403, Description = "Not Au", Type = typeof(Exception))]
        [SwaggerOperation(description: "This will return a random array of ints - sorted using a qucik sort")]
        public async Task<IActionResult> QuickSortRandom([Required] string numberItems)
        {
            int items = -1;
            if (!Int32.TryParse(numberItems, out items))
                return BadRequest("Error parsing numberOfItems");

            Random r = new Random();

            var services = _serviceProvider.GetServices<ISort>();
            var selectionSort = services.FirstOrDefault(o => o.GetType() == typeof(QuickSort));
            var output = await selectionSort.IntSort(Enumerable.Range(0, items).Select(x => r.Next(1000)).ToArray());
            return Ok(output);
        }

    }
}
