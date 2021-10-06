﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.Groups.Queries
{
    public class GetInternalGroupWithNameQuery : IRequest<Group>
    {
        public string GroupName { get; set; }

        public class GetInternalGroupWithNameQueryHandler : IRequestHandler<GetInternalGroupWithNameQuery,
            Group>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public GetInternalGroupWithNameQueryHandler(
                IGroupRepository groupRepository,
                IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            public async Task<Group> Handle(GetInternalGroupWithNameQuery request, CancellationToken cancellationToken)
            {
                return await _groupRepository.GetAsync(s => s.GroupName == request.GroupName);
            }
        }
    }
}