namespace AspNetCore_Social_Network_UI.Models
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public int NotificationSenderUserId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDescription { get; set; }
        public int NotificationOwnerUserId { get; set; }
        public bool NotificationIsSeen { get; set; }
        public virtual UserViewModel NotificationSenderUser { get; set; }
    }
}
