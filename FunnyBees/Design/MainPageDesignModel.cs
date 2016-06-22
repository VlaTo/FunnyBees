using System;
using System.Collections.ObjectModel;
using FunnyBees.ViewModels;

namespace FunnyBees.Design
{
    /// <summary>
    /// 
    /// </summary>
    public class MainPageDesignModel : ObservableViewModel
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