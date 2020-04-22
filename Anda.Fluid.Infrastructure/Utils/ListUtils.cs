using System.Collections.Generic;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class ListUtils
    {
        public List<T> NewList<T>(List<T> list)
        {
            return new List<T>(list);
        }
    }
}
