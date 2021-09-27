using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Business;
using Circle.Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Groups.Queries
{
    public class SearchGroupsByNameQuery : IRequest<IDataResult<IEnumerable<Group>>>
    {
        public string GroupName { get; set; }

        public class
            SearchGroupsByNameQueryHandler : IRequestHandler<SearchGroupsByNameQuery, IDataResult<IEnumerable<Group>>>
        {
            private readonly IGroupRepository _groupRepository;

            public SearchGroupsByNameQueryHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<Group>>> Handle(SearchGroupsByNameQuery request, CancellationToken cancellationToken)
            {
                var result = BusinessRules.Run(StringLengthMustBeGreaterThanThree(request.GroupName));

                if (result != null)
                {
                    return new ErrorDataResult<IEnumerable<Group>>(result.Message);
                }

                return new SuccessDataResult<IEnumerable<Group>>(
                    await _groupRepository.GetListAsync(
                        x => x.GroupName.ToLower().Contains(request.GroupName.ToLower())));
            }

            private static IResult StringLengthMustBeGreaterThanThree(string searchString)
            {
                if (searchString.Length >= 3)
                {
                    return new SuccessResult();
                }

                return new ErrorResult(Messages.StringLengthMustBeGreaterThanThree);
            }
        }
    }
}