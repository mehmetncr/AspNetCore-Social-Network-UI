namespace AspNetCore_Social_Network_UI.Models
{
    public class ReplyCommentViewModel
    {
		public int ReplyCommentId { get; set; }
		public DateTime ReplyCommentDate { get; set; }
		public string ReplyCommentContent { get; set; }


		public int ReplyCommentUserDtoId { get; set; }
		public virtual UserViewModel ReplyCommentUserDto { get; set; }

		public int ReplyCommentCommentDtoId { get; set; }
		public virtual CommentViewModel ReplyCommentCommentDto { get; set; }

    }
}
