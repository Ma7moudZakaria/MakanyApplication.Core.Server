using MakanyApplication.Shared.Models.Models;

namespace MakanyApplication.Shared.Models.DataTransferObjects.SubCategory
{
    public class IndexSubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Image Picture { get; set; }
        public int CategoryId { get; set; }
    }
}
