using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FunnyBees.Models;
using FunnyBees.Services;
using LibraProgramming.Windows.Commands;
using LibraProgramming.Windows.Infrastructure;
using LibraProgramming.Windows.Interaction;

namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainPageViewModel : ObservableViewModel, ISetupRequired, ICleanupRequired
    {
        private readonly IApplicationOptionsProvider optionsProvider;
        private readonly InteractionRequest<Confirmation> confirmRequest;
        private readonly InteractionRequest<Notification> notificationRequest;
        private readonly ISimulation simulation;
        private ISimulationToken token;

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
        public ICommand RunSimulation
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
        public MainPageViewModel(IApplicationOptionsProvider optionsProvider, ISimulation simulation)
        {
            this.optionsProvider = optionsProvider;
            this.simulation = simulation;

            confirmRequest = new InteractionRequest<Confirmation>();
            notificationRequest = new InteractionRequest<Notification>();
            Confirm = new RelayCommand(DoConfirm);
            RunSimulation = new AsynchronousCommand(RunSimulationAsync, arg => null == token);
            Beehives = new ObservableCollection<BeehiveViewModel>();

            optionsProvider.OptionsChanged += OnOptionsChanged;
        }

        Task ISetupRequired.SetupAsync()
        {
            Beehives.Add(new BeehiveViewModel());
            Beehives.Add(new BeehiveViewModel());
            Beehives.Add(new BeehiveViewModel());

            return Task.CompletedTask;
        }

        Task ICleanupRequired.CleanupAsync()
        {
            if (null != token)
            {
                token.Dispose();
                token = null;
            }

            optionsProvider.OptionsChanged -= OnOptionsChanged;

            return Task.CompletedTask;
        }

/*
        private void ConfigureSession(ISessionBuilder builder)
        {
            for (var index = 0; index < options.NumberOfBeehives; index++)
            {
                builder.CreateBeehive(beehive =>
                {
                    beehive.AddBee(bee => bee.SetBehaviour<QueenBeeBehaviour>());

                    for (var number = 0; number < options.NumberOfWorkingBees; number++)
                    {
                        beehive.AddBee(bee => bee.SetBehaviour<WorkingBeeBehavoiur>());
                    }
                });
            }
        }
*/

        private async Task RunSimulationAsync(object arg)
        {
            token = await simulation.RunAsync();

            simulation.Beehives
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