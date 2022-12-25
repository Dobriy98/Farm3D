using System;
using System.Collections.Generic;

namespace Common
{
    public interface IFsm
    {
        void Signal<TSignal>(TSignal signal);
        void DoUpdate();
        public bool CompareState<TState>();
    }
    
    public interface ISignalHandler<in TSignal>
    {
        public void Signal(TSignal signal);
    }
    
    public class Fsm<TContext> : IFsm
    {
        private Dictionary<Type, AState> _states;
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
            _currentState?.Exit();
            
            var type = typeof(TState);
            if (_states.ContainsKey(type))
            {
                _currentState = _states[type];
                _currentState.Fsm = this;
                _states[type].Enter();
            }
        }

        public void ChangeState<TState, TArg>(TArg arg) where TState : AState<TArg>
        {
            var newState = (AState<TArg>)_states[typeof(TState)];
            
            _currentState?.Exit();
            
            _currentState = newState;
            newState.Fsm = this;
            newState.SetStateArg(arg);
            newState.Enter();
        }

        public void ChangeState(AState state)
        {
            _currentState?.Exit();
        
            _currentState = state;
            _currentState.Fsm = this;
            state.Enter();
        }
        
        public void Signal<TSignal>(TSignal signal = default)
        {
            if (_currentState is ISignalHandler<TSignal> handler)
                handler.Signal(signal);
        }

        public AState TakeState<TState>() => _states[typeof(TState)];

        public AState TakeStateWithArgs<TState, TArg>(TArg arg) where TState : AState<TArg>
        {
            var state = (AState<TArg>)_states[typeof(TState)];
            state.SetStateArg(arg);
            return state;
        }

        public void DoUpdate() => _currentState?.Update();

        public bool CompareState<TState>() => _currentState?.GetType() == typeof(TState);
        
        
        
        public abstract class AState
        {
            public Fsm<TContext> Fsm { get; set; }
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