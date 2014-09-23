using System;

namespace gogogoClientiOS.Model
{
	public class CommentItem : BaseItem
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "eventID")]
		public string EventId { get; set; }

		[JsonProperty(PropertyName = "customerId")]
		public string CustomerId { get; set; }

		[JsonProperty(PropertyName = "date")]
		public DateTime Date { get; set; }

		public static CommentItem NullComment()
		{
			return new CommentItem () {
				Text = "",
				CustomerId = ""
			};
		}
	}


}

