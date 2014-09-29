using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using gogogoClientiOS.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace gogogoClientiOS.BusinessService
{
    public class EventService : DelegatingHandler
	{
		private static readonly object Lock = new object();
		private static volatile EventService _instance;

        protected const string ApplicationUrl = @"http://localhost:8083/";
        protected const string ApplicationKey = @"XHXdlpQuMFvqNesQtABHmzvlTajanC93";


	    private MobileServiceClient _client;
        private IMobileServiceSyncTable<EventItem> _table; 

		protected EventService ()
		{
		}

		public static EventService GetInstance() {
			if (_instance != null)
				return _instance;
			lock (Lock) {
				if (_instance != null)
					return _instance;
				_instance = new EventService();
			}

			return _instance;
		}

        private bool _startedIniting;
		public void Init()
		{
		    if (_startedIniting)
		        return;
		    _startedIniting = true;
			CurrentPlatform.Init ();
            
            SQLitePCL.CurrentPlatform.Init();

			// Initialize the Mobile Service client with your URL and key
			_client = new MobileServiceClient (ApplicationUrl, ApplicationKey, _instance);

			// Create an MSTable instance to allow us to work with the TodoItem table
		    _table = _instance._client.GetSyncTable<EventItem>();
		}

        public async Task<List<EventItem>> GetItems()
        {
            return await _table.ToListAsync();
        }

        private bool _storeInitializing;
        public async Task InitializeStoreAsync()
        {
            if (_storeInitializing)
                return;
            _storeInitializing = true;
            const string path = "syncstore.db";
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<EventItem>();
            await _client.SyncContext.InitializeAsync(store);
        }

        public async Task SyncAsync()
        {
            try
            {
                await _client.SyncContext.PushAsync();
                await _table.PullAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Console.Error.WriteLine(@"Sync Failed: {0}", e.Message);
            }
        }
	}
}

