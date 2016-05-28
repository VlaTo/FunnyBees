namespace LibraProgramming.Windows.Interaction
{
    public interface INotification
    {
        
    }

    public class Notification : InteractionRequestContext, INotification
    {
        public string Title
        {
            get;
        }

        public string Content
        {
            get;
        }

        public Notification(string title, string content)
        {
            Title = title;
            Content = content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public static InteractionRequestContext Confirmation(string title, string content, bool confirmed = false)
        {
            return new Confirmation(title, content, confirmed);
        }
    }
}