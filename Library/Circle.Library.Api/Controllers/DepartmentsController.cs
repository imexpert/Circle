using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Library.Business.Handlers.Groups.Commands;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Circle.Library.Business.Handlers.Groups.Queries;
using System;
using System.ComponentModel.DataAnnotations;
using Circle.Library.Business.Handlers.Categories.Queries;
using Circle.Library.Business.Handlers.Departments.Queries;
using Circle.Library.Business.Handlers.Departments.Commands;

namespace Circle.Library.Api.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///
    
    [ApiController]
    public class DepartmentsController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetDepartmentsQuery command = new GetDepartmentsQuery();

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWithId(Guid departmentId)
        {
            GetDepartmentQuery command = new GetDepartmentQuery()
            {
                Id = departmentId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            CreateDepartmentCommand command = new CreateDepartmentCommand()
            {
                Model = department
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Department department)
        {
            UpdateDepartmentCommand command = new UpdateDepartmentCommand()
            {
                Model = department
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid departmentId)
        {
            DeleteDepartmentCommand command = new DeleteDepartmentCommand()
            {
                Id = departmentId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }
    }
}