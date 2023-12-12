namespace AspNetCore_Social_Network_UI.Models
{
    public class MessageDetailViewModel
    {
        public int Id { get; set; }
        public int OwnerUserId { get; set; }
        public DateTime SendDate { get; set; }
        public string MessageContent { get; set; }
        public int MessageId { get; set; }
    }
}
