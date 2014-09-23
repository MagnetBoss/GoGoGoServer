using System.Collections.Generic;
using Caliburn.Micro;
using gogogoClientiOS.BusinessService.DesignMode;
using gogogoClientiOS.Model;

namespace gogogoClientiOS.BusinessService
{
	public class CommentService : BaseService<CommentItem>, IHandle<CurrentEventChangedMessage>
	{
		private static object _lock = new object();
		private static volatile CommentService _instance;

		protected CommentService ()
		{
			AppDelegate.Shared.Messenger.Subscribe (this);
		}

		public virtual void Handle(CurrentEventChangedMessage message)
		{
			if (message == null)
				return;
			if (message.CurrentEventItem == null || string.IsNullOrEmpty (message.CurrentEventItem.Id))
				return;
			var additionalParameters = new Dictionary<string, string> ();
			additionalParameters.Add ("eventId", message.CurrentEventItem.Id);
			SetAdditionalQueryParameters (additionalParameters);
			SetModelIsDirty ();
		}

		public static CommentService GetInstance() {
			if (_instance != null)
				return _instance;
			lock (_lock) {
				if (_instance != null)
					return _instance;
				_instance = new CommentDesignService ();
			}

			return _instance;
		}

		public virtual void Init()
		{
			CurrentPlatform.Init ();

			// Initialize the Mobile Service client with your URL and key
			_client = new MobileServiceClient (applicationURL, applicationKey, _instance);

			// Create an MSTable instance to allow us to work with the TodoItem table
			_table = _instance._client.GetTable <CommentItem> ();
		}
	}
}

