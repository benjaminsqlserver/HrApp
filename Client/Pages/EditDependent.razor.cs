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
    public partial class EditDependent
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
        public int dependent_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            dependent = await HrDBService.GetDependentByDependentId(dependentId:dependent_id);
        }
        protected bool errorVisible;
        protected HrApp.Server.Models.HrDB.Dependent dependent;

        protected IEnumerable<HrApp.Server.Models.HrDB.Employee> employeesForemployeeId;


        protected int employeesForemployeeIdCount;
        protected HrApp.Server.Models.HrDB.Employee employeesForemployeeIdValue;
        protected async Task employeesForemployeeIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await HrDBService.GetEmployees(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(first_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                employeesForemployeeId = result.Value.AsODataEnumerable();
                employeesForemployeeIdCount = result.Count;

                if (!object.Equals(dependent.employee_id, null))
                {
                    var valueResult = await HrDBService.GetEmployees(filter: $"employee_id eq {dependent.employee_id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        employeesForemployeeIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Employee" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await HrDBService.UpdateDependent(dependentId:dependent_id, dependent);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(dependent);
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

            dependent = await HrDBService.GetDependentByDependentId(dependentId:dependent_id);
        }
    }
}