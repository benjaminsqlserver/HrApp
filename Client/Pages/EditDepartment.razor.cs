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
    public partial class EditDepartment
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

        [Parameter]
        public int department_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            department = await HrDBService.GetDepartmentByDepartmentId(departmentId:department_id);
        }
        protected bool errorVisible;
        protected HrApp.Server.Models.HrDB.Department department;

        protected IEnumerable<HrApp.Server.Models.HrDB.Location> locationsForlocationId;


        protected int locationsForlocationIdCount;
        protected HrApp.Server.Models.HrDB.Location locationsForlocationIdValue;
        protected async Task locationsForlocationIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await HrDBService.GetLocations(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(street_address, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                locationsForlocationId = result.Value.AsODataEnumerable();
                locationsForlocationIdCount = result.Count;

                if (!object.Equals(department.location_id, null))
                {
                    var valueResult = await HrDBService.GetLocations(filter: $"location_id eq {department.location_id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        locationsForlocationIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Location" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await HrDBService.UpdateDepartment(departmentId:department_id, department);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(department);
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


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            department = await HrDBService.GetDepartmentByDepartmentId(departmentId:department_id);
        }
    }
}