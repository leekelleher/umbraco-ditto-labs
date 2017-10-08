using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    public class IsVisibleAttribute : PublishedContentFilterAttribute
    {
        public IsVisibleAttribute()
        {
            Filter = x => x.IsVisible();
        }
    }
}