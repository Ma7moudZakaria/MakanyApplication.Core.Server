using System.Collections.Generic;

namespace MakanyApplication.Shared.Models.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }        
        public string Password { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
