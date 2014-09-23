using gogogoClientiOS.BusinessService.DesignMode;
using gogogoClientiOS.Model;

namespace gogogoClientiOS.BusinessService
{
	public class EventService : BaseService<EventItem>
	{
		private static object _lock = new object();
		private static volatile EventService _instance;
		private EventItem _currentEvent = EventItem.NullEvent();

		protected EventService ()
		{
		}

		public static EventService GetInstance() {
			if (_instance != null)
				return _instance;
			lock (_lock) {
				if (_instance != null)
					return _instance;
				_instance = new EventDesignService ();
			}

			return _instance;
		}

		public void SetCurrentEvent(EventItem eventItem)
		{
			_currentEvent = eventItem ?? EventItem.NullEvent ();
			AppDelegate.Shared.Messenger.Publish(new CurrentEventChangedMessage(_currentEvent));
		}

		public virtual void Init()
		{
			CurrentPlatform.Init ();

			// Initialize the Mobile Service client with your URL and key
			_client = new MobileServiceClient (applicationURL, applicationKey, _instance);

			// Create an MSTable instance to allow us to work with the TodoItem table
			_table = _instance._client.GetTable <EventItem> ();
		}
	}
}

