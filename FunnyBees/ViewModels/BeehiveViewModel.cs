namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BeehiveViewModel : ObservableViewModel
    {
        private long currentNumber;
        private long numberOfBees;

        /// <summary>
        /// 
        /// </summary>
        public long CurrentNumber
        {
            get
            {
                return currentNumber;
            }
            set
            {
                SetProperty(ref currentNumber, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long NumberOfBees
        {
            get
            {
                return numberOfBees;
            }
            set
            {
                SetProperty(ref numberOfBees, value);
            }
        }
    }
}