using Circle.Core.Utilities.Results;
using Circle.Library.Business.Handlers.Categories.Queries;
using Circle.Library.Business.Handlers.ProductDetails.Queries;
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

namespace Circle.Library.Business.Handlers.Products.Queries
{
    public class GetProductsQuery : IRequest<ResponseMessage<List<ProductModel>>>
    {
        public Guid Id { get; set; }

        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ResponseMessage<List<ProductModel>>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProductsQueryHandler(
                IProductRepository productRepository,
                IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<ProductModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                List<ProductModel> productModelList = new List<ProductModel>();

                var products = await _productRepository.GetListAsync();

                foreach (var item in products)
                {
                    var urunKodList = await _mediator.Send(new GetProductCodeQuery() { Id = item.CategoryId });

                    urunKodList.Reverse();

                    string productCode = string.Empty;

                    if (urunKodList.Count > 0)
                        productCode = urunKodList.Select(s => s.Code).Aggregate((i, j) => i + "" + j);

                    ProductModel model = new ProductModel()
                    {
                        CategoryId = item.CategoryId,
                        Description = item.Description,
                        Image = item.Image,
                        Name = item.Name,
                        ProductCode = productCode,
                        Id = item.Id
                    };

                    productModelList.Add(model);
                }

                return ResponseMessage<List<ProductModel>>.Success(productModelList);
            }
        }
    }
}
