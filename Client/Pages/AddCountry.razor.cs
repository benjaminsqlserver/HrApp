using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace HrApp.Client.Pages
{
    public partial class AddCountry
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public HrDBService HrDBService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            country = new HrApp.Server.Models.HrDB.Country();
        }
        protected bool errorVisible;
        protected HrApp.Server.Models.HrDB.Country country;

        protected IEnumerable<HrApp.Server.Models.HrDB.Region> regionsForregionId;


        protected int regionsForregionIdCount;
        protected HrApp.Server.Models.HrDB.Region regionsForregionIdValue;
        protected async Task regionsForregionIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await HrDBService.GetRegions(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(region_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                regionsForregionId = result.Value.AsODataEnumerable();
                regionsForregionIdCount = result.Count;

                if (!object.Equals(country.region_id, null))
                {
                    var valueResult = await HrDBService.GetRegions(filter: $"region_id eq {country.region_id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        regionsForregionIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Region" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await HrDBService.CreateCountry(country);
                DialogService.Close(country);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;

        [Inject]
        protected SecurityService Security { get; set; }
    }
}