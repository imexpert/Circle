using Circle.Core.Utilities.IoC;
using Circle.Library.Business.BusinessAspects;
using MediatR;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Circle.Library.Business.Handlers.OperationClaims.Commands;
using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.OperationClaims.Queries;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.Handlers.Groups.Queries;
using Circle.Library.Business.Handlers.Groups.Commands;

namespace Circle.Library.Business.Helpers
{
    public static class OperationClaimCreatorMiddleware
    {
        public static async Task UseDbOperationClaimCreator(this IApplicationBuilder app)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            await SaveOperations();

            Guid adminGroupId = await SaveGroups();

            var operationClaims = (await mediator.Send(new GetOperationClaimsInternalQuery())).Data;
            var user = await mediator.Send(new RegisterUserInternalCommand
            {
                FullName = "System Admin",
                Password = "Q1w212*_*",
                Email = "admin@adminmail.com",
            });
            await mediator.Send(new CreateUserClaimsInternalCommand
            {
                UserId = 1,
                OperationClaims = operationClaims
            });
        }

        private async static Task SaveOperations()
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            //Uygulama ayağa kalkarken database de kayıtlı olmayan bütün operasyonları
            //kaydediyoruz.
            ResponseMessage<List<OperationClaim>> operationsResponse = await mediator.Send(new GetOperationClaimsQuery());

            if (operationsResponse.IsSuccess)
            {
                List<OperationClaim> operations = operationsResponse.Data;

                foreach (var operationName in GetOperationNames())
                {
                    if (!operations.Any(s => s.Name == operationName))
                    {
                        OperationClaim operationClaim = new OperationClaim();
                        operationClaim.Name = operationName;
                        operationClaim.Alias = operationName;
                        operationClaim.Description = operationName;

                        await mediator.Send(new CreateOperationClaimCommand
                        {
                            Model = operationClaim
                        });
                    }
                }
            }
        }

        private async static Task<Guid> SaveGroups()
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            //System Admin adında bir grup yoksa onu da ekliyoruz
            ResponseMessage<List<Group>> groupsResponse = await mediator.Send(new GetGroupsQuery());
            if (groupsResponse.IsSuccess)
            {
                List<Group> groups = groupsResponse.Data;
                if (!groups.Any(s => s.GroupName == "System Admin"))
                {
                    ResponseMessage<Group> group = await mediator.Send(new CreateGroupCommand
                    {
                        GroupName = "System Admin"
                    });

                    if (group.IsSuccess)
                    {
                        return group.Data.Id;
                    }
                }
                else
                {
                    return groups.FirstOrDefault(s => s.GroupName == "System Admin").Id;
                }
            }

            return Guid.Empty;
        }

        private async static Task<Guid> SaveGroupClaims(Guid groupId)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            ResponseMessage<List<OperationClaim>> operationsResponse = await mediator.Send(new GetOperationClaimsQuery());

            if (operationsResponse.IsSuccess)
            {
                foreach (var item in operationsResponse.Data)
                {
                    GroupClaim groupClaim = new GroupClaim()
                    {
                        GroupId = groupId,
                        OperationClaimId = item.Id
                    }
                }
            }

            return Guid.Empty;
        }

        private static IEnumerable<string> GetOperationNames()
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                    // runtime generated anonmous type'larin assemblysi olmadigi icin null cek yap
                    x.Namespace != null && x.Namespace.StartsWith("Business.Handlers") &&
                    (x.Name.EndsWith("Command") || x.Name.EndsWith("Query")) &&
                    x.CustomAttributes.All(a => a.AttributeType == typeof(SecuredOperation)))
                .Select(x => x.Name);
            return assemblyNames;
        }
    }
}
