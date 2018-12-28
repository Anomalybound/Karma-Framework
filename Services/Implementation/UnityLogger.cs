using System;

namespace Karma.Services
{
    public class UnityLogger : ILog
    {
        public void Log(string log)
        {
            UnityEngine.Debug.Log(log);
        }

        public void Log(object obj)
        {
            UnityEngine.Debug.Log(obj);
        }

        public void Warning(string warning)
        {
            UnityEngine.Debug.LogWarning(warning);
        }

        public void Error(string error)
        {
            UnityEngine.Debug.LogError(error);
        }

        public void Exception(Exception exception)
        {
            UnityEngine.Debug.LogException(exception);
        }
    }
}