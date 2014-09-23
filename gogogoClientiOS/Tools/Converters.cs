using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.Tools
{
	public static class Converters
	{
		//TODO move to tools
		public static UIImage FromBase64 (string str)
		{
			if (string.IsNullOrEmpty (str))
				return null;
			try {
				var base64Bytes = Convert.FromBase64String(str);  
				var data = NSData.FromArray (base64Bytes);                              
				var uiImage = UIImage.LoadFromData(data);

				return uiImage;
			}
			catch (Exception ex) {
				return null;
			}
		}
	}
}

