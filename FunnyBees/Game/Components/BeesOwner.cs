using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FunnyBees.Engine;
using LibraProgramming.Windows;

namespace FunnyBees.Game.Components
{
    public class BeesOwner : Component, Engine.IObservable<IBeeLifetimeObserver>
    {
        private readonly ICollection<Bee> bees;
        private readonly ICollection<IBeeLifetimeObserver> lifetimeObservers;

        public IEnumerable<Bee> Bees => bees;

        public BeesOwner()
        {
            bees = new List<Bee>();
            lifetimeObservers = new Collection<IBeeLifetimeObserver>();
        }

        public void AddBee(Bee bee)
        {
            if (null == bee)
            {
                throw new ArgumentNullException(nameof(bee));
            }

            bees.Add(bee);

            foreach (var observer in lifetimeObservers)
            {
                observer.OnAdded(bee);
            }
        }

        public void RemoveBee(Bee bee)
        {
            if (null == bee)
            {
                throw new ArgumentNullException(nameof(bee));
            }

            if (bees.Remove(bee))
            {
                foreach (var observer in lifetimeObservers)
                {
                    observer.OnRemoved(bee);
                }
            }
        }

        IDisposable Engine.IObservable<IBeeLifetimeObserver>.Subscribe(IBeeLifetimeObserver observer)
        {
            if (false == lifetimeObservers.Contains(observer))
            {
                lifetimeObservers.Add(observer);
            }

            return new DisposableToken<IBeeLifetimeObserver>(observer, RemoveLifetimeObserver);
        }

        private void RemoveLifetimeObserver(IBeeLifetimeObserver observer)
        {
            lifetimeObservers.Remove(observer);
        }
    }
}