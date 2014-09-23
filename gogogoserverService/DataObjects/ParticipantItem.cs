using System.Collections.Generic;
using Microsoft.WindowsAzure.Mobile.Service;

namespace gogogoserverService.DataObjects
{
    public class ParticipantItem : EntityData
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<EventItem> EventItems { get; set; }
    }
    public class ParticipantItemDto
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}