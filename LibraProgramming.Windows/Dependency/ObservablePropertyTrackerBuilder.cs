using System;
using System.Linq.Expressions;

namespace LibraProgramming.Windows.Dependency
{
    public class ObservablePropertyTrackerBuilder<TModel> : IObservablePropertyTrackerBuilder<TModel>
        where TModel : ObservableModel
    {
        public IDependentObservablePropertyBuilder<TModel> RaiseProperty(Expression<Func<TModel, object>> selector)
        {
            throw new NotImplementedException();
        }

        public IObservablePropertyTracker Construct()
        {
            throw new NotImplementedException();
        }
    }
}