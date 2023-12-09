namespace AspNetCore_Social_Network_UI.Models
{
    public class FriendRequestViewModel
    {
        public int FriendReqid { get; set; }
        public DateTime FriendReqCreatedDate { get; set; }
        public bool FriendReqStatus { get; set; }
        public int FriendReqUserId { get; set; }
        public int FriendReqFriendReqSenderid { get; set; }
        public UserViewModel FriendReqFriendReqSender { get; set; }
    }
}
