using MakanyApplication.Shared.Models.Models;
using System.Collections.Generic;

namespace MakanyApplication.Shared.Models.DataTransferObjects.Item
{
    public class CreateItem
    {
        public List<Image> Images { get; set; }
        public Image MainImage { get; set; }
        public string Title { get; set; }
        public string FaceBook { get; set; }
        public string Instagram { get; set; }
        public string LandLine { get; set; }
        public string Description { get; set; }
    }
}
