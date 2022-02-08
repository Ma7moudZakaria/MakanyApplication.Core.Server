namespace MakanyApplication.Shared.Models.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Image Picture { get; set; }
        public string Description { get; set; }
    }
}
