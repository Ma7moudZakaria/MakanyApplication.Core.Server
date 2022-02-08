using System.ComponentModel.DataAnnotations.Schema;

namespace MakanyApplication.Shared.Models.Models
{
    public class PhoneNumber : BaseEntity
    {
        public string Phone { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))] public User User { get; set; }
        [ForeignKey(nameof(ItemId))] public Item Item { get; set; }
    }
}
