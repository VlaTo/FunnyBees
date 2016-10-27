using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using FunnyBees.Engine;
using FunnyBees.Models;
using FunnyBees.Services;
using LibraProgramming.Windows.Commands;
using LibraProgramming.Windows.Infrastructure;
using LibraProgramming.Windows.Interaction;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;

namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainPageViewModel : ViewModel, ICleanupRequired
    {
        private readonly IUIThreadAccessor accessor;
        private readonly ISceneCreator sceneCreator;
        private readonly InteractionRequest<Confirmation> confirmRequest;
        private readonly InteractionRequest<Notification> notificationRequest;
        private readonly ISimulation simulation;
        private ISimulationSession session;
        private bool _isSimulationRuning;
        private TimeSpan sessionElapsedTime;

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
        public TimeSpan SessionElapsedTime
        {
            get
            {
                return sessionElapsedTime;
            }
            set
            {
                SetProperty(ref sessionElapsedTime, value);
            }
        }

        public Scene Scene
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSimulationRuning
        {
            get
            {
                return _isSimulationRuning;
            }
            set
            {
                SetProperty(ref _isSimulationRuning, value);
            }
        }

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
        /*public ObservableCollection<BeehiveViewModel> Beehives
        {
            get;
        } */

        /// <summary>
        /// 
        /// </summary>
        public MainPageViewModel(IUIThreadAccessor accessor, ISimulation simulation, ISceneCreator sceneCreator)
        {
            this.accessor = accessor;
            this.simulation = simulation;
            this.sceneCreator = sceneCreator;

            confirmRequest = new InteractionRequest<Confirmation>();
            notificationRequest = new InteractionRequest<Notification>();
            Confirm = new RelayCommand(DoConfirm);

            var command = new AsyncRelayCommand(RunSimulationAsync);

            command.Complete += OnRunSimulationComplete;

            IsSimulationRuning = false;
            RunSimulation = command;
        }

        public void DrawScene(CanvasDrawingSession drawingSession)
        {
        }

        public void Update(CanvasTimingInformation info)
        {
            if (null != Scene)
            {
                Scene.Update(info.TotalTime);
            }
        }

        Task ICleanupRequired.CleanupAsync()
        {
            /*if (null != session)
            {
                session.Dispose();
                session = null;
            }

            optionsProvider.OptionsChanged -= OnOptionsChanged;*/

            return Task.CompletedTask;
        }

        private async Task RunSimulationAsync(object notused)
        {
            await accessor.ExecuteAsync(() =>
            {
                IsSimulationRuning = true;
            });
            await Task.Delay(TimeSpan.FromSeconds(10.0d));
        }

        private async void OnRunSimulationComplete(IAsyncCommand sender, CommandCompleteEventArgs args)
        {
            await accessor.ExecuteAsync(() =>
            {
                IsSimulationRuning = false;
            });
        }


        /*private void OnSessionUpdated(object sender, SessionUpdatedEventArgs e)
        {
            accessor.ExecuteAsync(() => SessionElapsedTime = e.Elapsed).RunAndForget();
        }*/

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