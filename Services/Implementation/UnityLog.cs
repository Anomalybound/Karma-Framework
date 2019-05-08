using UnityEngine;

namespace Karma.Services
{
    public class UnityLog : ILog
    {
        private readonly bool _enable;

        public UnityLog(bool logEnable = true)
        {
            _enable = logEnable;
        }

        public void Log(object obj, Object context = null)
        {
            if (!_enable) { return; }

            Debug.Log(obj, context);
        }

        public void Warn(string warning, Object context = null)
        {
            if (!_enable) { return; }

            Debug.LogWarning(warning, context);
        }

        public void Error(string error, Object context = null)
        {
            if (!_enable) { return; }

            Debug.LogError(error, context);
        }
    }
}