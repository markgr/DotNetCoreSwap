namespace DotNetCoreSwap.Helpers
{
    /// <summary>
    /// Abstract class for parent of all sorts.
    /// </summary>
    public class AbstractSort
    {
        /// <summary>
        /// All sorts usees this - swap pos 1 for pos 2 in data
        /// </summary>
        /// <param name="data">data to use</param>
        /// <param name="m">pos 1</param>
        /// <param name="n">pos 2</param>
        public void Exchange(int[] data, int m, int n)
        {
            int temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
    }
}
