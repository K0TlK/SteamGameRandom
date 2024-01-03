using UnityEngine;

namespace StarterPack
{
    public class GlobalEvent<T> : PriorityEvent<T>
    {
        public GlobalEvent() { }

        /// <summary>
        /// Unsubscribes all subscribers
        /// </summary>
        public override void Dispose()
        {
            _subscribersList.Clear();
        }

        /// <summary>
        /// Causes callbacks for all subscribers
        /// </summary>
        public override void Invoke(T args)
        {
            for (var i = 0; i < _subscribersList.Count; i++)
            {
                _subscribersList[i].Item1.Invoke(args);
            }
        }
    }

    public class GlobalEvent : PriorityEvent
    {
        public GlobalEvent() { }

        /// <summary>
        /// Unsubscribes all subscribers
        /// </summary>
        public override void Dispose()
        {
            _subscribersList.Clear();
        }

        /// <summary>
        /// Causes callbacks for all subscribers
        /// </summary>
        public override void Invoke()
        {
            for (var i = 0; i < _subscribersList.Count; i++)
            {
                _subscribersList[i].Item1.Invoke();
            }
        }
    }
}