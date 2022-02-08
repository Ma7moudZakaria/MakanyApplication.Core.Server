using System.ComponentModel.DataAnnotations.Schema;

namespace MakanyApplication.Shared.Models.Models
{
    public class UserRate : BaseEntity
    {
        public string Comment { get; set; }
        public float Value { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(ItemId))] public Item Item { get; set; }
        [ForeignKey(nameof(UserId))] public User User { get; set; }
    }
}
