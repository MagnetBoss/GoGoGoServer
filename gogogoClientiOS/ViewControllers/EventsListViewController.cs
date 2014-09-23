using System;
using System.Collections.Generic;
using Caliburn.Micro;
using gogogoClientiOS.BusinessService;
using gogogoClientiOS.Model;
using gogogoClientiOS.Model.Messages;
using gogogoClientiOS.Tools;
using gogogoClientiOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public class EventsListViewController : UITableViewController, IHandle<ItemsChangedMessage<EventItem>>
	{
		public List<EventItem> EventItems { get; private set; } 

		public EventsListViewController ()
		{
			EventItems = new List<EventItem> ();

			AppDelegate.Shared.Messenger.Subscribe (this);

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new EventsListTableSource (this);
		}

		public virtual new void Handle(ItemsChangedMessage<EventItem> message)
		{
			InvokeOnMainThread (() => {
				Refresh ();
			});
		}

		public override void LoadView ()
		{
			base.LoadView ();
			View.BackgroundColor = UIColor.White;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NavigationController.SetNavigationBarHidden (true, true);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Refresh ();
		}

		void Refresh ()
		{
			try {
				EventItems.Clear ();
				EventItems.AddRange (EventService.GetInstance ().GetItems ());
				TableView.ReloadData ();
			} catch (Exception ex)
			{

			}
		}

	}

	public class EventsListTableSource : UITableViewSource {
		string cellIdentifier = "CustomEventTableCell";
		private EventsListViewController _parentController;
		public EventsListTableSource (EventsListViewController parentController)
		{
			_parentController = parentController;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _parentController.EventItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			EventCellView cell = tableView.DequeueReusableCell (cellIdentifier) as EventCellView;

			if (cell == null) {
				cell = new EventCellView (cellIdentifier);
			}

			var eventItem = indexPath.Row < _parentController.EventItems.Count ? 
				_parentController.EventItems [indexPath.Row] : EventItem.NullEvent ();

			cell.UpdateCell (eventItem.Name
				, "Братеево, 14 сентября, 20:00"
				, Converters.FromBase64(eventItem.Image));

			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 140;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			AppDelegate.Shared.ShowEventDetails (_parentController.EventItems [indexPath.Row].Id);
		}
	}
}

