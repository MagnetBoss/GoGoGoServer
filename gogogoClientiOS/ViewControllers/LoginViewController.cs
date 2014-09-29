using gogogoClientiOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public sealed class LoginViewController : UIViewController
	{
		private UIView _contentView;
		private UIScrollView _scrollView;
		private float _keyboardOffset;

		public LoginViewController ()
		{
			Title = "Авторизация";
			//This hides the back button text when you leave this View Controller
			NavigationItem.BackBarButtonItem = new UIBarButtonItem ("", UIBarButtonItemStyle.Plain, handler: null);
			AutomaticallyAdjustsScrollViewInsets = false;
		}

		public override void ViewDidLayoutSubviews ()
		{
			var bounds = View.Bounds;
			_contentView.Frame = bounds;
			_scrollView.ContentSize = bounds.Size;
			//Resize Scroller for keyboard;
			bounds.Height -= _keyboardOffset;
			_scrollView.Frame = bounds;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			View.AddSubview (_scrollView = new UIScrollView (View.Bounds));
			_scrollView.Add (_contentView = new LoginView ());
		}
		private void OnKeyboardNotification (NSNotification notification)
		{
			if (IsViewLoaded) {

				//Check if the keyboard is becoming visible
				bool visible = notification.Name == UIKeyboard.WillShowNotification;
				UIView.Animate (UIKeyboard.AnimationDurationFromNotification (notification), () => {
					UIView.SetAnimationCurve ((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification (notification));
					var frame = UIKeyboard.FrameEndFromNotification (notification);
					_keyboardOffset = visible ? frame.Height : 0; 
					ViewDidLayoutSubviews ();
				});
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			NavigationController.SetNavigationBarHidden (true, false);

			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			NavigationController.SetNavigationBarHidden (false, false);

			NSNotificationCenter.DefaultCenter.RemoveObservers (new []{UIKeyboard.WillHideNotification,UIKeyboard.WillShowNotification});
		}
	}
}

