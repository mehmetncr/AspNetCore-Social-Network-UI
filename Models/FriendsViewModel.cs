namespace AspNetCore_Social_Network_UI.Models
{
    public class FriendsViewModel
    {
        public int Id { get; set; }
        public int UserViewModelId { get; set; }
        public int FriendsUserId { get; set; }

        public virtual UserViewModel UserViewModel { get; set; }
    }
}
