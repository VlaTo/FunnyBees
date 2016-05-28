using System;
using System.Collections.Generic;
using System.Linq;
using LibraProgramming.FunnyBees.Interop;
using LibraProgramming.FunnyBees.Services;

namespace LibraProgramming.FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Session : IDisposable
    {
        private readonly ITimeIntervalGenerator generator;
        private readonly IList<IEntity> entities;
        private IDisposable token;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Session(ITimeIntervalGenerator generator, IList<IEntity> entities, TimeSpan interval)
        {
            this.generator = generator;
            this.entities = entities;

            generator.TimeInterval += OnTimeInterval;

            token = generator.Start(interval);
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            if (null == token)
            {
                return;
            }

            token.Dispose();
            token = null;
        }

        private void OnTimeInterval(ITimeIntervalGenerator sender, TimeIntervalEventArgs args)
        {
            foreach (var entity in entities.OfType<IUpdatable>())
            {
                entity.Update();
            }
        }
    }
}