namespace MakanyApplication.Shared.Models.DataTransferObjects.PhoneNumber
{
    public class UpdatePhoneNumber
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
    }
}
