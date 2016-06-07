using System;

namespace FunnyBees.Models
{
    public class UpdateContext
    {
        public TimeSpan Elapsed
        {
            get;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public UpdateContext(TimeSpan elapsed)
        {
            Elapsed = elapsed;
        }
    }
}