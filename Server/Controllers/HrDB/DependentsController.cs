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
    [Route("odata/HrDB/Dependents")]
    public partial class DependentsController : ODataController
    {
        private HrApp.Server.Data.HrDBContext context;

        public DependentsController(HrApp.Server.Data.HrDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<HrApp.Server.Models.HrDB.Dependent> GetDependents()
        {
            var items = this.context.Dependents.AsQueryable<HrApp.Server.Models.HrDB.Dependent>();
            this.OnDependentsRead(ref items);

            return items;
        }

        partial void OnDependentsRead(ref IQueryable<HrApp.Server.Models.HrDB.Dependent> items);

        partial void OnDependentGet(ref SingleResult<HrApp.Server.Models.HrDB.Dependent> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/HrDB/Dependents(dependent_id={dependent_id})")]
        public SingleResult<HrApp.Server.Models.HrDB.Dependent> GetDependent(int key)
        {
            var items = this.context.Dependents.Where(i => i.dependent_id == key);
            var result = SingleResult.Create(items);

            OnDependentGet(ref result);

            return result;
        }
        partial void OnDependentDeleted(HrApp.Server.Models.HrDB.Dependent item);
        partial void OnAfterDependentDeleted(HrApp.Server.Models.HrDB.Dependent item);

        [HttpDelete("/odata/HrDB/Dependents(dependent_id={dependent_id})")]
        public IActionResult DeleteDependent(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Dependents
                    .Where(i => i.dependent_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Dependent>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDependentDeleted(item);
                this.context.Dependents.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDependentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDependentUpdated(HrApp.Server.Models.HrDB.Dependent item);
        partial void OnAfterDependentUpdated(HrApp.Server.Models.HrDB.Dependent item);

        [HttpPut("/odata/HrDB/Dependents(dependent_id={dependent_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDependent(int key, [FromBody]HrApp.Server.Models.HrDB.Dependent item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Dependents
                    .Where(i => i.dependent_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Dependent>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDependentUpdated(item);
                this.context.Dependents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Dependents.Where(i => i.dependent_id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Employee");
                this.OnAfterDependentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/HrDB/Dependents(dependent_id={dependent_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDependent(int key, [FromBody]Delta<HrApp.Server.Models.HrDB.Dependent> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Dependents
                    .Where(i => i.dependent_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Dependent>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDependentUpdated(item);
                this.context.Dependents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Dependents.Where(i => i.dependent_id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Employee");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDependentCreated(HrApp.Server.Models.HrDB.Dependent item);
        partial void OnAfterDependentCreated(HrApp.Server.Models.HrDB.Dependent item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] HrApp.Server.Models.HrDB.Dependent item)
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

                this.OnDependentCreated(item);
                this.context.Dependents.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Dependents.Where(i => i.dependent_id == item.dependent_id);

                Request.QueryString = Request.QueryString.Add("$expand", "Employee");

                this.OnAfterDependentCreated(item);

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
