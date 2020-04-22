using Anda.Fluid.Infrastructure.Trace;
using System;

namespace CommonLibrary.Utils
{
    public class ConvertSafely
    {
        private const string TAG = "ConvertSafely";

        public static int ToInt32(string value, int defValue = 0)
        {
            int valueInt = defValue;
            try
            {
                valueInt = Convert.ToInt32(value);
            }
            catch (Exception e)
            {
                Log.Print(TAG, e);
            }
            return valueInt;
        }

        public static long ToInt64(string value, long defValue = 0L)
        {
            long valueLong = defValue;
            try
            {
                valueLong = Convert.ToInt64(value);
            }
            catch (Exception e)
            {
                Log.Print(TAG, e);
            }
            return valueLong;
        }

        public static float ToFloat(string value, float defValue = 0f)
        {
            float floatValue = defValue;
            try
            {
                floatValue = Convert.ToSingle(value);
            }
            catch (Exception e)
            {
                Log.Print(TAG, e);
            }
            return floatValue;
        }

        public static double ToDouble(string value, double defValue = 0d)
        {
            double valueDouble = defValue;
            try
            {
                valueDouble = Convert.ToDouble(value);
            }
            catch (Exception e)
            {
                Log.Print(TAG, e);
            }
            return valueDouble;
        }
    }
}
