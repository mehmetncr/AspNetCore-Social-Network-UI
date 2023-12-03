namespace AspNetCore_Social_Network_UI.Models
{
    public class ReplyCommentViewModel
    {
        public int Id { get; set; }
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }


        public int UserViewModelId { get; set; }
        public virtual UserViewModel UserViewModel { get; set; }

        public int CommentViewModelId { get; set; }
        public virtual CommentViewModel CommentViewModel { get; set; }

    }
}
