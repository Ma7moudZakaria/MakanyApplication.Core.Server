using MakanyApplication.Shared.Models.Models;

namespace MakanyApplication.Shared.Models.DataTransferObjects.SubCategory
{
    public class CreateSubCategory
    {
        public string Name { get; set; }
        public Image Picture { get; set; }
        public int CategoryId { get; set; }
    }
}
