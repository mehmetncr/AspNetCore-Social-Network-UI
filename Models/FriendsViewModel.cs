namespace AspNetCore_Social_Network_UI.Models
{
    public class FriendsViewModel
    {
		public int FriendsId { get; set; }
		public int FriendsUserId { get; set; }
        public string FriendsStatus { get; set; }
        public int FriendId { get; set; }

		public virtual UserViewModel Friend { get; set; }
    }
}
