using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gogogoClientiOS.BusinessService;
using gogogoClientiOS.Model;
using gogogoClientiOS.Tools;
using gogogoClientiOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public sealed class EventsListViewController : UITableViewController
	{
		public List<EventItem> EventItems { get; private set; } 

		public EventsListViewController ()
		{
			EventItems = new List<EventItem> ();

			AppDelegate.Shared.Messenger.Subscribe (this);

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new EventsListTableSource (this);
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

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            EventService.GetInstance().Init();
            await EventService.GetInstance().InitializeStoreAsync();
		    await RefreshAsync();
            AddRefreshControl();
		}

        private void AddRefreshControl()
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0)) return;
            // the refresh control is available, let's add it
            RefreshControl = new UIRefreshControl();
            RefreshControl.ValueChanged += async (sender, e) =>
            {
                await RefreshAsync();
            };
        }

        private bool _isLoading;
        private async Task RefreshAsync()
		{
		    if (_isLoading)
		        return;
            _isLoading = true;
            if (RefreshControl != null)
                RefreshControl.BeginRefreshing();
		    try
		    {
		        await EventService.GetInstance().SyncAsync();
		        EventItems.Clear();
		        EventItems.AddRange(await EventService.GetInstance().GetItems());
                TableView.ReloadData();
		    }
		    catch (Exception ex)
		    {
		        Console.WriteLine(ex.Message);   
		    }
		    finally
		    {
                _isLoading = false;
                if (RefreshControl != null)
                    RefreshControl.EndRefreshing();
            }
        }
	}

	public class EventsListTableSource : UITableViewSource {
	    private const string CellIdentifier = "CustomEventTableCell";
	    private readonly EventsListViewController _parentController;
		public EventsListTableSource (EventsListViewController parentController)
		{
			_parentController = parentController;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _parentController.EventItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (CellIdentifier) as EventCellView ?? new EventCellView (CellIdentifier);

		    var eventItem = indexPath.Row < _parentController.EventItems.Count ? 
				_parentController.EventItems [indexPath.Row] : EventItem.NullEvent ();

			cell.UpdateCell (eventItem.Name
				, "Братеево, 14 сентября, 20:00"
				, Converters.FromBase64(eventItem.ShortImage));

			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 140;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
		    var selectedItem = indexPath.Row < _parentController.EventItems.Count
		        ? _parentController.EventItems[indexPath.Row]
		        : EventItem.NullEvent();

            AppDelegate.Shared.ShowEventDetails(selectedItem);
		}
	}
}

