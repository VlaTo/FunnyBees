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
        private readonly InteractionRequest<Confirmation> confirmRequest;
        private readonly InteractionRequest<Notification> notificationRequest;
        private readonly ISimulation simulation;
        private ISimulationSession session;
        private bool isSessionRunning;
        private TimeSpan sessionElapsedTime;
        private readonly Scene scene;

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

        /// <summary>
        /// 
        /// </summary>
        public bool IsSessionRunning
        {
            get
            {
                return isSessionRunning;
            }
            set
            {
                SetProperty(ref isSessionRunning, value);
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
        public ObservableCollection<BeehiveViewModel> Beehives
        {
            get;
        } 

        /// <summary>
        /// 
        /// </summary>
        public MainPageViewModel(IUIThreadAccessor accessor, ISimulation simulation, ISceneBuilder sceneBuilder)
        {
            this.accessor = accessor;
            this.simulation = simulation;

            confirmRequest = new InteractionRequest<Confirmation>();
            notificationRequest = new InteractionRequest<Notification>();
            Confirm = new RelayCommand(DoConfirm);
            RunSimulation = new AsyncRelayCommand(RunSimulationAsync);
            Beehives = new ObservableCollection<BeehiveViewModel>();
            scene = new Scene(sceneBuilder);
        }

        public void DrawScene(CanvasDrawingSession drawingSession)
        {
            scene.Draw(drawingSession);
        }

        public void Update(CanvasTimingInformation info)
        {
            scene.Update(info.TotalTime);
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
            if (false == IsSessionRunning)
            {
                await scene.Initialize();
            }
            else
            {
                
            }

            await accessor.ExecuteAsync(() =>
            {
                IsSessionRunning = false == IsSessionRunning;
            });
        }

/*
        private void OnBeehiveChanged(IBeehive sender, BeehiveChangedEventArgs args)
        {
            var model = Beehives.First(beehive => beehive.Number == sender.Number);

            accessor
                .ExecuteAsync(() =>
                {
                    switch (args.Acion)
                    {
                        case BeehiveAcion.BeeAdded:
                        {
                            model.CurrentBeesCount++;
                            break;
                        }

                        case BeehiveAcion.BeeRemoved:
                        {
                            model.CurrentBeesCount--;
                            break;
                        }

                    }
                })
                .RunAndForget();
        }
*/

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