using System;

namespace FunnyBees.Engine
{
    public interface IComponentObservable
    {
        IDisposable Subscribe(IComponentObserver observer);
    }
}