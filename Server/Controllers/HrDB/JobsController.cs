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
    [Route("odata/HrDB/Jobs")]
    public partial class JobsController : ODataController
    {
        private HrApp.Server.Data.HrDBContext context;

        public JobsController(HrApp.Server.Data.HrDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<HrApp.Server.Models.HrDB.Job> GetJobs()
        {
            var items = this.context.Jobs.AsQueryable<HrApp.Server.Models.HrDB.Job>();
            this.OnJobsRead(ref items);

            return items;
        }

        partial void OnJobsRead(ref IQueryable<HrApp.Server.Models.HrDB.Job> items);

        partial void OnJobGet(ref SingleResult<HrApp.Server.Models.HrDB.Job> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/HrDB/Jobs(job_id={job_id})")]
        public SingleResult<HrApp.Server.Models.HrDB.Job> GetJob(int key)
        {
            var items = this.context.Jobs.Where(i => i.job_id == key);
            var result = SingleResult.Create(items);

            OnJobGet(ref result);

            return result;
        }
        partial void OnJobDeleted(HrApp.Server.Models.HrDB.Job item);
        partial void OnAfterJobDeleted(HrApp.Server.Models.HrDB.Job item);

        [HttpDelete("/odata/HrDB/Jobs(job_id={job_id})")]
        public IActionResult DeleteJob(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Jobs
                    .Where(i => i.job_id == key)
                    .Include(i => i.Employees)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Job>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnJobDeleted(item);
                this.context.Jobs.Remove(item);
                this.context.SaveChanges();
                this.OnAfterJobDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnJobUpdated(HrApp.Server.Models.HrDB.Job item);
        partial void OnAfterJobUpdated(HrApp.Server.Models.HrDB.Job item);

        [HttpPut("/odata/HrDB/Jobs(job_id={job_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutJob(int key, [FromBody]HrApp.Server.Models.HrDB.Job item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Jobs
                    .Where(i => i.job_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Job>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnJobUpdated(item);
                this.context.Jobs.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Jobs.Where(i => i.job_id == key);
                
                this.OnAfterJobUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/HrDB/Jobs(job_id={job_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchJob(int key, [FromBody]Delta<HrApp.Server.Models.HrDB.Job> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Jobs
                    .Where(i => i.job_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Job>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnJobUpdated(item);
                this.context.Jobs.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Jobs.Where(i => i.job_id == key);
                
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnJobCreated(HrApp.Server.Models.HrDB.Job item);
        partial void OnAfterJobCreated(HrApp.Server.Models.HrDB.Job item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] HrApp.Server.Models.HrDB.Job item)
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

                this.OnJobCreated(item);
                this.context.Jobs.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Jobs.Where(i => i.job_id == item.job_id);

                

                this.OnAfterJobCreated(item);

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
