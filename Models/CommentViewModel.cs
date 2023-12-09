using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore_Social_Network_UI.Models
{
    public class CommentViewModel
    {
		public int CommentId { get; set; }
		public DateTime CommentDate { get; set; }
		public string CommentContent { get; set; }

		[ForeignKey("UserViewModelId")]
		public int CommentUserId { get; set; }
		public virtual UserViewModel CommentUser { get; set; }
        [ForeignKey("PostViewModelId")]
		public int CommentPostId { get; set; }
		public virtual PostViewModel CommentPost { get; set; }
		public virtual List<ReplyCommentViewModel> ReplyComments { get; set; }

	}
}
