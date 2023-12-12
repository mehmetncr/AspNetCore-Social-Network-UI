namespace AspNetCore_Social_Network_UI.Models
{
	public class MessageViewModel
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OwnerUserId { get; set; }
        public UserViewModel User { get; set; }
        public List<MessageDetailViewModel> MessageDetails { get; set; }
    }
}
