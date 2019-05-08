using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Plugins.Karma.Utils
{
    public struct ResourcesRequestAwaiter : INotifyCompletion
    {
        private readonly ResourceRequest _asyncOperation;

        public ResourcesRequestAwaiter(ResourceRequest asyncOperation)
        {
            _asyncOperation = asyncOperation;
        }

        public ResourcesRequestAwaiter GetAwaiter() => this;

        public bool IsCompleted => _asyncOperation.isDone;

        public void OnCompleted(Action action)
        {
            action.Invoke();
        }

        public void GetResult() { }

        private async Task GetValue(Action action)
        {
            while (!_asyncOperation.isDone) { await Task.Yield(); }

            action();
        }
    }
}