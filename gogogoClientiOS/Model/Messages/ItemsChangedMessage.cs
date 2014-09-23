using System.Collections.Generic;

namespace gogogoClientiOS.Model.Messages
{
	//Поменялись значения данных элементов
	public class ItemsChangedMessage<T>
		where T : BaseItem
	{
		private List<T> _items;
		public List<T> Items {
			get {
				return _items;
			}
		}

		public ItemsChangedMessage (List<T> items)
		{
			_items = items;	
		}
	}

	//Добавились значения элементов
	public class ItemsAddedMessage<T>
		where T : BaseItem
	{
		private List<T> _items;
		public List<T> Items {
			get {
				return _items;
			}
		}

		public ItemsAddedMessage (List<T> items)
		{
			_items = items;	
		}
	}

	//Удалились значения элементов
	public class ItemsDeletedMessage<T>
		where T : BaseItem
	{
		private List<T> _items;
		public List<T> Items {
			get {
				return _items;
			}
		}

		public ItemsDeletedMessage (List<T> items)
		{
			_items = items;	
		}
	}
}

