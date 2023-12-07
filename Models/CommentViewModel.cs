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
		public int CommentUserDtoId { get; set; }
		public virtual UserViewModel CommentUserDto { get; set; }
        [ForeignKey("PostViewModelId")]
		public int CommentPostDtoId { get; set; }
		public virtual PostViewModel CommentPostDto { get; set; }
		public virtual List<ReplyCommentViewModel> ReplyCommentsDto { get; set; }

	}
}
