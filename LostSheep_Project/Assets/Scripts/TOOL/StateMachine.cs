using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TOOL
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T>
    {

        public State()
        {

        }

        //进入状态  
        public virtual void Enter(T root)
        {

        }

        //状态正常执行
        public virtual void Execute(T root)
        {

        }

        //退出状态
        public virtual void Exit(T root)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoState<T> : MonoBehaviour
    {
        //进入状态  
        public virtual void Enter(T root)
        {

        }

        //状态正常执行
        public virtual void Execute(T root)
        {

        }

        //退出状态
        public virtual void Exit(T root)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StateMachine<T>
    {

        private State<T> currentState;
        private List<State<T>> globalStates;
        private Dictionary<System.Enum, State<T>> states;
        private T root;

        public StateMachine(T _root)
        {
            root = _root;
            currentState = null;
            globalStates = new List<State<T>>();
            states = new Dictionary<System.Enum, State<T>>();
        }

        public void Add(System.Enum key, State<T> node)
        {
            if (!states.ContainsKey(key))
            {
                states.Add(key, node);
            }
        }

        public State<T> Get(System.Enum key)
        {
            if (states.ContainsKey(key))
            {
                return states[key];
            }
            else
            {
                return null;
            }
        }

        public void SetGlobalState(System.Enum key)
        {
            State<T> state = Get(key);
            if (!globalStates.Contains(state))
            {
                state.Enter(root);
                globalStates.Add(state);
            }
        }

        public void SetCurrentState(System.Enum key)
        {
            State<T> state = Get(key);
            currentState = state;
            currentState.Enter(root);
        }

        public void Update()
        {
            
            //全局状态的运行
            foreach (State<T> state in globalStates)
            {
                state.Execute(root);
            }

            //一般当前状态的运行
            if (currentState != null)
                currentState.Execute(root);
        }

        public void ChangeState(System.Enum key)
        {
            State<T> state = Get(key);
            if (state == null)
            {
                Debug.LogError("该状态不存在: " + key);
                return;
            }

            if (currentState == state)
            {
                Debug.LogError("该状态已存在: " + key);
                return;
            }

            //退出之前状态
            if (currentState != null)
                currentState.Exit(root);

            //设置当前状态
            currentState = state;

            //进入当前状态
            if (currentState != null)
                currentState.Enter(root);
        }

        public State<T> CurrentState()
        {
            //返回目前状态 
            return currentState;
        }
        public List<State<T>> GlobalStates()
        {
            //返回全局状态 
            return globalStates;
        }

        //
        public void RemoveGlobalState(State<T> state)
        {
            if (globalStates.Contains(state))
            {
                state.Exit(root);
                globalStates.Remove(state);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoStateMachine<T>
    {

        private MonoState<T> currentState;
        private MonoState<T> previousState;
        private List<MonoState<T>> globalStates;
        private T root;

        public MonoStateMachine(T _root)
        {
            root = _root;
            currentState = null;
            previousState = null;
            globalStates = new List<MonoState<T>>();
        }

        public void SetGlobalState(MonoState<T> state)
        {
            if (!globalStates.Contains(state))
            {
                state.Enter(root);
                globalStates.Add(state);
            }
        }

        public void RemoveGlobalState(MonoState<T> state)
        {
            if (globalStates.Contains(state))
            {
                state.Exit(root);
                globalStates.Remove(state);
            }
        }

        public void SetCurrentState(MonoState<T> state)
        {
            currentState = state;
            currentState.Enter(root);
        }

        public void Update()
        {
            //全局状态的运行
            foreach (MonoState<T> state in globalStates)
            {
                state.Execute(root);
            }

            //一般当前状态的运行
            if (currentState != null)
                currentState.Execute(root);
        }

        public void ChangeState(MonoState<T> pNewState)
        {
            if (pNewState == null)
            {
                Debug.LogError("该状态不存在");
            }

            if (currentState != pNewState)
            {
                //退出之前状态
                if (currentState != null)
                    currentState.Exit(root);

                //保存之前状态
                previousState = currentState;

                //设置当前状态
                currentState = pNewState;

                //进入当前状态
                if (currentState != null)
                    currentState.Enter(root);
            }
        }

        public void RevertToPreviousState()
        {
            //qie huan dao qian yi ge zhuang tai 
            ChangeState(previousState);
        }

        public MonoState<T> CurrentState()
        {
            //fan hui dang qian zhuang tai 
            return currentState;
        }
        public List<MonoState<T>> GlobalStates()
        {
            //fan hui quan ju zhuang tai 
            return globalStates;
        }
        public MonoState<T> PreviousState()
        {
            //fan hui qian yi ge zhuang tai 
            return previousState;
        }
    }

}

