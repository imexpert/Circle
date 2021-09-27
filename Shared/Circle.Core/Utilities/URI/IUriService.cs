using Circle.Core.Entities.Concrete;

namespace Circle.Core.Utilities.URI
{
    public interface IUriService
    {
        System.Uri GeneratePageRequestUri(PaginationFilter filter, string route);
    }
}