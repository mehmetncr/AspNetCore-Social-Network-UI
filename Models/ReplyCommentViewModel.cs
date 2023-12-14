namespace AspNetCore_Social_Network_UI.Models
{
    public class ReplyCommentViewModel
    {
		public int ReplyCommentId { get; set; }
		public DateTime ReplyCommentDate { get; set; }
		public string ReplyCommentContent { get; set; }

        public int PostId { get; set; }
        public int ReplyCommentUserId { get; set; }
		public virtual UserViewModel ReplyCommentUser { get; set; }

		public int ReplyCommentCommentId { get; set; }
		public virtual CommentViewModel ReplyCommentComment { get; set; }

    }
}
