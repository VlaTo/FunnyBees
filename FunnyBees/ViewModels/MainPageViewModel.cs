using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using LibraProgramming.FunnyBees.Models;
using LibraProgramming.FunnyBees.Services;
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
        private readonly IBeeApiarOptionsProvider optionsProvider;
        private readonly ISessionController sessionController;
        private readonly InteractionRequest<Confirmation> confirmRequest;
        private BeeApiarOptions options;
        private Session session;

        /// <summary>
        /// 
        /// </summary>
        public IInteractionRequest ConfirmRequest => confirmRequest;

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
        public ICommand StartSession
        {
            get;
        }

        public ObservableCollection<BeehiveViewModel> Beehives
        {
            get;
        } 

        /// <summary>
        /// 
        /// </summary>
        public MainPageViewModel(IBeeApiarOptionsProvider optionsProvider, ISessionController sessionController)
        {
            this.optionsProvider = optionsProvider;
            this.sessionController = sessionController;

            confirmRequest = new InteractionRequest<Confirmation>();
            Confirm = new RelayCommand(DoConfirm);
            StartSession = new RelayCommand(DoStartSession, () => null == session);
            Beehives = new ObservableCollection<BeehiveViewModel>();

            optionsProvider.OptionsChanged += OnOptionsChanged;
        }

        async Task ISetupRequired.SetupAsync()
        {
            options = await optionsProvider.GetOptionsAsync();

            Beehives.Add(new BeehiveViewModel());
            Beehives.Add(new BeehiveViewModel());
            Beehives.Add(new BeehiveViewModel());
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

        private void DoStartSession()
        {
            session = sessionController.Create(options, ConfigureSession);
        }

        private void DoConfirm()
        {
            confirmRequest.Raise(
                new Confirmation("Confirm", "Are you sure?"),
                confirmation =>
                {
                    if (confirmation.Confirmed)
                    {
                    }
                });
        }

        private void OnOptionsChanged(IBeeApiarOptionsProvider provider, OptionsChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}