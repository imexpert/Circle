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

namespace Circle.Library.Business.Handlers.Products.Queries
{
    public class GetProductQuery : IRequest<ResponseMessage<ProductModel>>
    {
        public Guid Id { get; set; }

        public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ResponseMessage<ProductModel>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProductQueryHandler(
                IProductRepository productRepository,
                IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<ProductModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(x => x.Id == request.Id);

                var urunKodList = await _mediator.Send(new GetProductCodeQuery() { Id = product.CategoryId });

                string productCode = urunKodList.Select(s => s.Name).Aggregate((i, j) => i + " " + j);

                ProductModel model = new ProductModel()
                {
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Image = product.Image,
                    Name = product.Name,
                    ProductCode = productCode
                };

                return ResponseMessage<ProductModel>.Success(model);
            }
        }
    }
}
