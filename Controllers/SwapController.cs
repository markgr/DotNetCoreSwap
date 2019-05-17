using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSwap.Extensions;
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
    [Authorize]
    [Route("api/[controller]")]
    public class SwapController : Controller
    {
        IServiceProvider _serviceProvider;

        public SwapController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // GET: api/values
        [AllowAnonymous]
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
                return BadRequest("Error psring numberOfItems");

            Random r = new Random();

            var services = _serviceProvider.GetServices<ISort>();
            var bubbleSort = services.FirstOrDefault(o => o.GetType() == typeof(BubbleSort));
            var output = await bubbleSort.IntSort(Enumerable.Range(0, items).Select(x => r.Next(1000)).ToArray());
            return Ok(output);
        }

        // GET: api/values
        [AllowAnonymous]
        [HttpGet("selectionSortRandom")]
        [SwaggerResponse(200, Type = typeof(IntSortResults))]
        [SwaggerResponse(400, Description = "Invalid", Type = typeof(Exception))]
        [SwaggerResponse(401, Description = "Not Auth", Type = typeof(Exception))]
        [SwaggerResponse(403, Description = "Not Au", Type = typeof(Exception))]
        [SwaggerOperation(description: "This will return a random array of ints - sorted using a bubble sort")]
        public async Task<IActionResult> SelectionSortRandom([Required] string numberItems)
        {
            int items = -1;
            if (!Int32.TryParse(numberItems, out items))
                return BadRequest("Error psring numberOfItems");

            Random r = new Random();

            var services = _serviceProvider.GetServices<ISort>();
            var selectionSort = services.FirstOrDefault(o => o.GetType() == typeof(SelectionSort));
            var output = await selectionSort.IntSort(Enumerable.Range(0, items).Select(x => r.Next(1000)).ToArray());
            return Ok(output);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
