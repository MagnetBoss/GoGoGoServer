using Newtonsoft.Json;

namespace gogogoClientiOS.Model
{
	public class ParticipantItem : BaseItem
	{
		public string Id { get; set;}

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "image")]
		public string Image { get; set; }

		public static ParticipantItem NullCustomer()
		{
			return new ParticipantItem {
				Name = "",
				Image = "" //TODO добавить нормальное изображение
			};
		}

		public static ParticipantItem LoadingCustomer(string customerId) {
			return new ParticipantItem {
				Id = customerId,
				Image = "" //TODO добавить изображение preloader
			};
		}
	}
}

