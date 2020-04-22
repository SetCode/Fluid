namespace Anda.Fluid.Infrastructure.Msg
{
    public interface IMsgReceiver
    {
        void HandleMsg(string msgName, IMsgSender sender, params object[] args);
    }
}