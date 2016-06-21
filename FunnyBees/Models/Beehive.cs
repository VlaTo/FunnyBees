﻿using System.Collections.Generic;

namespace FunnyBees.Models
{
    public class Beehive : IBeehive
    {
        public IList<IBee> Bees
        {
            get;
        }

        public int Number
        {
            get;
        }

        public int MaximumNumberOfBees
        {
            get;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Beehive(int number, int maximumNumberOfBees)
        {
            Number = number;
            MaximumNumberOfBees = maximumNumberOfBees;
            Bees = new List<IBee>();
        }

        public void Update(UpdateContext context)
        {
            for (var index = 0; index < Bees.Count; index++)
            {
                Bees[index].Update(context);
            }
        }
    }
}