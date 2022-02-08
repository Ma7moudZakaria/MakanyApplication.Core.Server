namespace MakanyApplication.Shared.Models.DataTransferObjects.UserRate
{
    public class UpdateUserRate
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public float Value { get; set; }
        public int ItemId { get; set; }
    }
}
