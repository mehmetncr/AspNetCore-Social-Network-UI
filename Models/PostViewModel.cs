namespace AspNetCore_Social_Network_UI.Models
{
    public class PostViewModel
    {
		public int PostId { get; set; }
		public int PostUserId { get; set; }
		public DateTime PostCreateDate { get; set; }
		public string PostTextContent { get; set; }
		public string PostImageUrl { get; set; }
		public string PostVideoUrl { get; set; }
		public string PostYoutubeUrl { get; set; }
		public int PostCommentNumber { get; set; }
		public int PostLikeNumber { get; set; }
		public int PostDislikeNumber { get; set; }
		public string PostLink { get; set; }
		public string PostType { get; set; }
		public virtual List<CommentViewModel> Comments { get; set; }
        public virtual UserViewModel PostUser { get; set; }
    }
}
