namespace MakanyApplication.Shared.Models.DataTransferObjects.Address
{
    public class IndexAddress
    {
        public int Id { get; set; }
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
    }
}
