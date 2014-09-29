using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Mobile.Service;

namespace gogogoserverService.DataObjects
{
    public class EventItem : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SmallDescription { get; set; }
        public virtual DateTime Date { get; set; }
        public string ShortImage { get; set; }
        public string LargeImage { get; set; }
        public int MaxParticipantsCount { get; set; }
        public int MinParticipantsCount { get; set; }
        public virtual PlaceItem PlaceItem { get; set; }
        public virtual ICollection<ParticipantItem> ParticipantItems { get; set; }
    }

    public class EventItemDto : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SmallDescription { get; set; }
        public virtual DateTime Date { get; set; }
        public string ShortImage { get; set; }
        public string LargeImage { get; set; }
        public int MaxParticipantsCount { get; set; }
        public int MinParticipantsCount { get; set; }
        public double PlaceLatitude { get; set; }
        public double PlaceLongitude { get; set; }
        public string PlaceTitle { get; set; }
        public string PlaceDescription { get; set; }
    }

}