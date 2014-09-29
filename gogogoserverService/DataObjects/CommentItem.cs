using Microsoft.WindowsAzure.Mobile.Service;

namespace gogogoserverService.DataObjects
{
    public class CommentItem : EntityData
    {
        public string Text { get; set; }
        public virtual ParticipantItem ParticipantItem { get; set; }
        public string ParticipantItemId { get; set; }
        public virtual EventItem EventItem { get; set; }
        public string EventItemId { get; set; }
    }

    public class CommentItemDto
    {
        public string Id { get; set; }
        public byte[] Version{ get; set; }
        public string Text { get; set; }
        public string ParticipantImage { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantItemId { get; set; }
        public string EventItemId { get; set; }
    }
}