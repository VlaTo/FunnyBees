using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;

namespace LibraProgramming.Windows.Infrastructure
{
    public class TypedWeakEventHandler<TSender, TResult>
        where TResult : EventArgs
    {
        private readonly IList<WeakDelegate<TypedEventHandler<TSender, TResult>>> handlers;

        /// <summary>
        /// 
        /// </summary>
        public bool IsAlive
        {
            get
            {
                var count = 0;
                var delegates = handlers.ToArray();

                foreach (var handler in delegates)
                {
                    if (handler.IsAlive)
                    {
                        count++;
                    }
                    else
                    {
                        handlers.Remove(handler);
                    }
                }

                return count > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TypedWeakEventHandler()
        {
            handlers = new List<WeakDelegate<TypedEventHandler<TSender, TResult>>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventHandler"></param>
        public void AddHandler(TypedEventHandler<TSender, TResult> eventHandler)
        {
            var handler = (Delegate)((object)eventHandler);
            var @delegate = new WeakDelegate<TypedEventHandler<TSender, TResult>>(handler);

            if (false == handlers.Contains(@delegate))
            {
                handlers.Add(@delegate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventHandler"></param>
        public void RemoveHandler(TypedEventHandler<TSender, TResult> eventHandler)
        {
            var handler = (Delegate)((object)eventHandler);
            handlers.Remove(new WeakDelegate<TypedEventHandler<TSender, TResult>>(handler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Invoke(object sender, EventArgs args)
        {
            var delegates = handlers.ToArray();

            foreach (var handler in delegates)
            {
                if (handler.IsAlive)
                {
                    handler.Invoke(sender, args);
                }
                else
                {
                    handlers.Remove(handler);
                }
            }
        }
    }
}