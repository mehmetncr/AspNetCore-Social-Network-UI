using Microsoft.Extensions.Hosting;

namespace AspNetCore_Social_Network_UI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public string? CoverPicture { get; set; }
        public string? Biography { get; set; }
        public string? Location { get; set; }

        public int? FollowerCount { get; set; }
        public int? FollowingCount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? JobInfo { get; set; }
        public string? EducationInfo { get; set; }

        public string? Language1 { get; set; }
        public string? Language2 { get; set; }
        public string? Language3 { get; set; }
        public virtual List<SocialMediaAccountViewModel> SocialMediaAccountsViewModel { get; set; }

        public DateTime? LastLogin { get; set; }
        public virtual List<UserActivityViewModel> ActivityHistoryViewModel { get; set; }
        public virtual List<NotificationViewModel> NotificationViewModel { get; set; }

        public int? PrivacySettingsViewModelId { get; set; }
        public virtual PrivacySettingsViewModel PrivacySettingsViewModel { get; set; }

        public virtual List<FriendsViewModel> FriendsViewModel { get; set; }
        public virtual List<IntrestViewModel> InterestsViewModel { get; set; }

        public virtual List<PostViewModel> PostsViewModel { get; set; }
        public virtual List<CommentViewModel> CommentsViewModel { get; set; }
        public virtual List<ReplyCommentViewModel> ReplyCommentsViewModel { get; set; }

    }
}
