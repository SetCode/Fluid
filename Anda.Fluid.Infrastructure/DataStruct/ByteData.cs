using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Anda.Fluid.Infrastructure.DataStruct
{
    public class ByteData 
    {
        public ByteData(byte[] data, Encoding encoding)
        {
            this.Bytes = data;
            this.Encoding = encoding;
        }

        public Encoding Encoding { get; private set; }

        public byte[] Bytes { get; private set; }

        public override string ToString()
        {
            return this.Encoding.GetString(Bytes);
        }
    }
}
