using System;
namespace DotNetCoreSwap.Helpers
{
    public class AbstractSort
    {
        public AbstractSort()
        {

        }

        public void Exchange(int[] data, int m, int n)
        {
            int temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
    }
}
