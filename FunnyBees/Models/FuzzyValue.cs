namespace FunnyBees.Models
{
    public struct FuzzyValue<TValue>
    {
        public TValue Minimum
        {
            get;
        }

        public TValue Maximum
        {
            get;
        }

        public TValue Value
        {
            get;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public FuzzyValue(TValue minimum, TValue maximum, TValue value)
        {
            Minimum = minimum;
            Maximum = maximum;
            Value = value;
        }
    }
}