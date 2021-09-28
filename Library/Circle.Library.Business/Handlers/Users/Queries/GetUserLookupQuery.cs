using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Dtos;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Users.Queries
{
    public class GetUserLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class
            GetUserLookupQueryHandler : IRequestHandler<GetUserLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserRepository _userRepository;

            public GetUserLookupQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserLookupQuery request, CancellationToken cancellationToken)
            {
                var list = await _userRepository.GetListAsync(x => x.Status);
                var userLookup = list.Select(x => new SelectionItem() 
                { 
                    Id = x.Id.ToString(), 
                    Label = $"{x.Lastname} {x.Lastname}" });
                return new SuccessDataResult<IEnumerable<SelectionItem>>(userLookup);
            }
        }
    }
}