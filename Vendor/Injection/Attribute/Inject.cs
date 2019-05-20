using System;

namespace Karma
{
    public class Inject : Attribute
    {
        public string BindingId { get; }

        public Inject() { }

        public Inject(string bindingId)
        {
            BindingId = bindingId;
        }
    }
}