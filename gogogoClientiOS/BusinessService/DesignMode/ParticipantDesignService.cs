using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using gogogoClientiOS.Model;
using gogogoClientiOS.Model.Messages;

namespace gogogoClientiOS.BusinessService.DesignMode
{
	public class ParticipantDesignService : ParticipantService 
	{
		private List<CustomerItem> _items = new List<CustomerItem> {
			new CustomerItem {
				Id = "Customer1",
				Name = "Никита Ильин",
				Image = "/9j/4AAQSkZJRgABAQIAHAAcAAD/2wBDAAQDAwQDAwQEAwQFBAQFBgoHBgYGBg0JCggKDw0QEA8NDw4RExgUERIXEg4PFRwVFxkZGxsbEBQdHx0aHxgaGxr/2wBDAQQFBQYFBgwHBwwaEQ8RGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhoaGhr/wAARCAAwADADASIAAhEBAxEB/8QAGgAAAgMBAQAAAAAAAAAAAAAABwgFBgkEA//EAC4QAAEDAwMDAwMEAwEAAAAAAAECAwQFBhEAByESEzEIQWEUIlEjMnGBFSaRUv/EABkBAAMAAwAAAAAAAAAAAAAAAAMEBQABAv/EAB4RAAICAgMBAQAAAAAAAAAAAAECAAMEERIhMRNB/9oADAMBAAIRAxEAPwB/tDXdbfWztoIqTc85TtSdQVR6dFAcfc484zhI8ckjz767d4tyYW1diT69NdQh/hiGhXPcfXwkY/A5UfgHWXqqtULq3MlVK5pDkyprkJcUtxQVhWc4PtgfjxrOu9xmin6sBGVr3ruuZ2T/AK1aNMgxVctGouuPrUM+4QUAf9OvWgeuW6WH2zddp0mXGP7zT3XWFgfAWVjPxkanYe19EvWmxRVYjZLY6kOsgJVz7E++u+nemWzobSXpIlS1oCgQt7GST8fjAxpIZ1euxKj4dSnQh02w3stPdeKTbsxTNQbSC/T5QCH2+POM4UPkE/1oi6Qjfe1WLBplIuvb5s0Ot0qUgd2KogqRzjq/PI9/OcHTXbGbqMbu7fwa4O21Umz9PUmEKB7T6QM8ewIIUM/n40xVYtychJuRR8jseRePW9cz66tb9rpcWuJ9Gag/H6AptaitSUKVwfHQr/ukkNXfoNT/AMhGS2884sLHWn7UlJGU4Htp0PXTbalXBaddTEW+l6IuCopPkpWVpHnyO4SOD758DSkMU1E1SEoZZceS4QlLvKApWM9XPjXVZAZuUaxRsdey92Z6krviz40KPCjynJDyUNttJKck8YAB0Tq76tJtsV1+mVOgKeejFKXQ3IBAJAJxxzwdDWxrah2ruZQHrvcpUNhh9uQlcTK0OfweR9vuOPOpvfe0GqvuTIn2UzBrTdeH1DaYg61NvI+1xA6DjnCVYP8A60u1eO1gGupUZLddjZk7cW9VL3jtqXSoMaVSKmlxp1LMhSS2/wDqBOAsHj9wJBHgE840TPRjHetm+r0t2JJkT6Q5FS/9QYi2WvqG1pSoDq8HD3gnOACQPGhTZ1o0+zLRmXDuBCRSQ02UpYQtTbjym3AvowoZClKQlIxnIJIxq/8AoQh1Cr3Zely1BLzg7HZU+t1Sgp150OLABOM/p5JxnnW6lVSRX5EMviKtfsazdrbSDunaTtGnOGM+26l+JJQhKlsuJ9wFccgkf38azbvGxJNkXDVafKmuSPo5CWVmSyWHVKKerIQc/YRjB+RrV7VB3K2etfdOD2bjiFEtsHsTY5CXmjjHnwR44IPjRWQlgwMm0W/JwT5MuojKqtU4qJUhliI2e33XXVJ6EAk44B5OTyB5OdNRTG2nrPgwIdNoMSiNPB5t6ly3FOpdGMO9KkZ6jjCiVZIJ/jUgj0TTqRVpD1OrFKrUJaT2UVFtxpTSvZRCApKsfPHxomtbD1qoxo8Spz6VRoiQEvIpbSllScchOUoCc/x/WhZFbWaVfJeGbTw2T3Fc3hcqt4Xba1qUmE9UHJLJejdkFalLWpTfVjwUp6OSfnTtbH7XMbS2FDovUh6pOqMmpSEpA7shQGf6AASP4z76slAsWg226xIp0BoTWYqYqJSkguBpOSEg+wyT41Y9FqTggEg3W/RuvJ//2Q=="
			}
		};

		public override void Init ()
		{
			Task.Run (() => {
				Thread.Sleep(10000);
				GetItems()[0].Name = "Bondarenko Ilya";
				AppDelegate.Shared.Messenger.Publish (new ItemsChangedMessage<CustomerItem>(GetItems()));
			});
		}

		public override void SetModelIsDirty ()
		{

		}

		public override System.Collections.Generic.List<CustomerItem> GetItems ()
		{
			return _items;
		}
	}
}

