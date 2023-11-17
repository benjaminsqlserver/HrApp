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
    [Route("odata/HrDB/Countries")]
    public partial class CountriesController : ODataController
    {
        private HrApp.Server.Data.HrDBContext context;

        public CountriesController(HrApp.Server.Data.HrDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<HrApp.Server.Models.HrDB.Country> GetCountries()
        {
            var items = this.context.Countries.AsQueryable<HrApp.Server.Models.HrDB.Country>();
            this.OnCountriesRead(ref items);

            return items;
        }

        partial void OnCountriesRead(ref IQueryable<HrApp.Server.Models.HrDB.Country> items);

        partial void OnCountryGet(ref SingleResult<HrApp.Server.Models.HrDB.Country> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/HrDB/Countries(country_id={country_id})")]
        public SingleResult<HrApp.Server.Models.HrDB.Country> GetCountry(string key)
        {
            var items = this.context.Countries.Where(i => i.country_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnCountryGet(ref result);

            return result;
        }
        partial void OnCountryDeleted(HrApp.Server.Models.HrDB.Country item);
        partial void OnAfterCountryDeleted(HrApp.Server.Models.HrDB.Country item);

        [HttpDelete("/odata/HrDB/Countries(country_id={country_id})")]
        public IActionResult DeleteCountry(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Countries
                    .Where(i => i.country_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Locations)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Country>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCountryDeleted(item);
                this.context.Countries.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCountryDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCountryUpdated(HrApp.Server.Models.HrDB.Country item);
        partial void OnAfterCountryUpdated(HrApp.Server.Models.HrDB.Country item);

        [HttpPut("/odata/HrDB/Countries(country_id={country_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCountry(string key, [FromBody]HrApp.Server.Models.HrDB.Country item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Countries
                    .Where(i => i.country_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Country>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCountryUpdated(item);
                this.context.Countries.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Countries.Where(i => i.country_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "Region");
                this.OnAfterCountryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/HrDB/Countries(country_id={country_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCountry(string key, [FromBody]Delta<HrApp.Server.Models.HrDB.Country> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Countries
                    .Where(i => i.country_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<HrApp.Server.Models.HrDB.Country>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCountryUpdated(item);
                this.context.Countries.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Countries.Where(i => i.country_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "Region");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCountryCreated(HrApp.Server.Models.HrDB.Country item);
        partial void OnAfterCountryCreated(HrApp.Server.Models.HrDB.Country item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] HrApp.Server.Models.HrDB.Country item)
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

                this.OnCountryCreated(item);
                this.context.Countries.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Countries.Where(i => i.country_id == item.country_id);

                Request.QueryString = Request.QueryString.Add("$expand", "Region");

                this.OnAfterCountryCreated(item);

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
