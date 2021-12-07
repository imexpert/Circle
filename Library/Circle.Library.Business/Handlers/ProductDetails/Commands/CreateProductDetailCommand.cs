using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Aspects.Autofac.Transaction;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Handlers.GroupClaims.Commands;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Entities.Concrete;
using MediatR;

namespace Circle.Library.Business.Handlers.ProductDetails.Commands
{
    public class CreateProductDetailCommand : IRequest<ResponseMessage<ProductDetail>>
    {
        public ProductDetail Model { get; set; }

        public class CreateProductDetailCommandHandler : IRequestHandler<CreateProductDetailCommand, ResponseMessage<ProductDetail>>
        {
            private readonly IProductDetailRepository _productDetailRepository;

            public CreateProductDetailCommandHandler(IProductDetailRepository productDetailRepository)
            {
                _productDetailRepository = productDetailRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[TransactionScopeAspect]
            public async Task<ResponseMessage<ProductDetail>> Handle(CreateProductDetailCommand request, CancellationToken cancellationToken)
            {
                _productDetailRepository.Add(request.Model);
                await _productDetailRepository.SaveChangesAsync();
                return ResponseMessage<ProductDetail>.Success(request.Model);
            }
        }
    }
}