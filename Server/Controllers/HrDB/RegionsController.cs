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
    [Route("odata/HrDB/Regions")]
    public partial class RegionsController : ODataController
    {
        private HrApp.Server.Data.HrDBContext context;

        public RegionsController(HrApp.Server.Data.HrDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<HrApp.Server.Models.HrDB.Region> GetRegions()
        {
            var items = this.context.Regions.AsQueryable<HrApp.Server.Models.HrDB.Region>();
            this.OnRegionsRead(ref items);

            return items;
        }

        partial void OnRegionsRead(ref IQueryable<HrApp.Server.Models.HrDB.Region> items);

        partial void OnRegionGet(ref SingleResult<HrApp.Server.Models.HrDB.Region> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/HrDB/Regions(region_id={region_id})")]
        public SingleResult<HrApp.Server.Models.HrDB.Region> GetRegion(int key)
        {
            var items = this.context.Regions.Where(i => i.region_id == key);
            var result = SingleResult.Create(items);

            OnRegionGet(ref result);

            return result;
        }
        partial void OnRegionDeleted(HrApp.Server.Models.HrDB.Region item);
        partial void OnAfterRegionDeleted(HrApp.Server.Models.HrDB.Region item);

        [HttpDelete("/odata/HrDB/Regions(region_id={region_id})")]
        public IActionResult DeleteRegion(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Regions
                    .Where(i => i.region_id == key)
                    .Include(i => i.Countries)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Region>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegionDeleted(item);
                this.context.Regions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRegionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegionUpdated(HrApp.Server.Models.HrDB.Region item);
        partial void OnAfterRegionUpdated(HrApp.Server.Models.HrDB.Region item);

        [HttpPut("/odata/HrDB/Regions(region_id={region_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRegion(int key, [FromBody]HrApp.Server.Models.HrDB.Region item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Regions
                    .Where(i => i.region_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Region>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegionUpdated(item);
                this.context.Regions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Regions.Where(i => i.region_id == key);
                
                this.OnAfterRegionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/HrDB/Regions(region_id={region_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRegion(int key, [FromBody]Delta<HrApp.Server.Models.HrDB.Region> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Regions
                    .Where(i => i.region_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Region>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRegionUpdated(item);
                this.context.Regions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Regions.Where(i => i.region_id == key);
                
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegionCreated(HrApp.Server.Models.HrDB.Region item);
        partial void OnAfterRegionCreated(HrApp.Server.Models.HrDB.Region item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] HrApp.Server.Models.HrDB.Region item)
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

                this.OnRegionCreated(item);
                this.context.Regions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Regions.Where(i => i.region_id == item.region_id);

                

                this.OnAfterRegionCreated(item);

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
