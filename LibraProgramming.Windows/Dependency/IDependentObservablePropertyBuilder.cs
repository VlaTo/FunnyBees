using System;

namespace LibraProgramming.Windows.Dependency
{
    public interface IDependentObservablePropertyBuilder<TModel>
        where TModel : ObservableModel
    {
        IDependecyObservablePropertyBuilder<TModel> CalculatedBy(Func<TModel, object> calculator);
    }
}