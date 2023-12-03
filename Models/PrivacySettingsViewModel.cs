namespace AspNetCore_Social_Network_UI.Models
{
    public class PrivacySettingsViewModel
    {
        public int Id { get; set; }
        public int UserViewModelId { get; set; }
        public bool FriendRequest { get; set; }
        public bool MessageRequest { get; set; }
        public bool HiddenProfile { get; set; }
        public virtual UserViewModel UserViewModel { get; set; }
    }
}
