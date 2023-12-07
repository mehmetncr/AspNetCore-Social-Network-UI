using Microsoft.Extensions.Hosting;

namespace AspNetCore_Social_Network_UI.Models
{
    public class UserViewModel
    {
		public int UserId { get; set; }
		public string UserFirstName { get; set; }
		public string UserLastName { get; set; }
		public DateTime? UserBirthDate { get; set; }
		public string? UserGender { get; set; }
		public string? UserProfilePicture { get; set; }
		public string? UserCoverPicture { get; set; }
		public string? UserBiography { get; set; }
		public string? UserLocation { get; set; }

		public int? UserFollowerCount { get; set; }
		public int? UserFollowingCount { get; set; }
		public DateTime? UserCreatedAt { get; set; }
		public string? UserPhoneNumber { get; set; }
		public string? UserWebsite { get; set; }
		public string? UserJobInfo { get; set; }
		public string? UserEducationInfo { get; set; }

		public string? UserLanguage1 { get; set; }
		public string? UserLanguage2 { get; set; }
		public string? UserLanguage3 { get; set; }
		public virtual List<SocialMediaAccountViewModel> SocialMediaAccountsViewModel { get; set; }

		public bool UserIsOnline { get; set; }
		public DateTime? UserLastLogin { get; set; }
		public virtual List<UserActivityViewModel> ActivityHistoryViewModel { get; set; }
        public virtual List<NotificationViewModel> NotificationViewModel { get; set; }

		public int? UserPrivacySettingsId { get; set; }
		public virtual PrivacySettingsViewModel UserPrivacySettings { get; set; }

        public virtual List<FriendsViewModel> FriendsViewModel { get; set; }
        public virtual List<IntrestViewModel> InterestsViewModel { get; set; }

        public virtual List<PostViewModel> PostsViewModel { get; set; }
        public virtual List<CommentViewModel> CommentsViewModel { get; set; }
        public virtual List<ReplyCommentViewModel> ReplyCommentsViewModel { get; set; }

    }
}
