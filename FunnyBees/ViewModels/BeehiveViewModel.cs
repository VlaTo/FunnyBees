using System.Threading.Tasks;
using LibraProgramming.Windows;
using LibraProgramming.Windows.Dependency;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BeehiveViewModel : ObservableModel, ISetupRequired, ICleanupRequired
    {
        private static readonly ObservableProperty NumberProperty;
        private static readonly ObservableProperty TotalProperty;
        private static readonly IObservablePropertyTracker tracker;

        private IObservableModelSubscription subscription;
//        private long number;
//        private long maximumNumberOfBees;
//        private long currentBeesCount;

        /// <summary>
        /// 
        /// </summary>
        public int Number
        {
            get
            {
                return (int) GetProperty(NumberProperty);
            }
            set
            {
                SetProperty(NumberProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long Total
        {
            get
            {
                return (long) GetProperty(TotalProperty);
            }
            set
            {
                SetProperty(TotalProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /*public long MaximumNumberOfBees
        {
            get
            {
                return maximumNumberOfBees;
            }
            set
            {
                SetProperty(ref maximumNumberOfBees, value);
            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        /*public long CurrentBeesCount
        {
            get
            {
                return currentBeesCount;
            }
            set
            {
                SetProperty(ref currentBeesCount, value);
            }
        }*/

        static BeehiveViewModel()
        {
            NumberProperty = ObservableProperty
                .Register(
                    nameof(Number),
                    typeof (int),
                    typeof (BeehiveViewModel)
                );
            TotalProperty = ObservableProperty
                .Register(
                    nameof(Total),
                    typeof (long),
                    typeof (BeehiveViewModel),
                    new ObservableMetadata(0L)
                );
            tracker = ObservablePropertyTracker.Create<BeehiveViewModel>(observer =>
            {
                observer
                    .RaiseProperty(model => model.Total)
                    .CalculatedBy(model => model.Number * 100)
                    .WhenChanged(model => model.Number);
            });
        }

        public Task SetupAsync()
        {
            subscription = tracker.Subscribe(this);

            using (subscription.Disable())
            {
                
            }

            return Task.CompletedTask;
        }

        public Task CleanupAsync()
        {
            subscription.Dispose();
            return Task.CompletedTask;
        }
    }
}