using Hermit.DataBinding;

namespace Hermit.Adapters
{
    [Adapter(typeof(int), typeof(string))]
    public class IntFloatAdapter : IAdapter
    {
        public object Covert(object fromObj, AdapterOptions options)
        {
            return (float) fromObj;
        }
    }
}