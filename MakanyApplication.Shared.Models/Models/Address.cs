using System.ComponentModel.DataAnnotations.Schema;

namespace MakanyApplication.Shared.Models.Models
{
    public class Address : BaseEntity
    {        
        public string BuildingName { get; set; }
        public int BuildingNumber { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int GovernmentId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }        

        [ForeignKey(nameof(UserId))] public User User { get; set; }
        [ForeignKey(nameof(ItemId))] public Item Item { get; set; }
        [ForeignKey(nameof(GovernmentId))] public Government Government { get; set; }
        [ForeignKey(nameof(CountryId))] public Country Country { get; set; }
        [ForeignKey(nameof(CityId))] public City City { get; set; }
        [ForeignKey(nameof(AreaId))] public Area Area { get; set; }
    }
}
