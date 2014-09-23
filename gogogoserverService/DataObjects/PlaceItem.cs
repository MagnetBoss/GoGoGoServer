using Microsoft.WindowsAzure.Mobile.Service;

namespace gogogoserverService.DataObjects
{
    public class PlaceItem : EntityData
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual EventItem EventItem { get; set; }
    }

    public class PlaceItemDto
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }     
    }
}