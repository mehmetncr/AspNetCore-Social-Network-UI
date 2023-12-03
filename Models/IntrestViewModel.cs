namespace AspNetCore_Social_Network_UI.Models
{
    public class IntrestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserViewModelId { get; set; }
        public virtual UserViewModel UserViewModel { get; set; }

    }
}
