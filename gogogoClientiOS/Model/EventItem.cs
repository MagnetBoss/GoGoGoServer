using System;

namespace gogogoClientiOS.Model
{
	public class EventItem : BaseItem
	{
		public string Id { get; set; }

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

		[JsonProperty(PropertyName = "curCustomers")]
		public int CurCustomers { get;set; }

		[JsonProperty(PropertyName = "status")]
		public int Status { get; set; }

		[JsonProperty(PropertyName = "image")]
		public string Image { get; set; }

		[JsonProperty(PropertyName = "descriptionImage")]
		public string DescriptionImage { get; set; }

		public static EventItem NullEvent()
		{
			return new EventItem () {
				Name = "",
				Description = "",
				SmallDescription = "",
				Date = DateTime.Now,
				MaxCustomers = 0,
				MinCustomers = 0,
				CurCustomers = 0,
				Status = 0,
				Image = "",
				DescriptionImage = ""
			};
		}
	}
}

