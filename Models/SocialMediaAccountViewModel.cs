namespace AspNetCore_Social_Network_UI.Models
{
    public class SocialMediaAccountViewModel
    {
        public int Id { get; set; }
        public int UserViewModelId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual UserViewModel UserViewModel { get; set; }

    }
}
