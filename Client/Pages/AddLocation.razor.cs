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
    public partial class AddLocation
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
            location = new HrApp.Server.Models.HrDB.Location();
        }
        protected bool errorVisible;
        protected HrApp.Server.Models.HrDB.Location location;

        protected IEnumerable<HrApp.Server.Models.HrDB.Country> countriesForcountryId;


        protected int countriesForcountryIdCount;
        protected HrApp.Server.Models.HrDB.Country countriesForcountryIdValue;
        protected async Task countriesForcountryIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await HrDBService.GetCountries(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(country_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                countriesForcountryId = result.Value.AsODataEnumerable();
                countriesForcountryIdCount = result.Count;

                if (!object.Equals(location.country_id, null))
                {
                    var valueResult = await HrDBService.GetCountries(filter: $"country_id eq '{location.country_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        countriesForcountryIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Country" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await HrDBService.CreateLocation(location);
                DialogService.Close(location);
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