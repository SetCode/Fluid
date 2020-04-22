using System;
using System.Collections.Generic;
using System.Threading;

namespace Anda.Fluid.Infrastructure.Msg
{
    public class MsgCenter
    {
        private static Dictionary<IMsgReceiver, string[]> msgNamesMap = new Dictionary<IMsgReceiver, string[]>();
        private static Dictionary<string, List<IMsgReceiver>> msgReceiverMap = new Dictionary<string, List<IMsgReceiver>>();
        private static object lockObj = new object();

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        /// <param name="msgReceiver"></param>
        /// <param name="msgNames"></param>
        public static void RegisterReceiver(IMsgReceiver msgReceiver, params string[] msgNames)
        {
            lock (lockObj)
            {
                if (msgNamesMap.ContainsKey(msgReceiver))
                {
                    throw new System.Exception("MsgReceiver has already been registered!");
                }
                if (msgNames == null || msgNames.Length == 0)
                {
                    throw new System.Exception("Msg names associated with the msg receiver can not be empty.");
                }
                msgNamesMap.Add(msgReceiver, msgNames);
                foreach (string msgName in msgNames)
                {
                    List<IMsgReceiver> msgReceiverList = null;
                    if (!msgReceiverMap.ContainsKey(msgName))
                    {
                        msgReceiverList = new List<IMsgReceiver>();
                        msgReceiverMap.Add(msgName, msgReceiverList);
                    }
                    else
                    {
                        msgReceiverList = msgReceiverMap[msgName];
                    }
                    msgReceiverList.Add(msgReceiver);
                }
            }
        }

        /// <summary>
        /// 解除注册消息接收者
        /// </summary>
        /// <param name="msgReceiver"></param>
        public static void UnregisterReceiver(IMsgReceiver msgReceiver)
        {
            lock (lockObj)
            {
                if (!msgNamesMap.ContainsKey(msgReceiver))
                {
                    throw new System.Exception("MsgReceiver has not been registered yet!");
                }
                string[] msgNames = msgNamesMap[msgReceiver];
                msgNamesMap.Remove(msgReceiver);
                foreach (string msgName in msgNames)
                {
                    if (!msgReceiverMap.ContainsKey(msgName))
                    {
                        throw new System.Exception("Unexpected status : msg name(" + msgName + ") can not be found in msgReceiverMap");
                    }
                    msgReceiverMap[msgName].Remove(msgReceiver);
                }
            }
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="msgName">消息名字</param>
        /// <param name="sender">消息发送者</param>
        /// <param name="args">消息携带的参数</param>
        public static void Broadcast(string msgName, IMsgSender sender, params object[] args)
        {
            lock (lockObj)
            {
                if (msgReceiverMap.ContainsKey(msgName))
                {
                    List<IMsgReceiver> msgReceiverList = msgReceiverMap[msgName];
                    foreach (IMsgReceiver msgReceiver in msgReceiverList)
                    {
                        msgReceiver.HandleMsg(msgName, sender, args);
                    }
                }
            }
        }

        public static void SendMsg(string msgName, IMsgSender sender, IMsgReceiver receiver, params object[] args)
        {
            lock(lockObj)
            {
                receiver.HandleMsg(msgName, sender, args);
            }
        }
    }
}
