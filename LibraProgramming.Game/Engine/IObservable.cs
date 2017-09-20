using System;

namespace FunnyBees.Engine
{
    public interface IObservable<in TObserver>
        where TObserver : IObserver
    {
        IDisposable Subscribe(TObserver observer);
    }
}