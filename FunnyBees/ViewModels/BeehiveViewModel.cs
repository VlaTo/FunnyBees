namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BeehiveViewModel : ViewModel
    {
        private long number;
        private long maximumNumberOfBees;
        private long currentBeesCount;

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
        public long CurrentBeesCount
        {
            get
            {
                return currentBeesCount;
            }
            set
            {
                SetProperty(ref currentBeesCount, value);
            }
        }

        public BeehiveViewModel()
        {
        }
    }
}