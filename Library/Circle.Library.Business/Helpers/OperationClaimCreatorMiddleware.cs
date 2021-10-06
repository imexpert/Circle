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
using Circle.Library.Business.Handlers.GroupClaims.Queries;
using Circle.Library.Business.Handlers.GroupClaims.Commands;
using Circle.Core.Entities;
using Circle.Core.Entities.Enums;
using Circle.Library.Business.Handlers.Users.Commands;
using Circle.Library.Business.Handlers.Users.Queries;
using Circle.Library.Business.Handlers.UserGroups.Commands;

namespace Circle.Library.Business.Helpers
{
    public static class OperationClaimCreatorMiddleware
    {
        public static async Task UseDbOperationClaimCreator(this IApplicationBuilder app)
        {
            try
            {
                await SaveOperations();
                await SaveGroups();
                await SaveGroupClaims();
                await SaveAdminUser();
                await SaveAdminUserGroup();
            }
            catch { }
        }

        private async static Task SaveOperations()
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            List<OperationClaim> operations = await mediator.Send(new GetInternalOperationClaimsQuery());

            List<OperationClaim> claimList = new List<OperationClaim>();

            foreach (var operationName in GetOperationNames())
            {
                if (!operations.Any(s => s.Name == operationName))
                {
                    OperationClaim operationClaim = new OperationClaim();
                    operationClaim.Name = operationName;
                    operationClaim.Alias = operationName;
                    operationClaim.Description = operationName;
                    claimList.Add(operationClaim);
                }
            }

            await mediator.Send(new CreateInternalOperationClaimCommand
            {
                ModelList = claimList
            });
        }

        private async static Task SaveGroups()
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            List<Group> groups = await mediator.Send(new GetInternalGroupsQuery());
            if (!groups.Any(s => s.GroupName == "System Admin"))
            {
                Group group = new Group();
                group.GroupName = "System Admin";
                await mediator.Send(new CreateInternalGroupCommand
                {
                    Group = group
                });
            }
        }

        private async static Task SaveGroupClaims()
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            List<OperationClaim> operations = await mediator.Send(new GetInternalOperationClaimsQuery());
            List<Group> groups = await mediator.Send(new GetInternalGroupsQuery());


            if (groups.Any(s => s.GroupName == "System Admin"))
            {
                Guid groupId = groups.First(s => s.GroupName == "System Admin").Id;

                List<GroupClaim> groupClaims = await mediator.Send(new GetInternalGroupClaimsWithGroupIdQuery() { GroupId = groupId });

                List<GroupClaim> addedGroupClaims = new List<GroupClaim>();

                foreach (var item in operations)
                {
                    if (!groupClaims.Any(s => s.GroupId == groupId && s.OperationClaimId == item.Id))
                    {
                        GroupClaim groupClaim = new GroupClaim()
                        {
                            GroupId = groupId,
                            OperationClaimId = item.Id
                        };

                        addedGroupClaims.Add(groupClaim);
                    }
                }

                await mediator.Send(new CreateInternalGroupClaimCommand()
                {
                    ModelList = addedGroupClaims
                });
            }
        }

        private async static Task SaveAdminUser()
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            User user = new User()
            {
                Address = "Ankara",
                BirthDate = new DateTime(1983, 7, 23),
                DepartmentId = Guid.NewGuid(),
                Email = "admin@admin.com",
                Firstname = "Ali Osman",
                Gender = ((int)GenderTypes.Erkek),
                Lastname = "ÜNAL",
                MobilePhones = "+90 555 682 2232",
                Password = "Qweasd00.",
                Status = true
            };

            await mediator.Send(new CreateInternalUserCommand()
            {
                User = user
            });
        }

        private async static Task SaveAdminUserGroup()
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            User user = await mediator.Send(new GetInternalUserQuery()
            {
                Email = "admin@admin.com"
            });

            Group group = await mediator.Send(new GetInternalGroupWithNameQuery()
            {
                GroupName = "System Admin"
            });

            UserGroup userGroup = new UserGroup()
            {
                GroupId = group.Id,
                UserId = user.Id
            };

            await mediator.Send(new CreateInternalUserGroupCommand()
            {
                UserGroup = userGroup
            });
        }
        private static IEnumerable<string> GetOperationNames()
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                    // runtime generated anonmous type'larin assemblysi olmadigi icin null cek yap
                    x.Namespace != null && x.Namespace.StartsWith("Circle.Library.Business.Handlers") &&
                    (x.Name.EndsWith("Command") || x.Name.EndsWith("Query")) &&
                    (!x.Name.Contains("Internal")) &&
                    x.CustomAttributes.All(a => a.AttributeType == typeof(SecuredOperation)))
                .Select(x => x.Name);
            return assemblyNames;
        }
    }
}
