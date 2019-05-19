using System;
namespace TestSwap.Mocks
{
    public class MockServiceProvider : IServiceProvider
    {
        public MockServiceProvider()
        {
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
