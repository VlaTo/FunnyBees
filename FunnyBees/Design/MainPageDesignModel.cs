﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FunnyBees.ViewModels;

namespace FunnyBees.Design
{
    /// <summary>
    /// 
    /// </summary>
    public class MainPageDesignModel : ViewModel
    {
        public TimeSpan SessionElapsedTime
        {
            get;
            set;
        }

        public ObservableCollection<BeehiveViewModel> Beehives
        {
            get;
        }

        public bool IsSessionRunning
        {
            get;
            set;
        }

        public ICommand RunSimulation
        {
            get;
        }

        public MainPageDesignModel()
        {
            Beehives = new ObservableCollection<BeehiveViewModel>();
            /*{
                new BeehiveViewModel
                {
                    MaximumNumberOfBees = 100,
                    Number = 88
                },
                new BeehiveViewModel
                {
                    MaximumNumberOfBees = 125,
                    Number = 54
                },
                new BeehiveViewModel
                {
                    MaximumNumberOfBees = 999,
                    Number = 300
                }
            };*/
        }
    }
}