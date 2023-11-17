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
    public partial class EditEmployee
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
        public int employee_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            employee = await HrDBService.GetEmployeeByEmployeeId(employeeId:employee_id);
        }
        protected bool errorVisible;
        protected HrApp.Server.Models.HrDB.Employee employee;

        protected IEnumerable<HrApp.Server.Models.HrDB.Department> departmentsFordepartmentId;

        protected IEnumerable<HrApp.Server.Models.HrDB.Job> jobsForjobId;

        protected IEnumerable<HrApp.Server.Models.HrDB.Employee> employeesFormanagerId;


        protected int departmentsFordepartmentIdCount;
        protected HrApp.Server.Models.HrDB.Department departmentsFordepartmentIdValue;
        protected async Task departmentsFordepartmentIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await HrDBService.GetDepartments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(department_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                departmentsFordepartmentId = result.Value.AsODataEnumerable();
                departmentsFordepartmentIdCount = result.Count;

                if (!object.Equals(employee.department_id, null))
                {
                    var valueResult = await HrDBService.GetDepartments(filter: $"department_id eq {employee.department_id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        departmentsFordepartmentIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Department" });
            }
        }

        protected int jobsForjobIdCount;
        protected HrApp.Server.Models.HrDB.Job jobsForjobIdValue;
        protected async Task jobsForjobIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await HrDBService.GetJobs(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(job_title, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                jobsForjobId = result.Value.AsODataEnumerable();
                jobsForjobIdCount = result.Count;

                if (!object.Equals(employee.job_id, null))
                {
                    var valueResult = await HrDBService.GetJobs(filter: $"job_id eq {employee.job_id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        jobsForjobIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Job" });
            }
        }

        protected int employeesFormanagerIdCount;
        protected HrApp.Server.Models.HrDB.Employee employeesFormanagerIdValue;
        protected async Task employeesFormanagerIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await HrDBService.GetEmployees(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(first_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                employeesFormanagerId = result.Value.AsODataEnumerable();
                employeesFormanagerIdCount = result.Count;

                if (!object.Equals(employee.manager_id, null))
                {
                    var valueResult = await HrDBService.GetEmployees(filter: $"employee_id eq {employee.manager_id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        employeesFormanagerIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Employee1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await HrDBService.UpdateEmployee(employeeId:employee_id, employee);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(employee);
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

            employee = await HrDBService.GetEmployeeByEmployeeId(employeeId:employee_id);
        }
    }
}