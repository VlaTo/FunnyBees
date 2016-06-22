using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FunnyBees.Core;
using FunnyBees.Models;
using FunnyBees.Services;
using LibraProgramming.Windows.Commands;
using LibraProgramming.Windows.Dependency.Tracking;
using LibraProgramming.Windows.Infrastructure;
using LibraProgramming.Windows.Interaction;

namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainPageViewModel : ObservableViewModel, ICleanupRequired
    {
        private readonly IApplicationOptionsProvider optionsProvider;
        private readonly IDispatcherProvider dp;
        private readonly InteractionRequest<Confirmation> confirmRequest;
        private readonly InteractionRequest<Notification> notificationRequest;
        private readonly ISimulation simulation;
        private ISimulationSession session;
        private bool isSessionRunning;

        /// <summary>
        /// 
        /// </summary>
        public IInteractionRequest ConfirmRequest => confirmRequest;

        /// <summary>
        /// 
        /// </summary>
        public IInteractionRequest NotificationRequest => notificationRequest;

        /// <summary>
        /// 
        /// </summary>
        public ICommand Confirm
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAsyncCommand RunSimulation
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<BeehiveViewModel> Beehives
        {
            get;
        } 

        /// <summary>
        /// 
        /// </summary>
        public MainPageViewModel(IApplicationOptionsProvider optionsProvider, IDispatcherProvider dp, ISimulation simulation)
        {
            this.optionsProvider = optionsProvider;
            this.dp = dp;
            this.simulation = simulation;

            confirmRequest = new InteractionRequest<Confirmation>();
            notificationRequest = new InteractionRequest<Notification>();
            Confirm = new RelayCommand(DoConfirm);
            RunSimulation = new AsyncRelayCommand(RunSimulationAsync);
            Beehives = new ObservableCollection<BeehiveViewModel>();

            optionsProvider.OptionsChanged += OnOptionsChanged;
        }

        Task ICleanupRequired.CleanupAsync()
        {
            if (null != session)
            {
                session.Dispose();
                session = null;
            }

            optionsProvider.OptionsChanged -= OnOptionsChanged;

            return Task.CompletedTask;
        }

        private async Task RunSimulationAsync(object notused)
        {
            if (null != session)
            {
                session.Updated -= OnSessionUpdated;

                session.Dispose();
                session = null;

                return;
            }

            session = await simulation.RunAsync().ConfigureAwait(false);

            await dp.Dispatcher.ExecuteAsync(() =>
            {
                session.Updated += OnSessionUpdated;

                Beehives.Clear();

                foreach (var beehive in session.Beehives)
                {
                    Beehives.Add(new BeehiveViewModel
                    {
                        Number = beehive.Number,
                        MaximumNumberOfBees = beehive.Bees.Count
                    });
                }
            });
        }

        private void OnSessionUpdated(object sender, SessionUpdatedEventArgs e)
        {
            
        }

        private void DoConfirm()
        {
            confirmRequest.Raise(new Confirmation("Confirm", "Are you sure?"), DoConfirmCallback);
        }

        private void OnOptionsChanged(IApplicationOptionsProvider provider, OptionsChangedEventArgs e)
        {
            notificationRequest.Raise(new Notification("Title", "Content"), null);
        }

        private void DoConfirmCallback(Confirmation confirmation)
        {
            if (confirmation.Confirmed)
            {

            }
        }
    }
}