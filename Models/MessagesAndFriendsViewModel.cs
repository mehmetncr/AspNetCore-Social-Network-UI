namespace AspNetCore_Social_Network_UI.Models
{
    public class MessagesAndFriendsViewModel
    {
        public List<MessageViewModel> Messages { get; set; }
        public List<FriendsViewModel> Friends { get; set; }
        public string UserProfilPic { get; set; }
    }
}
