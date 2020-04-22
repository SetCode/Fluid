using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gts;

namespace Anda.Fluid.Drive.Motion.CardFramework.CardExecutor
{
    public class GtsExtMdlExecutor : IExtMdlExecutable
    {
        public int DiData { get; set; }

        public int DoData { get; set; }

        public short Open(short cardId)
        {
            return mc.GT_OpenExtMdl(cardId, "gts.dll");
        }

        public short Close(short cardId)
        {
            return mc.GT_CloseExtMdl(cardId);
        }

        public short Reset(short cardId)
        {
            return mc.GT_ResetExtMdl(cardId);
        }

        public short LoadConfig(short cardId, string configFile)
        {
            return mc.GT_LoadExtConfig(cardId, configFile);
        }

        public short GetDi(short cardId, short mdlId, out int data)
        {
            ushort value;
            short rtn = mc.GT_GetExtIoValue(cardId, mdlId, out value);
            data = (int)value;
            return rtn;
        }

        public short SetDo(short cardId, short mdlId, int data)
        {
            return mc.GT_SetExtIoValue(cardId, mdlId, (ushort)data);
        }

        public short GetDo(short cardId, short mdlId, out int data)
        {
            ushort value;
            short rtn = mc.GT_GetExtDoValue(cardId, mdlId, out value);
            data = (int)value;
            return rtn;
        }

        public short SetDoBit(short cardId, short mdlId, short doId, short value)
        {
            int v = value ^ 1;
            return mc.GT_SetExtIoBit(cardId, mdlId, doId, (ushort)v);
        }

        public int GetDiBit(int data, short diId)
        {
            return ((data >> diId) & 1) ^ 1;
        }

        public int GetDoBit(int data, short doId)
        {
            return ((data >> doId) & 1) ^ 1;
        }
    }
}
