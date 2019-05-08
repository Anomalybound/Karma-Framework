using UnityEngine;

namespace Karma
{
    public interface ILog
    {
        void Log(object obj, Object context = null);

        void Warn(string warning, Object context = null);

        void Error(string error, Object context = null);
    }
}