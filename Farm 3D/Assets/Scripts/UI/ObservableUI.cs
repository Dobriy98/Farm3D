using System;

namespace UI
{
    public class ObservableUI<T>
    {
        public Action<T> Listeners;
        private T _value;
        
        public ObservableUI(T value = default)
        {
            _value = value;
        }
        
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Listeners?.Invoke(_value);
            }
        }
    }
}