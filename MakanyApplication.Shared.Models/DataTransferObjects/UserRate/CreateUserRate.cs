namespace MakanyApplication.Shared.Models.DataTransferObjects.UserRate
{
    public class CreateUserRate
    {
        public string Comment { get; set; }
        public float Value { get; set; }
        public int ItemId { get; set; }
    }
}
