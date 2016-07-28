using System;
using System.Linq.Expressions;
using LibraProgramming.Windows.Infrastructure;

namespace LibraProgramming.Windows.Dependency
{
    public interface IObservablePropertyTrackerBuilder<TModel> : IObjectBuilder<IObservablePropertyTracker>
        where TModel : ObservableModel
    {
        IDependentObservablePropertyBuilder<TModel> RaiseProperty(Expression<Func<TModel, object>> selector);
    }
}