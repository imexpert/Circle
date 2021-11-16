using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using Circle.Library.Entities.ComplexTypes;

namespace Circle.Library.Business.Handlers.Categories.Queries
{
    public class GetSubCategoriesQuery : IRequest<ResponseMessage<CategoryListModel>>
    {
        public Guid Id { get; set; }

        public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQuery, ResponseMessage<CategoryListModel>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IReturnUtility _returnUtility;

            public GetSubCategoriesQueryHandler(
                ICategoryRepository categoryRepository,
                IReturnUtility returnUtility)
            {
                _categoryRepository = categoryRepository;
                _returnUtility = returnUtility;
            }

            private List<string> GenerateProductCode(List<Category> all, Guid guid, List<string> urunKodList)
            {
                if (guid == Guid.Empty)
                    return urunKodList;

                var current = all.FirstOrDefault(s => s.Id == guid);

                if (!string.IsNullOrEmpty(current.Code))
                    urunKodList.Add(current.Code);

                if (current.LinkedCategoryId.HasValue)
                {
                    GenerateProductCode(all, current.LinkedCategoryId.Value, urunKodList);
                }

                return urunKodList;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<CategoryListModel>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
            {
                CategoryListModel result = new CategoryListModel();

                var butunKategoriler = await _categoryRepository.GetListAsync(s => s.LanguageId == LanguageExtension.LanguageId);

                if (request.Id != Guid.Empty)
                {
                    List<string> urunKodList = new List<string>();

                    urunKodList = GenerateProductCode(butunKategoriler, request.Id, urunKodList);

                    urunKodList.Reverse();

                    if (urunKodList != null && urunKodList.Count > 0)
                        result.ProductCode = urunKodList.Aggregate((i, j) => i + "" + j);
                }

                //var parent = butunKategoriler.First(s=>s.li)

                result.Category = await _categoryRepository.GetAsync(s => s.Id == request.Id);

                List<Category> categories = new List<Category>();

                if (request.Id == Guid.Empty)
                {
                    categories = await _categoryRepository.GetListAsync(x => x.LanguageId == LanguageExtension.LanguageId && x.LinkedCategoryId == null);
                }
                else
                {
                    categories = await _categoryRepository.GetListAsync(x => x.LanguageId == LanguageExtension.LanguageId && x.LinkedCategoryId == request.Id);
                }

                foreach (var item in categories.OrderBy(s => s.Name).ToList())
                {
                    var subs = await _categoryRepository.GetListAsync(s => s.LinkedCategoryId == item.Id && s.LanguageId == LanguageExtension.LanguageId);

                    if (subs.Count > 0)
                    {
                        string desc = "<ul>#temp</ul>";
                        string li = "";
                        foreach (var s in subs.OrderBy(s => s.Name).ToList())
                        {
                            li += "<li>" + s.Name + "</li>";
                        }

                        item.Description = desc.Replace("#temp", li);
                    }
                }

                result.Categories = categories.OrderBy(s => s.Name).ToList();

                return _returnUtility.SuccessData(result);
            }
        }
    }
}