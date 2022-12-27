using System;
using System.Collections.Generic;

namespace Common
{
    public interface IFsm
    {
        void DoUpdate();
        public bool CompareState<TState>();
    }
    
    public class Fsm : IFsm
    {
        private readonly Dictionary<Type, AState> _states;
        private AState _currentState;

        public Fsm()
        {
            _states = new Dictionary<Type, AState>();
        }

        public void AddState<TState>(AState state)
        {
            var type = typeof(TState);
            _states[type] = state;
        }
        
        public void ChangeState<TState>() where TState: AState
        {
            var type = typeof(TState);
            if (!_states.ContainsKey(type)) return;
            
            _currentState?.Exit();
            _currentState = _states[type];
            _currentState.Fsm = this;
            _states[type].Enter();
        }

        public void ChangeState<TState, TArg>(TArg arg) where TState : AState<TArg>
        {
            var type = typeof(TState);
            if(!_states.ContainsKey(type)) return;
            
            var newState = (AState<TArg>)_states[typeof(TState)];
            
            _currentState?.Exit();
            _currentState = newState;
            
            newState.Fsm = this;
            newState.SetStateArg(arg);
            newState.Enter();
        }

        public void ChangeState(AState state)
        {
            if(!_states.ContainsValue(state)) return;
            _currentState?.Exit();
        
            _currentState = state;
            _currentState.Fsm = this;
            state.Enter();
        }

        public TState TakeState<TState>() where TState : AState => _states[typeof(TState)] as TState;

        public TState TakeStateWithArgs<TState, TArg>(TArg arg) where TState : AState<TArg>
        {
            AState<TArg> state = _states[typeof(TState)] as AState<TArg>;
            state?.SetStateArg(arg);
            return state as TState;
        }

        public void DoUpdate() => _currentState?.Update();

        public bool CompareState<TState>() => _currentState?.GetType() == typeof(TState);
        
        
        
        public abstract class AState
        {
            public Fsm Fsm { get; set; }
            public virtual void Enter(){ }
            public virtual void Update() { }
            public virtual void Exit() { }
        }

        public abstract class AState<TArg> : AState
        {
            public abstract void SetStateArg(TArg arg);
        }
    }
}