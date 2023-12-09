namespace AspNetCore_Social_Network_UI.Models
{
    public class ProfileViewModel
    {
        public UserViewModel User { get; set; }
        public List<FriendsViewModel> Friends { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
