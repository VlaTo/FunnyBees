﻿namespace LibraProgramming.Windows.Interaction
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfirmation : INotification
    {
        /// <summary>
        /// 
        /// </summary>
        bool Confirmed
        {
            get;
            set;
        }
    }

    public class Confirmation : Notification, IConfirmation
    {
        public bool Confirmed
        {
            get;
            set;
        }

        public Confirmation(string title, string content, bool confirmed = false)
            : base(title, content)
        {
            Confirmed = confirmed;
        }
    }
}