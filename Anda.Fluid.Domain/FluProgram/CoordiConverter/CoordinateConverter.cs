using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.CoordiConverter
{

    /// <summary>
    /// 坐标转换
    /// 1. 机械坐标 -> pattern坐标
    /// 2. pattern坐标 -> 机械坐标
    /// </summary>
    public class CoordinateConverter
    {
        private PatternTree patternTree = new PatternTree();

        private CordinateSystemManager csm = new CordinateSystemManager();

        /// <summary>
        /// 开始添加pattern
        /// </summary>
        public void StartAddingPattern()
        {
            patternTree.Clear();
        }

        /// <summary>
        /// 结束添加pattern
        /// </summary>
        public void StopAddingPattern()
        {
            calculateIndex(patternTree.Root, 1);
            csm = new CordinateSystemManager();
            addCoordinateSystem(patternTree.Root);
            // PRINT
            patternTree.Print();
        }

        private void addCoordinateSystem(PatternNode node)
        {
            Log.Print("Add CordinateSystem, level=" + node.Level + ", index=" + node.Index + ", parentIndex=" + node.Parent.Index);
            csm.AddCordinateSystem(new CordinateSystem(node.Level, node.Index, node.Parent.Index, node.Origin));
            foreach (PatternNode n in node.Children)
            {
                addCoordinateSystem(n);
            }
        }

        /// <summary>
        /// 计算各节点所在层级的index
        /// </summary>
        /// <param name="node"></param>
        private void calculateIndex(PatternNode node, int index)
        {
            node.Index = index;
            for (int i = 0; i < node.Children.Count; i++)
            {
                calculateIndex(node.Children[0], i + 1);
            }
        }

        /// <summary>
        /// 添加pattern，按照中序遍历的顺序添加，即先访问根节点，再访问子节点
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="parent">patttern所属的外层pattern</param>
        /// <param name="origin">patttern原点, 注意是命令中的引用Pattern的位置，不是创建Pattern时的原点</param>
        /// <param name="level">pattern所处的层级，WorkPiece层级为1</param>
        public void AddPattern(Pattern pattern, Pattern parent, PointD origin, int level)
        {
            patternTree.AddPattern(pattern, parent, origin, level);
        }

        /// <summary>
        /// 运行模式下 pattern坐标 -> 机械坐标 (pattern的原点坐标是相对于外层pattern的原点坐标的)
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="patternPoint"></param>
        /// <returns></returns>
        public PointD ToMachine(Pattern pattern, PointD patternPoint)
        {
            PointD machinePoint = new PointD();


            return machinePoint;
        }

        /// <summary>
        /// 运行模式下 机械坐标 -> pattern坐标 (pattern的原点坐标是相对于外层pattern的原点坐标的)
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="machinePoint"></param>
        /// <returns></returns>
        public PointD ToPattern(Pattern pattern, PointD machinePoint)
        {
            PointD patternPoint = new PointD();

            return patternPoint;
        }

        public void Clear()
        {
            patternTree.Clear();
            csm = new CordinateSystemManager();
        }

        /// <summary>
        /// 编程模式下 pattern坐标 -> 机械坐标 （pattern的原点坐标是相对于机械坐标系的）
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="patternPoint"></param>
        /// <returns></returns>
        public PointD ToMachineS(Pattern pattern, PointD patternPoint)
        {
            PointD machinePoint = new PointD();
            machinePoint.X = patternPoint.X + pattern.Origin.X;
            machinePoint.Y = patternPoint.Y + pattern.Origin.Y;
            return machinePoint;
        }

        /// <summary>
        /// 编程模式下 机械坐标 -> pattern坐标 （pattern的原点坐标是相对于机械坐标系的）
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="machinePoint"></param>
        /// <returns></returns>
        public static PointD ToPatternS(Pattern pattern, PointD machinePoint)
        {
            PointD patternPoint = new PointD();
            patternPoint.X = machinePoint.X - pattern.Origin.X;
            patternPoint.Y = machinePoint.Y - pattern.Origin.Y;
            return patternPoint;
        }

        /// <summary>
        /// pattern树
        /// </summary>
        private class PatternTree
        {
            private PatternNode root;
            public PatternNode Root { get { return root; } }

            private List<PatternNode> allNodesList = new List<PatternNode>();
            private Dictionary<Pattern, PatternNode> map = new Dictionary<Pattern, PatternNode>();

            /// <summary>
            /// 添加pattern，按照中序遍历的顺序添加，即先访问根节点，再访问子节点
            /// </summary>
            /// <param name="pattern"></param>
            /// <param name="parentPattern">patttern所属的外层pattern</param>
            /// <param name="origin">patttern原点</param>
            /// <param name="level">pattern所处的层级，WorkPiece层级为1</param>
            public void AddPattern(Pattern pattern, Pattern parentPattern, PointD origin, int level)
            {
                if (pattern == null)
                {
                    throw new Exception("pattern can not be null.");
                }
                if (pattern is Workpiece)
                {
                    if (root != null)
                    {
                        throw new Exception("Workpiece has already been added!");
                    }
                    root = new PatternNode(null, pattern, parentPattern, origin, level);
                    allNodesList.Add(root);
                    map.Add(pattern, root);
                    return;
                }
                if (parentPattern == null)
                {
                    throw new Exception("Parent pattern of pattern " + pattern.Name + " can not be null.");
                }
                PatternNode parentNode = allNodesList.Find((obj) => { return parentPattern == obj.Pattern; });
                if (parentNode == null)
                {
                    throw new Exception("Parent " + parentPattern.Name + "is not found in pattern tree.");
                }
                PatternNode node = new PatternNode(parentNode, pattern, parentPattern, origin, level);
                parentNode.Children.Add(node);
                allNodesList.Add(node);
                map.Add(pattern, node);
            }

            public PatternNode GetNode(Pattern pattern)
            {
                if (!map.ContainsKey(pattern))
                {
                    return null;
                }
                return map[pattern];
            }

            public void Print()
            {
                Log.Print("---------pattern tree begin----------");
                foreach (PatternNode node in allNodesList)
                {
                    Log.Print("pattern " + node.Pattern.Name + ", level=" + node.Level + ", index=" + node.Index);
                }
                Log.Print("---------pattern tree end----------");
            }

            /// <summary>
            /// 清空pattern树
            /// </summary>
            public void Clear()
            {
                root = null;
                allNodesList.Clear();
            }
        }

        private class PatternNode
        {
            private PatternNode parent;
            public PatternNode Parent { get { return parent; } }

            private Pattern pattern;
            public Pattern Pattern { get { return pattern; } }

            private Pattern parentPattern;
            public Pattern ParentPattern { get { return parentPattern; } }

            private PointD origin;
            public PointD Origin;

            private int level;
            public int Level { get { return level; } }

            public int Index;

            public List<PatternNode> Children = new List<PatternNode>();

            public PatternNode(PatternNode parent, Pattern pattern, Pattern parentPattern, PointD origin, int level)
            {
                this.parent = parent;
                this.pattern = pattern;
                this.parentPattern = parentPattern;
                this.origin = origin;
                this.level = level;
            }
        }
    }
}
