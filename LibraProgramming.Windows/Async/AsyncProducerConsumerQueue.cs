using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraProgramming.Windows.Infrastructure;

namespace LibraProgramming.Windows.Async
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AsyncProducerConsumerQueue<T> : IDisposable
    {
        internal static readonly DequeueResult FalseResult;

        private readonly Queue<T> queue;
        private readonly int maxCount;
        private readonly AsyncLock mutex;
        private readonly AsyncConditionVariable notFull;
        private readonly AsyncConditionVariable completedOrNotEmpty;
        private readonly CancellationTokenSource completed;

        internal bool IsEmpty => 0 == queue.Count;

        internal bool IsFull => maxCount == queue.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="maxCount"></param>
        public AsyncProducerConsumerQueue(IEnumerable<T> collection, int maxCount)
        {
            if (0 >= maxCount)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCount));
            }

            queue = null == collection ? new Queue<T>() : new Queue<T>(collection);

            if (queue.Count > maxCount)
            {
                throw new ArgumentException();
            }

            this.maxCount = maxCount;
            mutex = new AsyncLock();
            notFull = new AsyncConditionVariable(mutex);
            completedOrNotEmpty = new AsyncConditionVariable(mutex);
            completed = new CancellationTokenSource();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public AsyncProducerConsumerQueue(IEnumerable<T> collection)
            : this(collection, int.MaxValue)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxCount"></param>
        public AsyncProducerConsumerQueue(int maxCount)
            : this(null, maxCount)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public AsyncProducerConsumerQueue()
            : this(null, int.MaxValue)
        {
        } 

        static AsyncProducerConsumerQueue()
        {
            FalseResult = new DequeueResult(null, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            completed.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AppendDone()
        {
            using (mutex.Lock())
            {
                if (completed.IsCancellationRequested)
                {
                    return;
                }

                completed.Cancel();
                completedOrNotEmpty.NotifyAll();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> TryEnqueueAsync(T item, CancellationToken ct)
        {
            var result = await TryEnqueueAsync(item, ct, null).ConfigureAwait(false);

            if (null != result)
            {
                return true;
            }

            ct.ThrowIfCancellationRequested();

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<bool> TryEnqueueAsync(T item)
        {
            return TryEnqueueAsync(item, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public bool TryEnqueue(T item, CancellationToken ct)
        {
            var result = TryEnqueueInternal(item, ct);

            if (null != result)
            {
                return true;
            }

            ct.ThrowIfCancellationRequested();

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryEnqueue(T item)
        {
            return TryEnqueue(item, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task EnqueueAsync(T item, CancellationToken ct)
        {
            var result = await TryEnqueueAsync(item, ct).ConfigureAwait(false);

            if (false == result)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task EnqueueAsync(T item)
        {
            return EnqueueAsync(item, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ct"></param>
        public void Enqueue(T item, CancellationToken ct)
        {
            var result = TryEnqueue(item, ct);

            if (false == result)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            Enqueue(item, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> HasItemsAsync(CancellationToken ct)
        {
            using (await mutex.LockAsync().ConfigureAwait(false))
            {
                while (false == completed.IsCancellationRequested && IsEmpty)
                {
                    await completedOrNotEmpty.WaitAsync(ct).ConfigureAwait(false);
                }

                return false == IsEmpty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<bool> HasItemsAsync()
        {
            return HasItemsAsync(CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public IEnumerable<T> EnumerateConsuming(CancellationToken ct)
        {
            while (true)
            {
                var result = TryDequeueInternal(ct);

                if (false == result.Success)
                {
                    yield break;
                }

                yield return result.Item;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> EnumerateConsuming()
        {
            return EnumerateConsuming(CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<DequeueResult> TryDequeueAsync(CancellationToken ct)
        {
            var result = await TryDequeueAsync(ct, null).ConfigureAwait(false);

            if (false == result.Success)
            {
                ct.ThrowIfCancellationRequested();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<DequeueResult> TryDequeueAsync()
        {
            return TryDequeueAsync(CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public DequeueResult TryDequeue(CancellationToken ct)
        {
            var result = TryDequeueInternal(ct);

            if (false == result.Success)
            {
                ct.ThrowIfCancellationRequested();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DequeueResult TryDequeue()
        {
            return TryDequeue(CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<T> DequeueAsync(CancellationToken ct)
        {
            var result = await TryDequeueAsync(ct).ConfigureAwait(false);

            if (false == result.Success)
            {
                ct.ThrowIfCancellationRequested();
            }

            return result.Item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<T> DequeueAsync()
        {
            return DequeueAsync(CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public T Dequeue(CancellationToken ct)
        {
            var result = TryDequeue(ct);

            if (false == result.Success)
            {
                throw new InvalidOperationException();
            }

            return result.Item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            return Dequeue(CancellationToken.None);
        }

        internal async Task<AsyncProducerConsumerQueue<T>> TryEnqueueAsync(T item, CancellationToken ct, TaskCompletionSource abort)
        {
            try
            {
                using (var source = CancellationTokenHelper.Aggregate(completed.Token, ct))
                {
                    using (await mutex.LockAsync().ConfigureAwait(false))
                    {
                        while (IsFull)
                        {
                            await notFull.WaitAsync(source.Token).ConfigureAwait(false);
                        }

                        if (completed.IsCancellationRequested)
                        {
                            return null;
                        }

                        if (null != abort && false == abort.TrySetCanceled())
                        {
                            return null;
                        }

                        queue.Enqueue(item);
                        completedOrNotEmpty.Notify();

                        return this;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        internal async Task<DequeueResult> TryDequeueAsync(CancellationToken ct, TaskCompletionSource abort)
        {
            try
            {
                using (await mutex.LockAsync().ConfigureAwait(false))
                {
                    while (false == completed.IsCancellationRequested && IsEmpty)
                    {
                        await completedOrNotEmpty.WaitAsync(ct).ConfigureAwait(false);
                    }

                    if (completed.IsCancellationRequested && IsEmpty)
                    {
                        return FalseResult;
                    }

                    if (null != abort && abort.TrySetCanceled())
                    {
                        return FalseResult;
                    }

                    var item = queue.Dequeue();

                    notFull.Notify();

                    return new DequeueResult(this, item);
                }
            }
            catch (OperationCanceledException)
            {
                return FalseResult;
            }
        }

        internal DequeueResult TryDequeueInternal(CancellationToken ct)
        {
            try
            {
                using (mutex.Lock())
                {
                    while (false == completed.IsCancellationRequested && IsEmpty)
                    {
                        completedOrNotEmpty.Wait(ct);
                    }

                    if (completed.IsCancellationRequested && IsEmpty)
                    {
                        return FalseResult;
                    }

                    var item = queue.Dequeue();

                    notFull.Notify();

                    return new DequeueResult(this, item);
                }
            }
            catch (OperationCanceledException)
            {
                return FalseResult;
            }
        }

        internal AsyncProducerConsumerQueue<T> TryEnqueueInternal(T item, CancellationToken ct)
        {
            try
            {
                using (var source = CancellationTokenHelper.Aggregate(completed.Token, ct))
                {
                    using (mutex.Lock())
                    {
                        while (IsFull)
                        {
                            notFull.Wait(source.Token);
                        }

                        if (completed.IsCancellationRequested)
                        {
                            return null;
                        }

                        queue.Enqueue(item);
                        completedOrNotEmpty.Notify();

                        return this;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public sealed class DequeueResult
        {
            /// <summary>
            /// 
            /// </summary>
            public AsyncProducerConsumerQueue<T> Queue
            {
                get;
            }

            /// <summary>
            /// 
            /// </summary>
            public T Item
            {
                get;
            }

            /// <summary>
            /// 
            /// </summary>
            public bool Success => null != Queue;

            internal DequeueResult(AsyncProducerConsumerQueue<T> queue, T item)
            {
                Queue = queue;
                Item = item;
            }
        }
    }
}