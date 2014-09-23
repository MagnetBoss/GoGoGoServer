using gogogoClientiOS.Model;

namespace gogogoClientiOS.BusinessService
{
	public class CurrentEventChangedMessage
	{
		public EventItem CurrentEventItem { get; private set; }

		public CurrentEventChangedMessage (EventItem eventItem)
		{
			CurrentEventItem = eventItem ?? EventItem.NullEvent ();
		}
	}
}

