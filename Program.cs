using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DotNetCoreSwap
{
    /// <summary>
    /// Main program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start point
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the web host 
        /// </summary>
        /// <returns>The web host builder.</returns>
        /// <param name="args">Arguments.</param>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
