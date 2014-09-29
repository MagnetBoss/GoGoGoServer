using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace gogogoClientiOS.Model
{
	public class EventItem : BaseItem
	{
		public string Id { get; set; }
        
        [Version]
        public string Version { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "smallDescription")]
		public string SmallDescription { get; set; }

		[JsonProperty(PropertyName = "date")]
		public DateTime Date { get; set; }

		[JsonProperty(PropertyName = "maxCustomers")]
		public int MaxCustomers { get; set; }

		[JsonProperty(PropertyName = "minCustomers")]
		public int MinCustomers { get; set; }

		[JsonProperty(PropertyName = "shortImage")]
        public string ShortImage { get; set; }

		[JsonProperty(PropertyName = "largeImage")]
		public string LargeImage { get; set; }

        [JsonProperty(PropertyName = "participantItems")]
        public List<ParticipantItem> ParticipantItems { get; set; } 

		public static EventItem NullEvent()
		{
			return new EventItem
			{
				Name = "",
				Description = "",
				SmallDescription = "",
				Date = DateTime.Now,
				MaxCustomers = 0,
				MinCustomers = 0,
				ShortImage = null,
				LargeImage = null,
                ParticipantItems = new List<ParticipantItem>()
			};
		}
	}
}

