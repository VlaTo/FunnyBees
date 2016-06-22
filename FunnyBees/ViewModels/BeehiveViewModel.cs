using System.Collections.ObjectModel;
using FunnyBees.Models;

namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BeehiveViewModel : ObservableViewModel
    {
        private long number;
        private long maximumNumberOfBees;

        /// <summary>
        /// 
        /// </summary>
        public long Number
        {
            get
            {
                return number;
            }
            set
            {
                SetProperty(ref number, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long MaximumNumberOfBees
        {
            get
            {
                return maximumNumberOfBees;
            }
            set
            {
                SetProperty(ref maximumNumberOfBees, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<IBee> Bees
        {
            get;
        }

        public BeehiveViewModel()
        {
            Bees = new ObservableCollection<IBee>();
        }
    }
}