using System;
using Windows.UI.Xaml;

namespace LibraProgramming.Windows.Interactivity
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangePropertyAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty DurationProperty;
        public static readonly DependencyProperty EaseProperty;
        public static readonly DependencyProperty IncrementProperty;
        public static readonly DependencyProperty PropertyNameProperty;
        public static readonly DependencyProperty ValueProperty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraint"></param>
        public ChangePropertyAction(Type constraint)
            : base(constraint)
        {
        }

        protected override void Invoke(object value)
        {
            throw new NotImplementedException();
        }
    }
}