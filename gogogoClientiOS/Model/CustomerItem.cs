namespace gogogoClientiOS.Model
{
	public class CustomerItem : BaseItem
	{
		public string Id { get; set;}

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "image")]
		public string Image { get; set; }

		public static CustomerItem NullCustomer()
		{
			return new CustomerItem {
				Name = "",
				Image = "" //TODO добавить нормальное изображение
			};
		}

		public static CustomerItem LoadingCustomer(string customerId) {
			return new CustomerItem {
				Id = customerId,
				Image = "" //TODO добавить изображение preloader
			};
		}
	}
}

