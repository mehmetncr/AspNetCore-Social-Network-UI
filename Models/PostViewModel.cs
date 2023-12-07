namespace AspNetCore_Social_Network_UI.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string TextContent { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public int CommentNumber { get; set; }
        public int LikeNumber { get; set; }
        public int DislikeNumber { get; set; }
        public string PostLink { get; set; }
        public string PostType { get; set; }
        public virtual List<CommentViewModel> CommentsViewModel { get; set; }
        public virtual UserViewModel UserViewModel { get; set; }
    }
}
