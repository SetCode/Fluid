using Anda.Fluid.Infrastructure.Tasker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public class HeaterServer:TaskLoop
    {
        private static HeaterServer instance = new HeaterServer();
        private Queue<HeaterMessage> queue = new Queue<HeaterMessage>();
        private Queue<HeaterMessage> priorityQueue = new Queue<HeaterMessage>();
        private HeaterServer()
        {

        }
        public static HeaterServer Instance => instance;

        public void Fire(HeaterMessage heaterMessage)
        {
            lock (this)
            {
                if (heaterMessage.Content == HeaterMsg.获取标准温度值
                    || heaterMessage.Content == HeaterMsg.获取温度上限值
                    || heaterMessage.Content == HeaterMsg.获取温度下限值
                    || heaterMessage.Content == HeaterMsg.获取温度漂移值)
                {
                    this.queue.Enqueue(heaterMessage);
                }
                else
                {
                    this.priorityQueue.Enqueue(heaterMessage);
                }
            }
        }

        public void Dispath()
        {
            while (priorityQueue.Count > 0)
            {
                HeaterMessage hm;
                lock (this)
                {
                    hm = priorityQueue.Dequeue();
                }
                hm?.HandleWriteMessage();
                Thread.Sleep(60);
            }

            foreach (var item in HeaterControllerMgr.Instance.FindAll())
            {
                Thread.Sleep(30);

                item.UpdateTemp();
               
                Thread.Sleep(30);
            }

            if (this.queue.Count > 0)
            {
                HeaterMessage hm;
                lock (this)
                {
                    hm = this.queue.Dequeue();
                }
                hm?.HandleReadMessage();
            }
            Thread.Sleep(30);

            foreach (var item in HeaterControllerMgr.Instance.FindAll())
            {
                item.UpdateAutoClose();
            }
        }

        protected override void Loop()
        {
            this.Dispath();
        }
    }
}
