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
        private readonly DateTime date;
        private IDisposable token;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Session(ITimeIntervalGenerator generator, IList<IEntity> entities, TimeSpan interval)
        {
            this.generator = generator;
            this.entities = entities;

            generator.TimeInterval += OnTimeInterval;

            date = DateTime.UtcNow;
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
            var context = new SessionContext(entities, DateTime.UtcNow - date);

            foreach (var entity in entities.OfType<IUpdatable<ISessionContext>>())
            {
                entity.Update(context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class SessionContext : ISessionContext
        {
            private readonly IList<IEntity> entities;

            public TimeSpan Elapsed
            {
                get;
            }

            public SessionContext(IList<IEntity> entities, TimeSpan elapsed)
            {
                this.entities = entities;
                Elapsed = elapsed;
            }

            public Beehive GetBeehive(int index)
            {
                return entities.OfType<Beehive>().Skip(index).FirstOrDefault();
            }
        }
    }
}