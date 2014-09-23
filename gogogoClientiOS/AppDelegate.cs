using System.Linq;
using Caliburn.Micro;
using gogogoClientiOS.BusinessService;
using gogogoClientiOS.ViewControllers;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public static AppDelegate Shared;

        private UIWindow _window;
        private UINavigationController _navigation;

        private IEventAggregator _messenger = new EventAggregator();
        public IEventAggregator Messenger
        {
            get
            {
                return _messenger;
            }
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Shared = this;

            EventService.GetInstance().Init();
            CommentService.GetInstance().Init();
            ParticipantService.GetInstance().Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            _window = new UIWindow(UIScreen.MainScreen.Bounds);
            //UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes {
            //	TextColor = UIColor.Blue
            //});

            var loginViewController = new LoginViewController();
            _navigation = new UINavigationController(loginViewController);

            _navigation.NavigationBar.TintColor = UIColor.Blue;
            _navigation.NavigationBar.BarTintColor = UIColor.White;
            _navigation.NavigationBar.Translucent = false;

            _window.RootViewController = _navigation;
            _window.MakeKeyAndVisible();
            return true;
        }

        public void ShowEventsList()
        {
            EventService.GetInstance().SetModelIsDirty();

            var eventsListViewController = new EventsListViewController();
            _navigation.PushViewController(eventsListViewController, true);
        }

        public void ShowEventDetails(string eventId)
        {
            var eventItem = EventService.GetInstance().GetItems().FirstOrDefault(item => item.Id == eventId);
            EventService.GetInstance().SetCurrentEvent(eventItem);
            var eventTabBarController = new EventTabViewController(eventItem);

            _navigation.PushViewController(eventTabBarController, true);
        }

    }
}

