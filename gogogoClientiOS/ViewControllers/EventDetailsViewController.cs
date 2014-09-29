using System.Drawing;
using gogogoClientiOS.Model;
using gogogoClientiOS.Tools;
using gogogoClientiOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public sealed class EventDetailsViewController : UIViewController	{
		private readonly EventDetailsView _contentView;
		private UIScrollView _scrollView;
		private float _keyboardOffset = 0;

	    public EventDetailsViewController (EventItem eventItem)
		{
		    AutomaticallyAdjustsScrollViewInsets = false;

			EventItem currentEvent = eventItem ?? EventItem.NullEvent();
			_contentView = new EventDetailsView (this)
			{
			    Image = Converters.FromBase64(currentEvent.LargeImage),
			    Name = currentEvent.Name,
			    LocationDescription = "Стадион школы №1037",
			    Date = currentEvent.Date.ToString("dd.MM"),
			    Time = "в " + currentEvent.Date.ToString("hh:mm"),
			    ParticipantsCount = 2.ToString(),
			    ParticipantCountWord = "участника",
			    DescriptionText = currentEvent.Description
			};
		}

		public override void ViewDidLayoutSubviews ()
		{
			_scrollView.Frame = new RectangleF (0, 0, View.Frame.Width, View.Frame.Height);

			//Resize Scroller for keyboard;
			var bounds = _scrollView.Frame;
			//bounds.Height -= _keyboardOffset;
			_scrollView.Frame = bounds;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			_contentView.Frame = new RectangleF (0, 0, View.Frame.Width, View.Frame.Height - 20);
			View.BackgroundColor = UIColor.White;
			_scrollView = new UIScrollView (View.Bounds);
			View.AddSubview (_scrollView);
			_scrollView.Add (_contentView);

		}

		//TODO HACK
		public void SetScrollSize(SizeF size)
		{
			_scrollView.ContentSize = size;
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

			NavigationController.SetNavigationBarHidden (false, true);

			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, OnKeyboardNotification);

			NavigationController.NavigationBar.BackItem.Title = "";
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			NavigationController.SetNavigationBarHidden (false, false);

			NSNotificationCenter.DefaultCenter.RemoveObservers (new []{UIKeyboard.WillHideNotification,UIKeyboard.WillShowNotification});
		}

	}
}

