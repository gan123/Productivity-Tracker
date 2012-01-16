using System.Collections.Generic;

namespace ProductivityTracker.Services.UnitOfWork
{
    public class RequestState : IRequestState
    {
        private static System.Collections.Generic.Dictionary<string, object> _state;

        public RequestState()
        {
            _state = new Dictionary<string, object>();
        }

        public T Get<T>(string key)
        {
            if (_state.ContainsKey(key))
                return (T)_state[key];
            return default(T);
        }

        public void Store(string key, object something)
        {
            _state[key] = something;
        }
    }
}