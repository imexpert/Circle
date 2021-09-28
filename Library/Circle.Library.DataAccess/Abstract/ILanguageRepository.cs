using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.DataAccess;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;

namespace Circle.Library.DataAccess.Abstract
{
    public interface ILanguageRepository : IEntityRepository<Language>
    {
        Task<List<SelectionItem>> GetLanguagesLookUp();
        Task<List<SelectionItem>> GetLanguagesLookUpWithCode();
    }
}