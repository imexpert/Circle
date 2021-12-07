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
    public class UpdateProductCommand : IRequest<ResponseMessage<Product>>
    {
        public UpdateProuctModel Model { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseMessage<Product>>
        {
            private readonly IProductRepository _productRepository;

            public UpdateProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[TransactionScopeAspect]
            public async Task<ResponseMessage<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                Product product = await _productRepository.GetAsync(s => s.Id == request.Model.ProductId);
                product.CategoryId = request.Model.ProductCategoryId;
                product.Description = request.Model.ProductDescription;
                product.Image = request.Model.Image;
                product.Name = request.Model.ProductName;

                _productRepository.Update(product);
                await _productRepository.SaveChangesAsync();
                return ResponseMessage<Product>.Success(product);
            }
        }
    }
}