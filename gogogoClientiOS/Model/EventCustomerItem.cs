namespace gogogoClientiOS.Model
{
	public class EventCustomerItem : BaseItem
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "customerId")]
		public string CustomerId { get; set; }

		[JsonProperty(PropertyName = "eventID")]
		public string EventId { get; set; } 
	}
}

