using System;

namespace Karma.Services
{
    public interface ILog
    {
        void Log(string log);
        
        void Log(object obj);

        void Warning(string warning);

        void Error(string error);

        void Exception(Exception exception);
    }
}