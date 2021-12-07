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
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProductDetailsQueryHandler(
                IProductRepository productRepository,
                IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<ProductDetailModel>>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
            {
                return ResponseMessage<List<ProductDetailModel>>.Success(new List<ProductDetailModel>());
            }
        }
    }
}
