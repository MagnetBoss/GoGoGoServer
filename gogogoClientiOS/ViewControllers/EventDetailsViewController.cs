using System.Drawing;
using Caliburn.Micro;
using gogogoClientiOS.Model;
using gogogoClientiOS.Model.Messages;
using gogogoClientiOS.Tools;
using gogogoClientiOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public class EventDetailsViewController : UIViewController, IHandle<ItemsChangedMessage<EventItem>>
	{
		private EventDetailsView _contentView;
		private UIScrollView _scrollView;
		private float _keyboardOffset = 0;
		private EventItem _currentEvent;

		public EventDetailsViewController (EventItem eventItem)
		{
			AutomaticallyAdjustsScrollViewInsets = false;

			_currentEvent = eventItem ?? EventItem.NullEvent();
			_contentView = new EventDetailsView (this);
			_contentView.Image = Converters.FromBase64 (_currentEvent.DescriptionImage);
			_contentView.Name = _currentEvent.Name;
			_contentView.LocationDescription = "Стадион школы №1037";
			_contentView.Date = _currentEvent.Date.ToString ("dd.MM");
			_contentView.Time = "в " + _currentEvent.Date.ToString ("hh:mm");
			_contentView.ParticipantsCount = _currentEvent.CurCustomers.ToString ();
			_contentView.ParticipantCountWord = "участника";
			_contentView.DescriptionText = _currentEvent.Description;
		}

		public virtual new void Handle(ItemsChangedMessage<EventItem> message)
		{
			InvokeOnMainThread (() => {
			});
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

