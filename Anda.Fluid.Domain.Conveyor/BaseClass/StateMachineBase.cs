using Anda.Fluid.Infrastructure.Tasker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.BaseClass
{
    /// <summary>
    /// 状态机基类
    /// </summary>
    public class StateMachineBase
    {
        private StateBase currentState;
        private StateBase prevState;

        private Dictionary<object, StateBase> dic;

        public StateMachineBase()
        {
            this.dic = new Dictionary<object, StateBase>();
        }
        public string CurrentSateName => this.currentState.GetName;
        public string PrevStateName => this.prevState.GetName;

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="state"></param>
        public void Region(object stateName,StateBase state)
        {
            if (!this.dic.ContainsKey(stateName))
            {
                this.dic.Add(stateName, state);
            }             
        }
        /// <summary>
        /// 为状态机设置默认状态
        /// </summary>
        /// <param name="stateName"></param>
        public void SetDefault(object stateName)
        {
            if (this.dic.ContainsKey(stateName))
            {
                this.currentState = this.dic[stateName];
                this.currentState.EnterState();
            }
        }
        /// <summary>
        /// 为状态机切换状态
        /// </summary>
        /// <param name="stateName"></param>
        public void ChangeState(object stateName)
        {
            if (this.dic.ContainsKey(stateName))
            {
                if (this.currentState != null) 
                {
                    this.currentState.ExitState();
                    this.prevState = this.currentState;
                    this.currentState = this.dic[stateName];
                    this.currentState.EnterState();
                }
            }
        }
        /// <summary>
        /// 调用此方法，状态机刷新状态
        /// </summary>
        public void UpdateSate()
        {
            if (this.currentState != null)
            {
                this.currentState.UpdateState();
            }
        }

    }
}
