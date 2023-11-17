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
    [Route("odata/HrDB/Employees")]
    public partial class EmployeesController : ODataController
    {
        private HrApp.Server.Data.HrDBContext context;

        public EmployeesController(HrApp.Server.Data.HrDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<HrApp.Server.Models.HrDB.Employee> GetEmployees()
        {
            var items = this.context.Employees.AsQueryable<HrApp.Server.Models.HrDB.Employee>();
            this.OnEmployeesRead(ref items);

            return items;
        }

        partial void OnEmployeesRead(ref IQueryable<HrApp.Server.Models.HrDB.Employee> items);

        partial void OnEmployeeGet(ref SingleResult<HrApp.Server.Models.HrDB.Employee> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/HrDB/Employees(employee_id={employee_id})")]
        public SingleResult<HrApp.Server.Models.HrDB.Employee> GetEmployee(int key)
        {
            var items = this.context.Employees.Where(i => i.employee_id == key);
            var result = SingleResult.Create(items);

            OnEmployeeGet(ref result);

            return result;
        }
        partial void OnEmployeeDeleted(HrApp.Server.Models.HrDB.Employee item);
        partial void OnAfterEmployeeDeleted(HrApp.Server.Models.HrDB.Employee item);

        [HttpDelete("/odata/HrDB/Employees(employee_id={employee_id})")]
        public IActionResult DeleteEmployee(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Employees
                    .Where(i => i.employee_id == key)
                    .Include(i => i.Dependents)
                    .Include(i => i.Employees1)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Employee>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmployeeDeleted(item);
                this.context.Employees.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEmployeeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmployeeUpdated(HrApp.Server.Models.HrDB.Employee item);
        partial void OnAfterEmployeeUpdated(HrApp.Server.Models.HrDB.Employee item);

        [HttpPut("/odata/HrDB/Employees(employee_id={employee_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEmployee(int key, [FromBody]HrApp.Server.Models.HrDB.Employee item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Employees
                    .Where(i => i.employee_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Employee>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmployeeUpdated(item);
                this.context.Employees.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Employees.Where(i => i.employee_id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Department,Job,Employee1");
                this.OnAfterEmployeeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/HrDB/Employees(employee_id={employee_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEmployee(int key, [FromBody]Delta<HrApp.Server.Models.HrDB.Employee> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Employees
                    .Where(i => i.employee_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Employee>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEmployeeUpdated(item);
                this.context.Employees.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Employees.Where(i => i.employee_id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Department,Job,Employee1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmployeeCreated(HrApp.Server.Models.HrDB.Employee item);
        partial void OnAfterEmployeeCreated(HrApp.Server.Models.HrDB.Employee item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] HrApp.Server.Models.HrDB.Employee item)
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

                this.OnEmployeeCreated(item);
                this.context.Employees.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Employees.Where(i => i.employee_id == item.employee_id);

                Request.QueryString = Request.QueryString.Add("$expand", "Department,Job,Employee1");

                this.OnAfterEmployeeCreated(item);

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
