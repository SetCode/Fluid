using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.FluProgram.Structure
{
    /// <summary>
    /// 根据mark点实际位置偏差进行坐标校正
    /// </summary>
    [Serializable]
    public class CoordinateCorrector
    {
        private const string TAG = "CoordinateCorrector";
        // 存储 RunnableModule--(MarkCmd, mark点实际位置)
        private Dictionary<RunnableModule, Dictionary<MarkCmd, PointD>> markInfoMap = new Dictionary<RunnableModule, Dictionary<MarkCmd, PointD>>();
        // 辅助数据结构，加快查找速度
        private Dictionary<MarkCmd, Dictionary<MarkCmd, PointD>> markPointMap = new Dictionary<MarkCmd, Dictionary<MarkCmd, PointD>>();
        // ASV非标Mark第二个点的数据保存，用于生成坐标校正器
        private Dictionary<MarkCmd, PointD> asvMarkPoint2s = new Dictionary<MarkCmd, PointD>();
        // ASV非标Mark角度的数据保存，用于生成坐标校正器
        private Dictionary<MarkCmd, double> asvMarkAngles = new Dictionary<MarkCmd, double>();
        // 每个RunnableModule对应的坐标校正器
        private Dictionary<RunnableModule, CoordinateTransformer> transMap = new Dictionary<RunnableModule, CoordinateTransformer>();
        // runnable树形结构
        private RunnableModuleStructure runnableModuleStructure;

        public CoordinateCorrector(RunnableModuleStructure runnableModuleStructure)
        {
            this.runnableModuleStructure = runnableModuleStructure;
            IReadOnlyDictionary<RunnableModule, List<MarkCmd>> markMap = runnableModuleStructure.MarkMap;
            foreach (RunnableModule module in markMap.Keys)
            {
                Dictionary<MarkCmd, PointD> map = new Dictionary<MarkCmd, PointD>();
                foreach (MarkCmd markCmd in markMap[module])
                {
                    map.Add(markCmd, new PointD());
                    markPointMap.Add(markCmd, map);
                    if (markCmd.ModelFindPrm.IsUnStandard)
                    {
                        if (markCmd.ModelFindPrm.UnStandardType == 0)
                        {
                            this.asvMarkAngles.Add(markCmd, 0);
                        }
                        else
                        {
                            this.asvMarkPoint2s.Add(markCmd, new PointD());
                        }
                    }
                }
                markInfoMap.Add(module, map);
            }
        }

        /// <summary>
        /// 设置ASVmark实际输出角度值
        /// </summary>
        /// <param name="markCmd"></param>
        /// <param name="realPosition"></param>
        public void SetASVMarkRealAngle(MarkCmd markCmd, double realRotation)
        {
            asvMarkAngles[markCmd] = realRotation;
        }

        /// <summary>
        /// 设置ASVmark点2实际坐标位置
        /// </summary>
        /// <param name="markCmd"></param>
        /// <param name="realPosition"></param>
        public void SetASVMarkRealPosition2(MarkCmd markCmd, PointD realPosition)
        {
            PointD real = asvMarkPoint2s[markCmd];
            real.X = realPosition.X;
            real.Y = realPosition.Y;
        }

        /// <summary>
        /// 设置mark点实际坐标位置
        /// </summary>
        /// <param name="markCmd"></param>
        /// <param name="realPosition"></param>
        public void SetMarkRealPosition(MarkCmd markCmd, PointD realPosition)
        {
            PointD real = markPointMap[markCmd][markCmd];
            if(realPosition == null)
            {
                return;
            }
            real.X = realPosition.X;
            real.Y = realPosition.Y;
        }
        /// <summary>
        /// 返回Mark点实际坐标位置
        /// </summary>
        /// <param name="markCmd"></param>
        /// <returns></returns>
        public PointD GetMarkRealPosition(MarkCmd markCmd)
        {
            if (markPointMap.ContainsKey(markCmd))
            {
                return new PointD(markPointMap[markCmd][markCmd].X, markPointMap[markCmd][markCmd].Y);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 调用前提，当前RunnableModule的Mark已经执行拍照
        /// 设置RunnableModule的坐标校正器
        /// </summary>
        /// <param name="module"></param>
        public void SetRunnableModuleTransformer(RunnableModule module)
        {
            if (module == null)
            {
                return;
            }
            PointD mark1 = new PointD(), real1 = new PointD(), mark2 = new PointD(), real2 = new PointD();
            double markRotation = 0, realRotation = 0;
            Dictionary<MarkCmd, PointD> marks = markInfoMap[module];
            MarkCmd singleMark = null;
            if (marks.Count <= 0)
            {
                return;
            }
            int count = 0;
            foreach (MarkCmd markCmd in marks.Keys)
            {
                if (count == 0)
                {
                    singleMark = markCmd;
                    //非标ASVMark
                    if (singleMark.ModelFindPrm.IsUnStandard)
                    {
                        var structure = module.CommandsModule.program.ModuleStructure;
                        PointD p = new PointD(singleMark.ModelFindPrm.ReferenceX, singleMark.ModelFindPrm.ReferenceY);
                        PointD p2 = new PointD(singleMark.ModelFindPrm.ReferenceX2, singleMark.ModelFindPrm.ReferenceY2);
                        mark1.CopyFrom(structure.ToMachine(module, p));
                        real1.CopyFrom(singleMark.ModelFindPrm.TargetInMachine);
                        if (singleMark.ModelFindPrm.UnStandardType == 0)
                        {
                            markRotation = singleMark.ModelFindPrm.ReferenceA;
                            realRotation = singleMark.ModelFindPrm.Angle;
                        }
                        else
                        {
                            mark2.CopyFrom(structure.ToMachine(module, p2));
                            real2.CopyFrom(singleMark.ModelFindPrm.TargetInMachine2);
                        }
                    }
                    //正常Mark
                    else
                    {
                        mark1.CopyFrom(markCmd.Position);//编程时
                        real1.CopyFrom(marks[markCmd]);
                    }


                }
                else if (count == 1)
                {
                    mark2.CopyFrom(markCmd.Position);
                    real2.CopyFrom(marks[markCmd]);
                    // 脚本语法上限制了最多添加两个Mark点，此处可直接跳出循环
                    break;
                }
                count++;
            }
            CoordinateTransformer transformer = new CoordinateTransformer();
            if (marks.Count == 1)
            {
                Log.Dprint(TAG, "runnable mark count is 1, standard pos : " + mark1 + ", real pos : " + real1);

                if (singleMark.ModelFindPrm.IsUnStandard)
                {
                    if (singleMark.ModelFindPrm.UnStandardType == 0)
                    {
                        transformer.SetMarkPoint(mark1, markRotation, real1, realRotation);
                    }
                    else
                    {
                        transformer.SetMarkPoint(mark1, mark2, real1, real2);
                    }
                }
                else
                {
                    transformer.SetMarkPoint(mark1, real1);
                }

            }
            else
            {
                Log.Dprint(TAG, "runnable mark count is 2, standard pos1 : " + mark1 + ", real pos1 : " + real1
                    + ", standard pos2 : " + mark2 + ", real pos2 : " + real2);
                transformer.SetMarkPoint(mark1, mark2, real1, real2);
            }
            transMap.Add(module, transformer);
        }
        /// <summary>
        /// 当飞拍所有的mark点命令执行完后，需要生成transMap
        /// </summary>
        public void OnAllMarkCmdsExecuted()
        {
            transMap.Clear();
            PointD mark1 = new PointD(), real1 = new PointD(), mark2 = new PointD(), real2 = new PointD();
            double markRotation = 0, realRotation = 0;
            foreach (RunnableModule module in markInfoMap.Keys)
            {
                if (module.Mode == ModuleMode.SkipMode)
                {
                    continue;
                }
                Dictionary<MarkCmd, PointD> marks = markInfoMap[module];
                MarkCmd singleMark = null;
                if (marks.Count <= 0)
                {
                    continue;
                }
                int count = 0;
                foreach (MarkCmd markCmd in marks.Keys)
                {
                    if (count == 0)
                    {
                        singleMark = markCmd;
                        if (singleMark.ModelFindPrm.IsUnStandard)
                        {
                            //非标
                            var structure = module.CommandsModule.program.ModuleStructure;
                            PointD p = new PointD(singleMark.ModelFindPrm.ReferenceX, singleMark.ModelFindPrm.ReferenceY);
                            PointD p2 = new PointD(singleMark.ModelFindPrm.ReferenceX2, singleMark.ModelFindPrm.ReferenceY2);
                            mark1.CopyFrom(structure.ToMachine(module, p));
                            real1.CopyFrom(marks[markCmd]);
                            // 飞拍Mark点2取值
                            if (singleMark.ModelFindPrm.UnStandardType == 0)
                            {
                                markRotation = singleMark.ModelFindPrm.ReferenceA;
                                realRotation = asvMarkAngles[markCmd];
                            }
                            else
                            {
                                mark2.CopyFrom(structure.ToMachine(module, p2));
                                real2.CopyFrom(asvMarkPoint2s[markCmd]);
                            }
                            
                        }
                        else
                        {
                            mark1.CopyFrom(markCmd.Position);
                            real1.CopyFrom(marks[markCmd]);
                        }

                    }
                    else if (count == 1)
                    {
                        mark2.CopyFrom(markCmd.Position);
                        real2.CopyFrom(marks[markCmd]);
                        // 脚本语法上限制了最多添加两个Mark点，此处可直接跳出循环
                        break;
                    }
                    count++;
                }
                CoordinateTransformer transformer = new CoordinateTransformer();
                if (marks.Count == 1)
                {
                    Log.Dprint(TAG, "runnable mark count is 1, standard pos : " + mark1 + ", real pos : " + real1);
                    if (singleMark.ModelFindPrm.IsUnStandard)
                    {
                        if (singleMark.ModelFindPrm.UnStandardType == 0)
                        {
                            transformer.SetMarkPoint(mark1, markRotation, real1, realRotation);
                        }
                        else
                        {
                            transformer.SetMarkPoint(mark1, mark2, real1, real2);
                        }
                    }
                    else
                    {
                        transformer.SetMarkPoint(mark1, real1);
                    }
                }
                else
                {
                    Log.Dprint(TAG, "runnable mark count is 2, standard pos1 : " + mark1 + ", real pos1 : " + real1
                        + ", standard pos2 : " + mark2 + ", real pos2 : " + real2);
                    transformer.SetMarkPoint(mark1, mark2, real1, real2);
                }
                transMap.Add(module, transformer);
            }
        }

        /// <summary>
        /// 坐标校正
        /// </summary>
        /// <param name="runnableModule">点所在的RunnableModule</param>
        /// <param name="point">点坐标</param>
        public PointD Correct(RunnableModule runnableModule, PointD point, PointD executantOriginOffset)
        {
            if (!transMap.ContainsKey(runnableModule))
            {
                //当前拼版没有mark时获取父级
                RunnableModule parentModule = runnableModuleStructure.GetParentModule(runnableModule);
                //如果父级不为空也没有Mark再获取父级的父级
                while (parentModule != null && !transMap.ContainsKey(parentModule))
                {
                    parentModule = runnableModuleStructure.GetParentModule(parentModule);
                }
                //使用最终获取的父级的校正器校准当前点位(此时当前拼版与最终获取到的父级拼版之间的层级没有mark，所以可以直接校准)
                if (parentModule != null)
                {
                    return transMap[parentModule].Transform(point);
                }
                else
                {
                    //如果父级没有Mark的时候，就要加上轨道偏移值
                    return new PointD(point + executantOriginOffset); //所有父级都没有Mark直接返回当前点位
                }
            }
            return transMap[runnableModule].Transform(point);
        }
        /// <summary>
        /// Get the distence between the mark1 of the two runnableModules
        /// if runnable don't have any marks,return the distence between the origin of the two runnableModules
        /// </summary>
        /// <param name="mainModule"></param>
        /// <param name="simulModule"></param>
        /// <returns></returns>
        public VectorD GetRunnableDistence(RunnableModule mainModule, RunnableModule simulModule)
        {
            if (!markInfoMap.ContainsKey(mainModule) || !markInfoMap.ContainsKey(mainModule))
            {
                return mainModule.Origin - simulModule.Origin;
            }
            Dictionary<MarkCmd, PointD> marks = markInfoMap[mainModule];
            if (marks.Count < 1)
            {
                return mainModule.Origin - simulModule.Origin;
            }
            else
            {
                PointD module1Mark1 = new PointD();//主mark在机械坐标系中坐标
                PointD module2Mark1 = new PointD();//副阀mark在机械坐标系中坐标
                int count = 0;
                foreach (MarkCmd markCmd in marks.Keys)
                {
                    if (count == 0)
                    {
                        module1Mark1.CopyFrom(marks[markCmd]);
                        break;
                    }
                }
                count = 0;
                marks = markInfoMap[simulModule];
                foreach (MarkCmd markCmd in marks.Keys)
                {
                    if (count == 0)
                    {
                        module2Mark1.CopyFrom(marks[markCmd]);
                        break;
                    }
                }
                return module1Mark1 - module2Mark1;
            }
        }
        /// <summary>
        /// 获取2个RunnableModule之间的同步坐标转换器
        /// </summary>
        /// <param name="markCmd"></param>
        /// <returns></returns>
        public CoordinateTransformer GetSimul(RunnableModule mainModule, RunnableModule simulModule)
        {
            CoordinateTransformer transformer = new CoordinateTransformer();
            //默认使用两个pattern的原点进行纠偏，（有Mark则使用mark纠偏）
            transformer.SetMarkPoint(mainModule.Origin, simulModule.Origin);
            //MarkInfo中没有对应Mark指令，说明对应pattern没有添加Mark
            if (!markInfoMap.ContainsKey(mainModule))
            {
                return transformer;
            }
            Dictionary<MarkCmd, PointD> marks = markInfoMap[mainModule];
            if (marks.Count <= 0)
            {
                return transformer;
            }
            int count = 0;
            PointD module1Mark1 = new PointD(), module1Mark2 = new PointD(), module2Mark1 = new PointD(), module2Mark2 = new PointD();
            foreach (MarkCmd markCmd in marks.Keys)
            {
                if (count == 0)
                {
                    module1Mark1.CopyFrom(marks[markCmd]);
                }
                else if (count == 1)
                {
                    module1Mark2.CopyFrom(marks[markCmd]);
                    // 脚本语法上限制了最多添加两个Mark点，此处可直接跳出循环
                    break;
                }
                count++;
            }
            marks = markInfoMap[simulModule];
            count = 0;
            foreach (MarkCmd markCmd in marks.Keys)
            {
                if (count == 0)
                {
                    module2Mark1.CopyFrom(marks[markCmd]);
                }
                else if (count == 1)
                {
                    module2Mark2.CopyFrom(marks[markCmd]);
                    // 脚本语法上限制了最多添加两个Mark点，此处可直接跳出循环
                    break;
                }
                count++;
            }
            if (count == 1)
            {
                transformer.SetMarkPoint(module1Mark1, module1Mark2, module2Mark1, module2Mark2);
            }
            else
            {
                transformer.SetMarkPoint(module1Mark1, module2Mark1);
            }
            return transformer;
        }

        ///<summary>
        /// Description	:返回指定拼版Mark数量
        /// Author  	:liyi
        /// Date		:2019/07/15
        ///</summary>   
        /// <param name="module"></param>
        /// <returns></returns>
        public List<MarkCmd> GetRunnableModuleMark(RunnableModule module)
        {
            if (!markInfoMap.ContainsKey(module))
            {
                return new List<MarkCmd>();
            }
            Dictionary<MarkCmd, PointD> marks = markInfoMap[module];
            List<MarkCmd> result = new List<MarkCmd>();
            foreach (MarkCmd item in marks.Keys)
            {
                result.Add(item);
            }
            return result;
        }
    }
}