namespace AspNetCore_Social_Network_UI.Models
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public int UserViewModelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual UserViewModel UserViewModel { get; set; }
    }
}
