using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore_Social_Network_UI.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }

        [ForeignKey("UserViewModelId")]
        public int UserViewModelId { get; set; }
        public virtual UserViewModel UserViewModel { get; set; }
        [ForeignKey("PostViewModelId")]
        public int PostViewModelId { get; set; }
        public virtual PostViewModel PostViewModel { get; set; }

    }
}
