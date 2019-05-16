using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSwap.Extensions;
using DotNetCoreSwap.Interfaces;
using DotNetCoreSwap.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetCoreSwap.Controllers
{
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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var services = _serviceProvider.GetServices<ISort>();
            var bubbleSort = services.FirstOrDefault(o => o.GetType() == typeof(BubbleSort));
            var output = await bubbleSort.IntSort(new int[] { 7, 3, 6, 12, 54, 12, 2, 8, 29});
            return Ok(output);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
