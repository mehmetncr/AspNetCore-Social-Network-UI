namespace AspNetCore_Social_Network_UI.Models
{
    public class HomeViewModel
    {
        public List<PostViewModel> PostViewModel { get; set; } //Postlar
        public List<FriendsViewModel> OnlineFriendsViewModel { get; set; } //Online kullanıcılar
        public List<UserViewModel> OfferUserViewModel { get; set; }
    }
}
