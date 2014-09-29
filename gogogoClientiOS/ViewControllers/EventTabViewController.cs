using gogogoClientiOS.Model;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public sealed class EventTabViewController : UITabBarController
	{
	    public EventTabViewController (EventItem eventItem)
		{
		    EventItem currentEvent = eventItem ?? EventItem.NullEvent ();

			var eventDetailsViewController = new EventDetailsViewController (eventItem ?? EventItem.NullEvent ())
			{
			    TabBarItem = new UITabBarItem {Title = "Описание", Image = UIImage.FromBundle("info_filled-32.png")}
			};

		    var participantsListViewController = new ParticipantsListViewController (currentEvent.ParticipantItems)
		    {
		        TabBarItem = new UITabBarItem {Title = "Участники", Image = UIImage.FromBundle("group-32.png")}
		    };

		    var commentsListViewController = new CommentsListViewController
		    {
		        TabBarItem = new UITabBarItem {Title = "Осбуждение", Image = UIImage.FromBundle("topic-32.png")}
		    };

		    ViewControllers = new UIViewController[] {
				eventDetailsViewController,
				participantsListViewController,
				commentsListViewController
			};

			Title = currentEvent.Name;

			TabBar.Translucent = false;
		}

	}
}

