using System;

namespace LibraProgramming.Windows.Dependency
{
    /*public sealed class ObservablePropertyTracker : IObservablePropertyTracker
    {
        public static IObservablePropertyTracker Create<TModel>(Action<IObservablePropertyTrackerBuilder<TModel>> configurator)
            where TModel : ObservableModel
        {
            if (null == configurator)
            {
                throw new ArgumentNullException(nameof(configurator));
            }

            var builder = new ObservablePropertyTrackerBuilder<TModel>();

            configurator(builder);

            return builder.Construct();
        }
    }*/
}