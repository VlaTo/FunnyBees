using System.Collections.ObjectModel;
using FunnyBees.ViewModels;

namespace FunnyBees.Design
{
    /// <summary>
    /// 
    /// </summary>
    public class MainPageDesignModel : ObservableViewModel
    {
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
        public MainPageDesignModel()
        {
            Beehives = new ObservableCollection<BeehiveViewModel>
            {
                new BeehiveViewModel
                {
                    NumberOfBees = 100,
                    CurrentNumber = 88
                },
                new BeehiveViewModel
                {
                    NumberOfBees = 125,
                    CurrentNumber = 54
                },
                new BeehiveViewModel
                {
                    NumberOfBees = 999,
                    CurrentNumber = 300
                }
            };
        }
    }
}