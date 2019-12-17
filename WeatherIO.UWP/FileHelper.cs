using System.IO;
using Xamarin.Forms;
using WeatherIO.UWP;
using Windows.Storage;

[assembly: Dependency(typeof(FileHelper))]
namespace WeatherIO.UWP
{
	public class FileHelper : IFileHelper
	{
		public string GetLocalFilePath(string filename)
		{
			return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
		}
	}
}