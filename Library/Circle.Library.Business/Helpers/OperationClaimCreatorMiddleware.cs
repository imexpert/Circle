using Circle.Core.Utilities.IoC;
using Circle.Library.Business.BusinessAspects;
using MediatR;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Circle.Library.Business.Handlers.OperationClaims.Commands;
using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.OperationClaims.Queries;
using Circle.Library.Business.Handlers.Groups.Queries;
using Circle.Library.Business.Handlers.Groups.Commands;
using Circle.Library.Business.Handlers.GroupClaims.Queries;
using Circle.Library.Business.Handlers.GroupClaims.Commands;
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
                Guid engId = new Guid("852a6581-7493-4669-982d-a9e30dbfd000");
                Guid trId = new Guid("19347039-d124-48fb-9ec8-fa45f7797dfc");

                await SaveOperations(engId, trId);
                await SaveGroups(engId, trId);
                await SaveGroupClaims(engId);
                await SaveAdminUser();
                await SaveAdminUserGroup();
            }
            catch (Exception e)
            {

            }
        }

        private async static Task SaveOperations(Guid engId, Guid trId)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            List<OperationClaim> operations = await mediator.Send(new GetInternalOperationClaimsQuery());

            List<OperationClaim> claimList = new List<OperationClaim>();

            foreach (var operationName in GetOperationNames())
            {
                if (!operations.Any(s => s.Name == operationName))
                {
                    Guid id = Guid.NewGuid();

                    string descriptionEn = "";
                    string descriptionTr = "";

                    if (operationName.Contains("Create"))
                    {
                        string temp = operationName.CutStart("Create");
                        temp = temp.CutStart("Command");

                        descriptionEn = "Create - " + temp;
                        descriptionTr = "Ekleme - " + temp;
                    }

                    if (operationName.Contains("Update"))
                    {
                        string temp = operationName.CutStart("Update");
                        temp = temp.CutStart("Command");

                        descriptionEn = "Update - " + temp;
                        descriptionTr = "Güncelleme - " + temp;
                    }

                    if (operationName.Contains("Delete"))
                    {
                        string temp = operationName.CutStart("Delete");
                        temp = temp.CutStart("Command");

                        descriptionEn = "Delete - " + temp;
                        descriptionTr = "Silme - " + temp;
                    }

                    if (operationName.Contains("sQuery"))
                    {
                        string temp = operationName.CutStart("sQuery");
                        temp = temp.CutStart("Get");

                        descriptionEn = "Get List - " + temp;
                        descriptionTr = "Liste Getir - " + temp;
                    }

                    if (operationName.Contains("Query") && !operationName.Contains("sQuery"))
                    {
                        string temp = operationName.CutStart("Query");
                        temp = temp.CutStart("Get");

                        descriptionEn = "Get - " + temp;
                        descriptionTr = "Oku - " + temp;
                    }

                    OperationClaim operationClaimEn = new OperationClaim
                    {
                        Id = id,
                        LanguageId = engId,
                        Name = operationName,
                        Alias = operationName,
                        Description = descriptionEn
                    };

                    OperationClaim operationClaimTr = new OperationClaim
                    {
                        Id = id,
                        LanguageId = trId,
                        Name = operationName,
                        Alias = operationName,
                        Description = descriptionTr
                    };

                    claimList.Add(operationClaimTr);
                    claimList.Add(operationClaimEn);
                }
            }

            await mediator.Send(new CreateInternalOperationClaimCommand
            {
                ModelList = claimList
            });
        }

        private async static Task SaveGroups(Guid engId, Guid trId)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            List<Group> groups = await mediator.Send(new GetInternalGroupsQuery());
            if (!groups.Any(s => s.GroupName == "System Admin"))
            {
                Guid id = Guid.NewGuid();

                Group groupEn = new Group();
                groupEn.Id = id;
                groupEn.LanguageId = engId;
                groupEn.GroupName = "System Admin";
                await mediator.Send(new CreateInternalGroupCommand
                {
                    Group = groupEn
                });

                Group groupTr = new Group();
                groupTr.Id = id;
                groupTr.LanguageId = trId;
                groupTr.GroupName = "Sistem Yönetcisi";
                await mediator.Send(new CreateInternalGroupCommand
                {
                    Group = groupTr
                });
            }
        }

        private async static Task SaveGroupClaims(Guid engId)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            List<OperationClaim> operations = await mediator.Send(new GetInternalOperationClaimsQuery());

            List<Group> groups = await mediator.Send(new GetInternalGroupsQuery());


            if (groups.Any(s => s.GroupName == "System Admin" && s.LanguageId == engId))
            {
                Guid groupId = groups.First(s => s.GroupName == "System Admin" && s.LanguageId == engId).Id;

                List<GroupClaim> groupClaims = await mediator.Send(new GetInternalGroupClaimsWithGroupIdQuery() { GroupId = groupId });

                List<GroupClaim> addedGroupClaims = new List<GroupClaim>();

                foreach (var item in operations.Where(s => s.LanguageId == engId))
                {
                    if (!groupClaims.Any(s => s.GroupId == groupId && s.OperationClaimId == item.Id))
                    {
                        GroupClaim groupClaim = new GroupClaim()
                        {
                            Id = Guid.NewGuid(),
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
                DepartmentId = new Guid("3a3d8d33-e5cd-46f5-8dc8-fd11a06c20b8"),
                Email = "admin@admin.com",
                Firstname = "Ali Osman",
                Gender = ((int)GenderTypes.Erkek),
                Lastname = "ÜNAL",
                MobilePhones = "+90 555 682 2232",
                Password = "Qweasd00.",
                Status = true,
                Image = null
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

            UserGroup userGroup = new UserGroup()
            {
                GroupId = new Guid("EF222783-DEAA-4C5A-84FE-E1E20FD78EB7"),
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
        public static string CutStart(this string s, string what)
        {
            if (s.Contains(what))
            {
                //substring to remove from string.
                string substring = what;

                //this line get index of substring from string
                int indexOfSubString = s.IndexOf(substring);

                //remove specified substring from string
                string withoutSubString = s.Remove(indexOfSubString, substring.Length);

                return withoutSubString;
            }

            return s;
        }
    }
}
