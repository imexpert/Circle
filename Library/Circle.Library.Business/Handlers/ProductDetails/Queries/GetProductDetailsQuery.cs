using Circle.Core.Utilities.Results;
using Circle.Library.Business.Handlers.Categories.Queries;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.ProductDetails.Queries
{
    public class GetProductDetailsQuery : IRequest<ResponseMessage<List<ProductDetailModel>>>
    {
        public Guid ProductId { get; set; }

        public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ResponseMessage<List<ProductDetailModel>>>
        {
            private readonly IProductDetailRepository _productDetailRepository;
            private readonly ICategoryAttributeRepository _categoryAttributeRepository;

            public GetProductDetailsQueryHandler(
                IProductDetailRepository productDetailRepository,
                ICategoryAttributeRepository categoryAttributeRepository)
            {
                _productDetailRepository = productDetailRepository;
                _categoryAttributeRepository = categoryAttributeRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<ProductDetailModel>>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
            {
                List<ProductDetailModel> list = new List<ProductDetailModel>();

                var productDetailList = await _productDetailRepository.GetListAsync(s => s.ProductId == request.ProductId);

                foreach (var item in productDetailList)
                {
                    var material = await _categoryAttributeRepository.GetAsync(s => s.Id == item.Material);
                    var materialDetail = await _categoryAttributeRepository.GetAsync(s => s.Id == item.MaterialDetail);
                    var diameter = await _categoryAttributeRepository.GetAsync(s => s.Id == item.Diameter);
                    var length = await _categoryAttributeRepository.GetAsync(s => s.Id == item.Length);

                    list.Add(new ProductDetailModel()
                    {
                        MaterialName = material != null ? material.Name : "",
                        MaterialDetailName = materialDetail != null ? materialDetail.Name : "",
                        MaterialTypeCode = material != null ? material.TypeCode : 0,
                        Diameter = diameter != null ? diameter.Code : "",
                        Length = length != null ? length.Code : "",
                        Material = material != null ? material.Code : "",
                        MaterialDetail = materialDetail != null ? materialDetail.Code : "",
                        ProductDetailId = item.Id
                    });
                }

                return ResponseMessage<List<ProductDetailModel>>.Success(list);
            }
        }
    }
}
