using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Circle.Library.DataAccess.Concrete.EntityFramework
{
    public class LanguageRepository : EntityRepositoryBase<Language, ProjectDbContext>, ILanguageRepository
    {
        public LanguageRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<List<SelectionItem>> GetLanguagesLookUp()
        {
            var lookUp = await (from entity in Context.Languages
                select new SelectionItem()
                {
                    Id = entity.Id,
                    Label = entity.Name
                }).ToListAsync();
            return lookUp;
        }

        public async Task<List<SelectionItem>> GetLanguagesLookUpWithCode()
        {
            var lookUp = await (from entity in Context.Languages
                select new SelectionItem()
                {
                    Id = entity.Code.ToString(),
                    Label = entity.Name
                }).ToListAsync();
            return lookUp;
        }
    }
}