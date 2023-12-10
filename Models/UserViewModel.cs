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
		public string? UserEmail { get; set; }

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
		public virtual List<SocialMediaAccountViewModel>? SocialMediaAccounts { get; set; }

		public bool UserIsOnline { get; set; }
		public DateTime? UserLastLogin { get; set; }
		public virtual List<UserActivityViewModel>? ActivityHistory { get; set; }
        public virtual List<NotificationViewModel>? Notificationl { get; set; }

		public int? UserPrivacySettingsId { get; set; }
		public virtual PrivacySettingsViewModel UserPrivacy { get; set; }

        public virtual List<FriendsViewModel>? Friends{ get; set; }
        public virtual List<IntrestViewModel>? Interests { get; set; }

        public virtual List<PostViewModel>? Posts { get; set; }
        public virtual List<CommentViewModel>? Comments { get; set; }
        public virtual List<ReplyCommentViewModel>? ReplyComments { get; set; }

    }
}
