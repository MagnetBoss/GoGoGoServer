using gogogoClientiOS.Model;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public class EventTabViewController : UITabBarController
	{
		private EventItem _currentEvent;

		public EventTabViewController (EventItem eventItem)
		{
			_currentEvent = eventItem ?? EventItem.NullEvent ();

			var eventDetailsViewController = new EventDetailsViewController (eventItem ?? EventItem.NullEvent ());
			eventDetailsViewController.TabBarItem = new UITabBarItem ();
			eventDetailsViewController.TabBarItem.Title = "Описание";
			eventDetailsViewController.TabBarItem.Image = UIImage.FromBundle ("info_filled-32.png");

			var participantsListViewController = new ParticipantsListViewController ();
			participantsListViewController.TabBarItem = new UITabBarItem ();
			participantsListViewController.TabBarItem.Title = "Участники";
			participantsListViewController.TabBarItem.Image = UIImage.FromBundle ("group-32.png");

			var commentsListViewController = new CommentsListViewController ();
			commentsListViewController.TabBarItem = new UITabBarItem ();
			commentsListViewController.TabBarItem.Title = "Осбуждение";
			commentsListViewController.TabBarItem.Image = UIImage.FromBundle ("topic-32.png");

			ViewControllers = new UIViewController[] {
				eventDetailsViewController,
				participantsListViewController,
				commentsListViewController
			};

			Title = _currentEvent.Name;

			TabBar.Translucent = false;
		}

	}
}

