using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace HSDeck.Services.ImageLoader
{
    public interface IImageLoader
    {
        Task<BitmapImage> GetFromUrl(string url);
    }
}