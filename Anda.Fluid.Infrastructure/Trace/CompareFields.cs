using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Trace
{
    public class CompareFields
    {
        public static bool CompareObj(Object newObj, Object oldObj)
        {
            return true;
            try
            {
                Type newType = newObj.GetType();
                Type oldType = oldObj.GetType();
                PropertyInfo[] newPropties = newType.GetProperties();
                PropertyInfo[] oldPropties = oldType.GetProperties();

                if (newPropties.Length == oldPropties.Length)
                {
                    for (int i = 0; i < newPropties.Length; i++)
                    {
                        PropertyInfo newPInfo = newPropties[i];
                        PropertyInfo oldPInfo = oldPropties[i];
                        object valueNew = null;
                        string nameNew = string.Empty;
                        object valueOld = null;
                        string nameOld = string.Empty;
                        if (newPInfo.PropertyType.IsClass)
                        {
                            Type proType = newPInfo.PropertyType;
                            if (typeof(string).Equals(proType))
                            {
                                nameNew = newPInfo.Name;
                                valueNew = newPInfo.GetValue(newObj);
                                nameOld = oldPInfo.Name;
                                valueOld = oldPInfo.GetValue(oldObj);
                                if (valueNew == null || valueOld == null)
                                {
                                    return false;
                                }
                                else
                                {
                                    if (!valueNew.Equals(valueOld))
                                    {
                                        //Console.WriteLine("oldValue: {0} ->  newValue: {1}", valueNew, valueOld);
                                        string strOutPut = string.Format("oldValue: {0} ->  newValue: {1}", valueOld, valueNew);
                                        Logger.DEFAULT.Info(LogCategory.SETTING,strOutPut);
                                        oldPInfo.SetValue(oldObj, valueNew);
                                    }
                                }
                               
                            }
                            else
                            {
                                object ObjNewInner = newPInfo.GetValue(newObj);
                                object ObjOldInner = oldPInfo.GetValue(oldObj);
                                CompareObj(ObjNewInner, ObjOldInner);
                            }
                        }
                        else if (newPInfo.PropertyType.IsValueType)
                        {
                            nameNew = newPInfo.Name;
                            valueNew = newPInfo.GetValue(newObj);
                            nameOld = oldPInfo.Name;
                            valueOld = oldPInfo.GetValue(oldObj);
                            if (valueNew == null || valueOld == null)
                            {
                                return false;
                            }
                            else
                            {
                                if (!valueNew.Equals(valueOld))
                                {
                                    //Console.WriteLine("oldValue: {0} ->  newValue: {1}", valueNew, valueOld);
                                    string strOutPut = string.Format("oldValue: {0} ->  newValue: {1}", valueOld,valueNew);
                                    Logger.DEFAULT.Info(LogCategory.SETTING, strOutPut);
                                    oldPInfo.SetValue(oldObj, valueNew);

                                }
                            }

                        }


                    }
                }

                return true;


            }
            catch
            {
                return false;
            }
        }
    }
}
