using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anda.Fluid.Domain.FluProgram.Structure
{

    /// <summary>
    /// 记录程序的RunnableModule结构信息、Mark点等
    /// </summary>
    [Serializable]
    public class RunnableModuleStructure
    {
        // RunnableModule构成的树结构
        private ModuleTree moduleTree = new ModuleTree();
        /// <summary>
        /// RunnableModule 对应的MarkCmd对应的dic
        /// </summary>
        private Dictionary<RunnableModule, List<MarkCmd>> markMap = new Dictionary<RunnableModule, List<MarkCmd>>();
        /// <summary>
        /// RunnableModule--所包含的MarkCmd 映射关系
        /// </summary>
        public IReadOnlyDictionary<RunnableModule, List<MarkCmd>> MarkMap
        {
            get { return markMap; }
        }
        private Dictionary<RunnableModule, List<BadMarkCmd>> badMarkMap = new Dictionary<RunnableModule, List<BadMarkCmd>>();
        /// <summary>
        /// 每个RunnableModule与每个BadMark之间的映射关系
        /// </summary>
        public IReadOnlyDictionary<RunnableModule, List<BadMarkCmd>> BadMarkMap
        {
            get { return badMarkMap; }
        }
        private Dictionary<RunnableModule, List<BarcodeCmd>> barcodeMap = new Dictionary<RunnableModule, List<BarcodeCmd>>();
        
        /// <summary>
        /// 每个RunnableModule与每个Barcode之间的映射关系
        /// </summary>
        public IReadOnlyDictionary<RunnableModule, List<BarcodeCmd>> BarcodeMap
        {
            get { return barcodeMap; }
        }
        private Dictionary<RunnableModule,List<MeasureHeightCmd>> measureHeightMap = new Dictionary<RunnableModule, List<MeasureHeightCmd>>();
        /// <summary>
        /// 每个RunnableModule所对应的MeasureHeight集合映射
        /// </summary>
        public IReadOnlyDictionary<RunnableModule,List<MeasureHeightCmd>> MeasureHeightMap
        {
            get { return measureHeightMap; }
        }

        private Dictionary<RunnableModule, List<MeasureCmd>> measureMap = new Dictionary<RunnableModule, List<MeasureCmd>>();

        /// <summary>
        /// 每个RunnableModule与每个Barcode之间的映射关系
        /// </summary>
        public IReadOnlyDictionary<RunnableModule, List<MeasureCmd>> MeasureMap
        {
            get { return measureMap; }
        }

        private Dictionary<RunnableModule, List<BlobsCmd>> blobsMap = new Dictionary<RunnableModule, List<BlobsCmd>>();

        /// <summary>
        /// 每个RunnableModule与每个Blobs之间的映射关系
        /// </summary>
        public IReadOnlyDictionary<RunnableModule, List<BlobsCmd>> BlobsMap
        {
            get { return blobsMap; }
        }

        private Dictionary<RunnableModule, List<SymbolLinesCmd>> symbolLinesMap = new Dictionary<RunnableModule, List<SymbolLinesCmd>>();

        /// <summary>
        /// 每个RunnableModule与每个SymbolLinesCmd之间的映射关系
        /// </summary>
        public IReadOnlyDictionary<RunnableModule, List<SymbolLinesCmd>> SymbolLinesMap
        {
            get { return symbolLinesMap; }
        }

        public List<MarkCmd> MarksSorted = null;

        public List<MeasureHeightCmd> MHCmdsSorted = null;

        private List<int> AllBoardsNo = new List<int>();

        public RunnableModule ProgramModule
        {
            get
            {
                if (moduleTree.IsEmpty)
                {
                    throw new Exception("RunnableModuleTree is empty.");
                }
                return moduleTree.Root.Module;
            }
        }

        public RunnableModule WorkpieceModule
        {
            get
            {
                if (moduleTree.IsEmpty)
                {
                    throw new Exception("Runnable module tree is empty.");
                }
                if (moduleTree.Root.Children.Count <= 0)
                {
                    throw new Exception("Workpiece is not found in runnable module tree.");
                }
                return moduleTree.Root.Children[0].Module;
            }
        }

        public void AddBoardNo(int boardNo)
        {
            if (!this.AllBoardsNo.Contains(boardNo))
            {
                this.AllBoardsNo.Add(boardNo);
            }
        }

        /// <summary>
        /// 跳过指定穴位的拼版（用于Mes或其他特殊功能跳过指定穴位拼版）
        /// </summary>
        /// <param name="boardsNo">跳过所有指定的穴位的拼版</param>
        public void SkipRunnableModuleByBoardNo(List<int> boardsNo)
        {
            if (boardsNo.Count <= 0)
            {
                return;
            }
            List <RunnableModule> allModules = GetAllRunnableModules();
            foreach (int boardNo in boardsNo)
            {
                if (!this.AllBoardsNo.Contains(boardNo))
                {
                    continue;
                }

                foreach (RunnableModule module in allModules)
                {
                    if (module.Mode == ModuleMode.SkipMode)
                    {
                        continue;
                    }

                    if (module.BoardNo == boardNo)
                    {
                        module.Mode = ModuleMode.SkipMode;
                        SetAllChildModuleMode(module, ModuleMode.SkipMode);
                    }
                }
            }
            
        }

        public RunnableModule GetParentModule(RunnableModule module)
        {
            ModuleNode node = moduleTree.GetNode(module);
            if(node == null)
            {
                return null;
            }
            if (node.Parent == null)
            {
                return null;
            }
            return node.Parent.Module;
        }

        public List<RunnableModule> GetChildrenModule(RunnableModule module)
        {
            ModuleNode node = moduleTree.GetNode(module);
            if (node == null)
            {
                return null;
            }
            if (node.Children.Count < 1)
            {
                return null;
            }
            List<RunnableModule> list = new List<RunnableModule>();
            foreach (ModuleNode nodeItem in node.Children)
            {
                list.Add(nodeItem.Module);
            }
            return list;
        }

        public void SetAllChildModuleMode(RunnableModule module,ModuleMode mode)
        {
            List<RunnableModule> childModules = GetChildrenModule(module);
            if (childModules != null)
            {
                foreach (RunnableModule childModule in childModules)
                {
                    childModule.Mode = mode;
                    SetAllChildModuleMode(childModule, mode);
                }
            }
        }
        /// <summary>
        /// 只在计算同步pattern时调用，计算当前同步pattern的子集的同步坐标系
        /// </summary>
        /// <param name="mainModule">主拼版</param>
        /// <param name="simulModule">同步拼版</param>
        /// <param name="coordinateCorrector">RunnableModule与对应Mark的映射关系类</param>
        /// <param name="topMainOrign">最顶层的主拼版的原点、或最近一级的有Mark的</param>
        /// <param name="topSimulOrigin">最顶层的同步拼版的原</param>
        public void SetAllChildModuleSimulTransfromer(RunnableModule mainModule,RunnableModule simulModule,CoordinateCorrector coordinateCorrector)
        {
            //必须使用相同的pattern才可以同步
            if (!mainModule.CommandsModule.Name.Equals(simulModule.CommandsModule.Name))
            {
                return;
            }
            List<RunnableModule> mainChildModules = GetChildrenModule(mainModule);
            List<RunnableModule> simulChildModules = GetChildrenModule(simulModule);
            if (mainChildModules == null)
            {
                return;
            }
            for (int i = 0; i < mainChildModules.Count; i++)
            {
                mainChildModules[i].Mode = mainModule.Mode;
                simulChildModules[i].Mode = simulModule.Mode;
                //没有Mark就使用上一级的同步坐标校正器
                if (!markMap.ContainsKey(mainChildModules[i]))
                {
                    mainChildModules[i].SimulTransformer = mainModule.SimulTransformer;
                }
                else
                {
                    mainChildModules[i].SimulTransformer = coordinateCorrector.GetSimul(mainChildModules[i], simulChildModules[i]);
                }
                SetAllChildModuleSimulTransfromer(mainChildModules[i], simulChildModules[i], coordinateCorrector);
            }
        }

        /// <summary>
        /// 添加 RunnableModule，按照先访问根节点，再访问子节点顺序遍历添加
        /// </summary>
        public void AddModule(RunnableModule module, RunnableModule parentModule, int level)
        {
            moduleTree.AddModule(module, parentModule, level);
        }

        /// <summary>
        /// 获取所有的RunableModules
        /// </summary>
        /// <returns></returns>
        public List<RunnableModule> GetAllRunnableModules()
        {
            return moduleTree.Map.Keys.ToList();
        }

        /// <summary>
        /// 记录 RunnableModule -- SymbolLinesCmd 映射关系，语法解析时
        /// </summary>
        /// <param name="module"></param>
        /// <param name="markCmd"></param>
        public void RecordSymbolLinesCmd(RunnableModule module, SymbolLinesCmd symbolCmd)
        {
            List<SymbolLinesCmd> symbolCmdSet;
            if (!symbolLinesMap.ContainsKey(module))
            {
                symbolCmdSet = new List<SymbolLinesCmd>();
                symbolLinesMap.Add(module, symbolCmdSet);
            }
            else
            {
                symbolCmdSet = symbolLinesMap[module];
            }
            symbolCmdSet.Add(symbolCmd);
        }

        /// <summary>
        /// 获取所有SymbolLinesCmd
        /// </summary>
        /// <returns></returns>
        public List<SymbolLinesCmd> GetAllSymbolLines()
        {
            List<SymbolLinesCmd> symbolLinesCmdList = new List<SymbolLinesCmd>();
            foreach (RunnableModule module in symbolLinesMap.Keys)
            {
                symbolLinesCmdList.AddRange(symbolLinesMap[module]);
            }
            return symbolLinesCmdList;
        }

        /// <summary>
        /// 记录 RunnableModule -- MarkCmd 映射关系，语法解析时
        /// </summary>
        /// <param name="module"></param>
        /// <param name="markCmd"></param>
        public void RecordMarkPoint(RunnableModule module, MarkCmd markCmd)
        {
            List<MarkCmd> markCmdSet;
            if (!markMap.ContainsKey(module))
            {
                markCmdSet = new List<MarkCmd>();
                markMap.Add(module, markCmdSet);
            }
            else
            {
                markCmdSet = markMap[module];
            }
            markCmdSet.Add(markCmd);
        }

        /// <summary>
        /// 获取所有mark点，拍mark时
        /// </summary>
        /// <returns></returns>
        public List<MarkCmd> GetAllMarkPoints()
        {
            List<MarkCmd> markCmdList = new List<MarkCmd>();
            foreach (RunnableModule module in markMap.Keys)
            {
                markCmdList.AddRange(markMap[module]);
            }
            return markCmdList;
        }

        public bool SetAllMarkFlyOffset(List<VectorD> offsetList)
        {
            List<MarkCmd> markCmdList = this.GetAllMarkPoints();
            if (markCmdList.Count != offsetList.Count)
            {
                return false;
            }
            for (int i = 0; i < markCmdList.Count; i++)
            {
                markCmdList[i].FlyOffset.X = offsetList[i].X;
                markCmdList[i].FlyOffset.Y = offsetList[i].Y;
            }
            return true;
        }
        /// <summary>
        /// 判断是否所有Mark都在同一层级
        /// </summary>
        /// <returns></returns>
        public bool AllMarkIsSameLevel()
        {
            if (markMap.Count <= 1)
            {
                return true;
            }
            int level = -1;
            foreach (RunnableModule item in markMap.Keys)
            {
                int itemLevel = moduleTree.GetNode(item).Level;
                if (level != -1 && level != itemLevel)
                {
                    return false;
                }
                level = itemLevel;
            }
            return true;
        }

        /// <summary>
        /// 记录 RunnableModule -- BADMarkCmd 映射关系 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="markCmd"></param>
        public void RecordBadMarkPoint(RunnableModule module, BadMarkCmd badMarkCmd)
        {
            if (badMarkMap == null)
            {
                badMarkMap = new Dictionary<RunnableModule, List<BadMarkCmd>>();
            }
            List<BadMarkCmd> badMarkList;
            if (!badMarkMap.ContainsKey(module))
            {
                badMarkList = new List<BadMarkCmd>();
                badMarkMap.Add(module, badMarkList);
            }
            else
            {
                badMarkList = badMarkMap[module];
            }
            badMarkList.Add(badMarkCmd);
        }

        /// <summary>
        /// 记录 RunnableModule -- BarcodeCmd 映射关系 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="barcodeCmd"></param>
        public void RecordBarcodeCmds(RunnableModule module, BarcodeCmd barcodeCmd)
        {
            if (barcodeMap == null)
            {
                barcodeMap = new Dictionary<RunnableModule, List<BarcodeCmd>>();
            }
            List<BarcodeCmd> barcodeList;
            if (!barcodeMap.ContainsKey(module))
            {
                barcodeList = new List<BarcodeCmd>();
                barcodeMap.Add(module, barcodeList);
            }
            else
            {
                barcodeList = barcodeMap[module];
            }
            barcodeList.Add(barcodeCmd);
        }
        /// <summary>
        ///  记录 RunnableModule -- MeasureCmd 映射关系 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="measureCmd"></param>
        public void RecordMeasureGlueHTCmds(RunnableModule module, MeasureCmd measureCmd)
        {
            if (this.measureMap == null)
            {
                measureMap = new Dictionary<RunnableModule, List<MeasureCmd>>();
            }
            List<MeasureCmd> measureList;
            if (!this.measureMap.ContainsKey(module))
            {
                measureList = new List<MeasureCmd>();
                this.measureMap.Add(module, measureList);
            }
            else
            {
                measureList = this.measureMap[module];
            }
            measureList.Add(measureCmd);
        }
        /// <summary>
        ///  记录 RunnableModule -- BlobsCmd 映射关系 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="measureCmd"></param>
        public void RecordBlobsCmds(RunnableModule module, BlobsCmd blobsCmd)
        {
            if (this.blobsMap == null)
            {
                blobsMap = new Dictionary<RunnableModule, List<BlobsCmd>>();
            }
            List<BlobsCmd> blobsList;
            if (!this.blobsMap.ContainsKey(module))
            {
                blobsList = new List<BlobsCmd>();
                this.blobsMap.Add(module, blobsList);
            }
            else
            {
                blobsList = this.blobsMap[module];
            }
            blobsList.Add(blobsCmd);
        }

        public List<MeasureHeightCmd> GetAllMeasureGlueHTCmds()
        {
            List<MeasureHeightCmd> measureHeightCmds = new List<MeasureHeightCmd>();
            foreach (RunnableModule rm in this.measureMap.Keys)
            {
                foreach (MeasureCmd item in this.measureMap[rm])
                {
                    if (item.MeasureContent.HasFlag(MeasureContents.GlueHeight) && item.MeasureHeightCmds!=null && item.MeasureHeightCmds.Count==2)
                    {
                        measureHeightCmds.Add(item.MeasureHeightCmds[0]);
                    }
                }
                
            }
            return measureHeightCmds;
        }
        /// <summary>
        /// 记录RunnableModule与 每个测高点的映射关系
        /// </summary>
        /// <param name="runnableModule"></param>
        /// <param name="measureHeightCmd"></param>
        public void RecordMeasureHeightPoint(RunnableModule runnableModule,MeasureHeightCmd measureHeightCmd)
        {
            if (measureHeightMap ==null)
            {
                measureHeightMap = new Dictionary<RunnableModule, List<MeasureHeightCmd>>();
            }
            List<MeasureHeightCmd> measureHeightList;
            if (!measureHeightMap.ContainsKey(runnableModule))
            {
                measureHeightList = new List<MeasureHeightCmd>();
                measureHeightMap.Add(runnableModule, measureHeightList);
            }
            else
            {
                measureHeightList = measureHeightMap[runnableModule];
            }
            measureHeightList.Add(measureHeightCmd);
        }
        
        public List<MeasureHeightCmd> GetAllMeasureHeightCmds()
        {
            List<MeasureHeightCmd> measureHeightList = new List<MeasureHeightCmd>();
            foreach (RunnableModule runnableModule in measureHeightMap.Keys)
            {
                if (runnableModule.Mode == ModuleMode.SkipMode)
                {
                    continue;
                }
                measureHeightList.AddRange(measureHeightMap[runnableModule]);
            }
            return measureHeightList;
        }

        /// <summary>
        /// Pattern坐标 -> 机械坐标
        /// </summary>
        /// <param name="module"></param>
        /// <param name="posInPattern"></param>
        /// <returns></returns>
        public PointD ToMachine(RunnableModule module, PointD posInPattern)
        {
            PointD p = new PointD(posInPattern);
            ModuleNode node = moduleTree.GetNode(module);
            if (node == null)
            {
                throw new Exception("RunnableModule [" + module.CommandsModule.Name + "] is not found.");
            }
            while (node != null && node.Level > 0)
            {
                if(node.Module.CommandsModule is Workpiece)
                {
                    //p += (node.Module.CommandsModule as Workpiece).GetOriginPos().ToSystem();
                    p += node.Module.Origin.ToSystem();
                }
                else
                {
                    p += node.Module.Origin;
                }
                node = node.Parent;
            }
            return p.ToMachine();
        }

        /// <summary>
        /// 机械坐标 -> Pattern坐标
        /// </summary>
        /// <param name="posInMachine"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public PointD ToPattern(PointD posInMachine, RunnableModule module)
        {
            PointD posInPattern = new PointD(posInMachine);
            ModuleNode node = moduleTree.GetNode(module);
            if (node == null)
            {
                throw new Exception("RunnableModule [" + module.CommandsModule.Name + "] is not found.");
            }
            while (node != null && node.Level > 0)
            {
                posInPattern.X -= node.Module.Origin.X;
                posInPattern.Y -= node.Module.Origin.Y;
                node = node.Parent;
            }
            return posInPattern;
        }

        public void Clear()
        {
            Log.Print("RunnableModuleStructure.Clear()");
            moduleTree.Clear();
            markMap?.Clear();
            measureHeightMap?.Clear();
            badMarkMap?.Clear();
            barcodeMap?.Clear();
            measureMap?.Clear();
            blobsMap?.Clear();
            AllBoardsNo?.Clear();
            symbolLinesMap?.Clear();
        }

        public void Print()
        {
            moduleTree.Print();
        }

        /// <summary>
        /// CommandsModule树结构
        /// </summary>
        [Serializable]
        private class ModuleTree
        {
            private ModuleNode root;
            /// <summary>
            /// 根节点
            /// </summary>
            public ModuleNode Root { get { return root; } }
            // 所有节点
            private List<ModuleNode> allNodesList = new List<ModuleNode>();
            // RunnableModule与对应的节点
            public Dictionary<RunnableModule, ModuleNode> Map { get; private set; } = new Dictionary<RunnableModule, ModuleNode>();

            /// <summary>
            /// 添加 RunnableModule，按照先访问根节点，再访问子节点顺序遍历添加
            /// </summary>
            public void AddModule(RunnableModule module, RunnableModule parentModule, int level)
            {
                //Log.Print("AddModule Begine origin:" + module.Origin+ module.CommandsModule.Name);
                if (module == null)
                {
                    throw new Exception("module can not be null.");
                }               
                if (root == null)
                {
                    if (parentModule != null)
                    {
                        throw new Exception("Please add root node firstly.");
                    }
                    //Log.Print("Add root module");
                    root = new ModuleNode(null, module, level);
                    allNodesList.Add(root);
                    Map.Add(module, root);
                    return;
                }
                // 子节点的parentModule不能为null
                if (parentModule == null)
                {
                    throw new Exception("Parent of runnable module [" + module.CommandsModule.Name + "] is null.");
                }
                ModuleNode parentNode = GetNode(parentModule);
                if (parentNode == null)
                {
                    throw new Exception("Parent runnable module [" + parentModule.CommandsModule.Name + "] is not found in runnable module tree.");
                }
                ModuleNode node = new ModuleNode(parentNode, module, level);
                parentNode.Children.Add(node);
                allNodesList.Add(node);
                Map.Add(module, node);
                //Log.Print("AddModule Done:" + module.CommandsModule.Name);
               
            }

            public ModuleNode GetNode(RunnableModule module)
            {               
                if (!Map.ContainsKey(module))
                {                    
                    return null;
                }               
                return Map[module];
            }

            public void Print()
            {
                Log.Print("---------runnable module tree begin----------");
                print(root);
                Log.Print("---------runnable module tree end----------");
            }

            private void print(ModuleNode node)
            {
                if (node == null)
                {
                    return;
                }
                Log.Print("RunnableModule " + node.Module.CommandsModule.Name + ", origin=" 
                    + node.Module.Origin + ", level=" + node.Level);
                foreach (ModuleNode child in node.Children)
                {
                    print(child);
                }
            }

            /// <summary>
            /// 清空pattern树
            /// </summary>
            public void Clear()
            {
                Log.Print("module tree clear.");
                root = null;
                allNodesList.Clear();
                Map.Clear();
            }

            /// <summary>
            /// pattern树是否为空
            /// </summary>
            public bool IsEmpty
            {
                get { return root == null; }
            }
        }

        [Serializable]
        private class ModuleNode
        {
            private ModuleNode parent;
            /// <summary>
            /// 父节点
            /// </summary>
            public ModuleNode Parent { get { return parent; } }

            /// <summary>
            /// 子节点
            /// </summary>
            public List<ModuleNode> Children = new List<ModuleNode>();

            private RunnableModule module;
            public RunnableModule Module { get { return module; } }

            private int level;
            public int Level { get { return level; } }

            public ModuleNode(ModuleNode parent, RunnableModule module, int level)
            {
                this.parent = parent;
                this.module = module;
                this.level = level;
            }
        }
    }
}
