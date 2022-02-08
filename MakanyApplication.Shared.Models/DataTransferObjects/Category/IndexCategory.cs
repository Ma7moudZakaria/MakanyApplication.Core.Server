using MakanyApplication.Shared.Models.Models;

namespace MakanyApplication.Shared.Models.DataTransferObjects.Category
{
    public class IndexCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Image Picture { get; set; }
        public string Description { get; set; }
    }
}
