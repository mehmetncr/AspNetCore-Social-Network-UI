namespace AspNetCore_Social_Network_UI.Models
{
    public class PrivacySettingsViewModel
    {
        public int PrivacySettingsId { get; set; }
        public int PrivacySettingsUserId { get; set; }
        public bool PrivacySettingsFriendRequest { get; set; }
        public bool PrivacySettingsMessageRequest { get; set; }
        public bool PrivacySettingsHiddenProfile { get; set; }
    }
}
