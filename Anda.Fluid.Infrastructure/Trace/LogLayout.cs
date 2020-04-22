using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Trace
{
    class LogLayout : ILogLayout
    {
        public string Layout(LoggingEvent e)
        {
            string timeStr = e.Time.ToString("yyyy/MM/dd HH:mm:ss:fff");
            StringBuilder sb = new StringBuilder();
            sb.Append(timeStr).Append('{').Append(e.Level).Append('}').Append('[').Append(e.Category);
            if (e.Tag != null)
            {
                sb.Append(" / ").Append(e.Tag);
            }
            sb.Append("] ").Append(e.Message);
            if (e.Exeption != null)
            {
                sb.Append(" <Exeption>").Append(e.Exeption.ToString());
            }
            sb.Append("\r\n");
            return sb.ToString();
        }
    }
}
