namespace Anda.Fluid.Infrastructure.Common
{
    public class CommonDelegates
    {
        public delegate void OnStart();

        public delegate void OnError(int errCode, string errMsg);

        public delegate void OnFinished();

        public delegate void OnFinished<T>(T t);
    }
}