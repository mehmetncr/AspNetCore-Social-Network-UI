namespace AspNetCore_Social_Network_UI.Models
{
    public class HomeViewModel
    {
        public List<PostViewModel> PostDtos { get; set; } //Postlar
        public List<FriendsViewModel> OnlineFriendsDtos { get; set; } //Online kullanıcılar
        public List<UserViewModel> OfferUserDtos { get; set; }
    }
}
