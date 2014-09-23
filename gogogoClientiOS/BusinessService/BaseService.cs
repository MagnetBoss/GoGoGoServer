using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gogogoClientiOS.Model;
using gogogoClientiOS.Model.Messages;

namespace gogogoClientiOS.BusinessService
{
	public abstract class BaseService<T> : DelegatingHandler
		where T : BaseItem
	{
		protected const string applicationURL = @"https://moneyfactory.azure-mobile.net/";
		protected const string applicationKey = @"XHXdlpQuMFvqNesQtABHmzvlTajanC93";

		private bool _modelIsDirty;
		private object _itemsLock = new object ();

		private List<T> _items = new List<T>();

		public virtual List<T> GetItems() {
			List<T> itemsCopy;

			lock (_itemsLock) {
				itemsCopy = _items.ToList();
			}
			return itemsCopy;
		}

		protected IMobileServiceTable<T> _table;
		protected MobileServiceClient _client;
		private int _busyCount = 0;
		private Dictionary<string, string> _additionalQueryParameters = new Dictionary<string, string>();

		public event Action<bool> BusyUpdate;

		public virtual void SetModelIsDirty()
		{
			if (_modelIsDirty)
				return;
			_modelIsDirty = true;

			StartRefreshItems ();
		}

		private object _modelLock = new object ();

		private void StartRefreshItems()
		{
			Task.Run (() => {
				IDictionary<string, string> additionalQueryParameters;
				lock (_additionalQueryParameters) {
					additionalQueryParameters = _additionalQueryParameters.ToDictionary(item => item.Key, item => item.Value);
				}
				lock (_modelLock)
				{
					try
					{
						Task<List<T>> task = _table.Where(AdditionalQuery()).WithParameters(additionalQueryParameters) .ToListAsync();
						var items = task.GetAwaiter().GetResult();

						lock (_itemsLock)
						{
							_items.Clear ();
							_items.AddRange (items);
						}

						_modelIsDirty = false;

						AppDelegate.Shared.Messenger.Publish (new ItemsChangedMessage<T>(items));
					}
					catch (Exception ex)
					{
					}
				}
			});
		}

		protected void SetAdditionalQueryParameters(IDictionary<string, string> additionalQueryParameters)
		{
			lock (_additionalQueryParameters) {
				_additionalQueryParameters.Clear ();
				if (additionalQueryParameters == null || additionalQueryParameters.Count == 0)
					return;
				foreach (var item in additionalQueryParameters) {
					_additionalQueryParameters.Add (item.Key, item.Value);
				}
			}
		}

		protected virtual Expression<Func<T, bool>> AdditionalQuery()
		{
			return item => true;
		}

		public async Task InsertItemAsync (T item)
		{
			try {
				// This code inserts a new TodoItem into the database. When the operation completes
				// and Mobile Services has assigned an Id, the item is added to the CollectionView
				BeforeInsert(item);
				await _table.InsertAsync (item);
				SetModelIsDirty();

			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
		}

		protected virtual void BeforeInsert (T item)
		{
		}

		protected void Busy (bool busy)
		{
			// assumes always executes on UI thread
			if (busy) 
			{
				if (_busyCount++ == 0 && BusyUpdate != null)
					BusyUpdate (true);

			} 
			else 
			{
				if (--_busyCount == 0 && BusyUpdate != null)
					BusyUpdate (false);
			}
		}

		#region implemented abstract members of HttpMessageHandler

		protected override async Task<System.Net.Http.HttpResponseMessage> SendAsync (System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			Busy (true);
			var response = await base.SendAsync (request, cancellationToken);

			Busy (false);
			return response;
		}

		#endregion
	}

	public class ItemsChangedEventArgs<T> : EventArgs
	{
		private List<T> _items;
		public List<T> Items {
			get {
				return _items;
			}
		}

		public ItemsChangedEventArgs (List<T> items)
		{
			_items = items;
		}
	}
}

