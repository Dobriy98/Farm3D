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
        private readonly TContext _context;
        private AState _currentState;

        public Fsm(TContext ctx, AState initState)
        {
            _context = ctx;
            _currentState = initState;
            initState.Fsm = this;
            initState.Context = _context;
            initState.Enter();
        }
        
        public void ChangeState<TState>(TState state) where TState: AState
        {
            var newState = state;

            _currentState?.Exit();
            _currentState = newState;
            newState.Fsm = this;
            newState.Context = _context;
            newState.Enter();
        }
        
        public void Signal<TSignal>(TSignal signal = default)
        {
            if (_currentState is ISignalHandler<TSignal> handler)
                handler.Signal(signal);
        }
        
        public void DoUpdate() => _currentState?.Update();

        public bool CompareState<TState>() => _currentState?.GetType() == typeof(TState);
        
        
        
        public abstract class AState
        {
            public Fsm<TContext> Fsm { get; set; }
            public TContext Context { get; set; }
            public virtual void Enter() { }
            public virtual void Update() { }
            public virtual void Exit() { }
        }
    }
}