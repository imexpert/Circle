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

namespace Circle.Library.Business.Handlers.Products.Commands
{
    public class CreateProductCommand : IRequest<ResponseMessage<Product>>
    {
        public Product Model { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseMessage<Product>>
        {
            private readonly IProductRepository _productRepository;

            public CreateProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[TransactionScopeAspect]
            public async Task<ResponseMessage<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                _productRepository.Add(request.Model);
                await _productRepository.SaveChangesAsync();
                return ResponseMessage<Product>.Success(request.Model);
            }
        }
    }
}