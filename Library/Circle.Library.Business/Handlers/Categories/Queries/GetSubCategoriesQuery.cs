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

            private List<Category> GenerateProductCode(List<Category> all, Guid guid, List<Category> urunKodList)
            {
                if (guid == Guid.Empty)
                    return urunKodList;

                var current = all.FirstOrDefault(s => s.Id == guid);

                if (!string.IsNullOrEmpty(current.Code))
                    urunKodList.Add(new Category()
                    {
                        Code = current.Code,
                        Name = current.Name
                    });

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

                var butunKategoriler = await _categoryRepository.GetListAsync();

                if (request.Id != Guid.Empty)
                {
                    List<Category> urunKodList = new List<Category>();

                    urunKodList = GenerateProductCode(butunKategoriler, request.Id, urunKodList);

                    urunKodList.Reverse();

                    result.ProductCodeList = urunKodList;
                }

                result.Category = butunKategoriler.FirstOrDefault(s => s.Id == request.Id);

                List<Category> categories = new List<Category>();

                if (request.Id == Guid.Empty)
                {
                    categories = butunKategoriler.Where(x =>x.LinkedCategoryId == null).ToList();
                }
                else
                {
                    categories = butunKategoriler.Where(x => x.LinkedCategoryId == request.Id).ToList();

                    if (categories == null || categories.Count == 0)
                    {
                        result.IsLastCategory = true;
                    }
                }

                //foreach (var item in categories.OrderBy(s => s.Name).ToList())
                //{
                //    var subs = butunKategoriler.Where(s => s.LinkedCategoryId == item.Id && s.LanguageId == LanguageExtension.LanguageId).ToList();

                //    if (subs.Count > 0)
                //    {
                //        string desc = "<ul>#temp</ul>";
                //        string li = "";
                //        foreach (var s in subs.OrderBy(s => s.Name).ToList())
                //        {
                //            li += "<li>" + s.Name + "</li>";
                //        }

                //        item.Description = desc.Replace("#temp", li);
                //    }
                //}

                result.Categories = categories.OrderBy(s => s.Order).ToList();

                return _returnUtility.SuccessData(result);
            }
        }
    }
}