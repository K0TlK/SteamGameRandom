using System;
using System.Collections.Generic;
using UnityEngine;

namespace StarterPack
{

    public class PriorityEvent<T> : IDisposable
    {
        protected List<(Action<T>, int)> _subscribersList = new List<(Action<T>, int)>();

        /// <param name="callback">The method that is called when the event fires. It is recommended to use a tuple as the T parameter</param>
        /// <param name="priority">Callback call priority. Subscribers with a larger number are called before the rest</param>
        public void Subscribe(Action<T> callback, int priority = 0)
        {
            (Action<T>, int) subscriber = (callback, priority);
            for (var i = 0; i < _subscribersList.Count; i++)
            {
                int currentPriority = _subscribersList[i].Item2;
                if (currentPriority < priority)
                {
                    _subscribersList.Insert(i, subscriber);
                    return;
                }
            }

            _subscribersList.Add(subscriber);
        }

        /// <param name="callback">Previously Signed Method</param>
        public void Unsubscribe(Action<T> callback)
        {
            _subscribersList.RemoveAll(pair => pair.Item1 == callback);
        }

        /// <summary>
        /// Unsubscribes all subscribers. Do not use this method outside the class where the event is defined
        /// </summary>
        public virtual void Dispose()
        {
            _subscribersList.Clear();
        }

        /// <summary>
        /// Causes callbacks for all subscribers. Do not use this method outside the class where the event is defined
        /// </summary>
        public virtual void Invoke(T args)
        {
            for (var i = 0; i < _subscribersList.Count; i++)
            {
                _subscribersList[i].Item1.Invoke(args);
            }
        }
    }

    public class PriorityEvent : IDisposable
    {
        protected List<(Action, int)> _subscribersList = new List<(Action, int)>();

        /// <param name="callback">The method that is called when the event fires. It is recommended to use a tuple as the T parameter</param>
        /// <param name="priority">Callback call priority. Subscribers with a larger number are called before the rest</param>
        public void Subscribe(Action callback, int priority = 0)
        {
            (Action, int) subscriber = (callback, priority);
            for (var i = 0; i < _subscribersList.Count; i++)
            {
                int currentPriority = _subscribersList[i].Item2;
                if (currentPriority < priority)
                {
                    _subscribersList.Insert(i, subscriber);
                    return;
                }
            }

            _subscribersList.Add(subscriber);
        }

        /// <param name="callback">Previously Signed Method</param>
        public void Unsubscribe(Action callback)
        {
            _subscribersList.RemoveAll(pair => pair.Item1 == callback);
        }

        /// <summary>
        /// Unsubscribes all subscribers. Do not use this method outside the class where the event is defined
        /// </summary>
        public virtual void Dispose()
        {
            _subscribersList.Clear();
        }

        /// <summary>
        /// Causes callbacks for all subscribers. Do not use this method outside the class where the event is defined
        /// </summary>
        public virtual void Invoke()
        {
            for (var i = 0; i < _subscribersList.Count; i++)
            {
                _subscribersList[i].Item1.Invoke();
            }
        }
    }
}