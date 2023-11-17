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
    [Route("odata/HrDB/Locations")]
    public partial class LocationsController : ODataController
    {
        private HrApp.Server.Data.HrDBContext context;

        public LocationsController(HrApp.Server.Data.HrDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<HrApp.Server.Models.HrDB.Location> GetLocations()
        {
            var items = this.context.Locations.AsQueryable<HrApp.Server.Models.HrDB.Location>();
            this.OnLocationsRead(ref items);

            return items;
        }

        partial void OnLocationsRead(ref IQueryable<HrApp.Server.Models.HrDB.Location> items);

        partial void OnLocationGet(ref SingleResult<HrApp.Server.Models.HrDB.Location> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/HrDB/Locations(location_id={location_id})")]
        public SingleResult<HrApp.Server.Models.HrDB.Location> GetLocation(int key)
        {
            var items = this.context.Locations.Where(i => i.location_id == key);
            var result = SingleResult.Create(items);

            OnLocationGet(ref result);

            return result;
        }
        partial void OnLocationDeleted(HrApp.Server.Models.HrDB.Location item);
        partial void OnAfterLocationDeleted(HrApp.Server.Models.HrDB.Location item);

        [HttpDelete("/odata/HrDB/Locations(location_id={location_id})")]
        public IActionResult DeleteLocation(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Locations
                    .Where(i => i.location_id == key)
                    .Include(i => i.Departments)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Location>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLocationDeleted(item);
                this.context.Locations.Remove(item);
                this.context.SaveChanges();
                this.OnAfterLocationDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLocationUpdated(HrApp.Server.Models.HrDB.Location item);
        partial void OnAfterLocationUpdated(HrApp.Server.Models.HrDB.Location item);

        [HttpPut("/odata/HrDB/Locations(location_id={location_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutLocation(int key, [FromBody]HrApp.Server.Models.HrDB.Location item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Locations
                    .Where(i => i.location_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Location>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLocationUpdated(item);
                this.context.Locations.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Locations.Where(i => i.location_id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Country");
                this.OnAfterLocationUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/HrDB/Locations(location_id={location_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchLocation(int key, [FromBody]Delta<HrApp.Server.Models.HrDB.Location> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Locations
                    .Where(i => i.location_id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Location>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnLocationUpdated(item);
                this.context.Locations.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Locations.Where(i => i.location_id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Country");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLocationCreated(HrApp.Server.Models.HrDB.Location item);
        partial void OnAfterLocationCreated(HrApp.Server.Models.HrDB.Location item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] HrApp.Server.Models.HrDB.Location item)
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

                this.OnLocationCreated(item);
                this.context.Locations.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Locations.Where(i => i.location_id == item.location_id);

                Request.QueryString = Request.QueryString.Add("$expand", "Country");

                this.OnAfterLocationCreated(item);

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
