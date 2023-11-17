using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HrApp.Server.Controllers.HrDB
{
    [Route("odata/HrDB/Departments")]
    public partial class DepartmentsController : ODataController
    {
        private HrApp.Server.Data.HrDBContext context;

        public DepartmentsController(HrApp.Server.Data.HrDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<HrApp.Server.Models.HrDB.Department> GetDepartments()
        {
            var items = this.context.Departments.AsQueryable<HrApp.Server.Models.HrDB.Department>();
            this.OnDepartmentsRead(ref items);

            return items;
        }

        partial void OnDepartmentsRead(ref IQueryable<HrApp.Server.Models.HrDB.Department> items);

        partial void OnDepartmentGet(ref SingleResult<HrApp.Server.Models.HrDB.Department> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/HrDB/Departments(department_id={department_id})")]
        public SingleResult<HrApp.Server.Models.HrDB.Department> GetDepartment(int key)
        {
            var items = this.context.Departments.Where(i => i.department_id == key);
            var result = SingleResult.Create(items);

            OnDepartmentGet(ref result);

            return result;
        }
        partial void OnDepartmentDeleted(HrApp.Server.Models.HrDB.Department item);
        partial void OnAfterDepartmentDeleted(HrApp.Server.Models.HrDB.Department item);

        [HttpDelete("/odata/HrDB/Departments(department_id={department_id})")]
        public IActionResult DeleteDepartment(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Departments
                    .Where(i => i.department_id == key)
                    .Include(i => i.Employees)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Department>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepartmentDeleted(item);
                this.context.Departments.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDepartmentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepartmentUpdated(HrApp.Server.Models.HrDB.Department item);
        partial void OnAfterDepartmentUpdated(HrApp.Server.Models.HrDB.Department item);

        [HttpPut("/odata/HrDB/Departments(department_id={department_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDepartment(int key, [FromBody]HrApp.Server.Models.HrDB.Department item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Departments
                    .Where(i => i.department_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Department>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepartmentUpdated(item);
                this.context.Departments.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Departments.Where(i => i.department_id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Location");
                this.OnAfterDepartmentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/HrDB/Departments(department_id={department_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDepartment(int key, [FromBody]Delta<HrApp.Server.Models.HrDB.Department> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Departments
                    .Where(i => i.department_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Department>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDepartmentUpdated(item);
                this.context.Departments.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Departments.Where(i => i.department_id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Location");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepartmentCreated(HrApp.Server.Models.HrDB.Department item);
        partial void OnAfterDepartmentCreated(HrApp.Server.Models.HrDB.Department item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] HrApp.Server.Models.HrDB.Department item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnDepartmentCreated(item);
                this.context.Departments.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Departments.Where(i => i.department_id == item.department_id);

                Request.QueryString = Request.QueryString.Add("$expand", "Location");

                this.OnAfterDepartmentCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
