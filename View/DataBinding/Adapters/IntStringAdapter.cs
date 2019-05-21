using Hermit.DataBinding;

namespace Hermit.Adapters
{
    [Adapter(typeof(int), typeof(string))]
    public class IntStringAdapter : IAdapter
    {
        public object Covert(object fromObj, AdapterOptions options)
        {
            return fromObj.ToString();
        }
    }
}