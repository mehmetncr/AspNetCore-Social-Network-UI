namespace AspNetCore_Social_Network_UI.Models
{
    public class ProfileFriendsViewModel
    {
        public List<FriendsViewModel> friends { get; set; }
        public List<FriendRequestViewModel> friendRequests { get; set; }
    }
}
