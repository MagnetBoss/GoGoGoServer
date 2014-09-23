using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Mobile.Service;

namespace gogogoserverService.DataObjects
{
    public class EventItem : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ParticipantItem> ParticipantItems { get; set; }
        public virtual ICollection<CommentItem> CommentItems { get; set; }
        public virtual PlaceItem PlaceItem { get; set; }
        public virtual DateTime Date { get; set; }
        public byte[] ShortImage { get; set; }
        public byte[] LargeImage { get; set; }
        public int MaxParticipantsCount { get; set; }
        public int MinParticipantsCount { get; set; }
    }

    public class EventItemDto
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ParticipantItemDto> Participants { get; set; }
        public PlaceItemDto Place { get; set; }
        public virtual DateTime Date { get; set; }
        public byte[] ShortImage { get; set; }
        public byte[] LargeImage { get; set; }
        public int MaxParticipantsCount { get; set; }
        public int MinParticipantsCount { get; set; }
    }

}