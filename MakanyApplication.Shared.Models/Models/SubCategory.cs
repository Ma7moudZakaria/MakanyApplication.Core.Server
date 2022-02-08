using System.ComponentModel.DataAnnotations.Schema;

namespace MakanyApplication.Shared.Models.Models
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public Image Picture { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))] public Category Category { get; set; }
    }
}
