
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace HrApp.Client
{
    public partial class HrDBService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public HrDBService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/HrDB/");
        }


        public async System.Threading.Tasks.Task ExportCountriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCountriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCountries(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Country>> GetCountries(Query query)
        {
            return await GetCountries(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Country>> GetCountries(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Countries");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCountries(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Country>>(response);
        }

        partial void OnCreateCountry(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Country> CreateCountry(HrApp.Server.Models.HrDB.Country country = default(HrApp.Server.Models.HrDB.Country))
        {
            var uri = new Uri(baseUri, $"Countries");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(country), Encoding.UTF8, "application/json");

            OnCreateCountry(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Country>(response);
        }

        partial void OnDeleteCountry(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCountry(string countryId = default(string))
        {
            var uri = new Uri(baseUri, $"Countries('{Uri.EscapeDataString(countryId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCountry(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCountryByCountryId(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Country> GetCountryByCountryId(string expand = default(string), string countryId = default(string))
        {
            var uri = new Uri(baseUri, $"Countries('{Uri.EscapeDataString(countryId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCountryByCountryId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Country>(response);
        }

        partial void OnUpdateCountry(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCountry(string countryId = default(string), HrApp.Server.Models.HrDB.Country country = default(HrApp.Server.Models.HrDB.Country))
        {
            var uri = new Uri(baseUri, $"Countries('{Uri.EscapeDataString(countryId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", country.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(country), Encoding.UTF8, "application/json");

            OnUpdateCountry(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDepartmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/departments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/departments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepartmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/departments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/departments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepartments(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Department>> GetDepartments(Query query)
        {
            return await GetDepartments(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Department>> GetDepartments(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Departments");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepartments(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Department>>(response);
        }

        partial void OnCreateDepartment(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Department> CreateDepartment(HrApp.Server.Models.HrDB.Department department = default(HrApp.Server.Models.HrDB.Department))
        {
            var uri = new Uri(baseUri, $"Departments");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(department), Encoding.UTF8, "application/json");

            OnCreateDepartment(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Department>(response);
        }

        partial void OnDeleteDepartment(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDepartment(int departmentId = default(int))
        {
            var uri = new Uri(baseUri, $"Departments({departmentId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDepartment(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepartmentByDepartmentId(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Department> GetDepartmentByDepartmentId(string expand = default(string), int departmentId = default(int))
        {
            var uri = new Uri(baseUri, $"Departments({departmentId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepartmentByDepartmentId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Department>(response);
        }

        partial void OnUpdateDepartment(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDepartment(int departmentId = default(int), HrApp.Server.Models.HrDB.Department department = default(HrApp.Server.Models.HrDB.Department))
        {
            var uri = new Uri(baseUri, $"Departments({departmentId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", department.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(department), Encoding.UTF8, "application/json");

            OnUpdateDepartment(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDependentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/dependents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/dependents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDependentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/dependents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/dependents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDependents(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Dependent>> GetDependents(Query query)
        {
            return await GetDependents(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Dependent>> GetDependents(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Dependents");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDependents(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Dependent>>(response);
        }

        partial void OnCreateDependent(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Dependent> CreateDependent(HrApp.Server.Models.HrDB.Dependent dependent = default(HrApp.Server.Models.HrDB.Dependent))
        {
            var uri = new Uri(baseUri, $"Dependents");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(dependent), Encoding.UTF8, "application/json");

            OnCreateDependent(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Dependent>(response);
        }

        partial void OnDeleteDependent(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDependent(int dependentId = default(int))
        {
            var uri = new Uri(baseUri, $"Dependents({dependentId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDependent(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDependentByDependentId(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Dependent> GetDependentByDependentId(string expand = default(string), int dependentId = default(int))
        {
            var uri = new Uri(baseUri, $"Dependents({dependentId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDependentByDependentId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Dependent>(response);
        }

        partial void OnUpdateDependent(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDependent(int dependentId = default(int), HrApp.Server.Models.HrDB.Dependent dependent = default(HrApp.Server.Models.HrDB.Dependent))
        {
            var uri = new Uri(baseUri, $"Dependents({dependentId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", dependent.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(dependent), Encoding.UTF8, "application/json");

            OnUpdateDependent(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEmployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEmployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEmployees(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Employee>> GetEmployees(Query query)
        {
            return await GetEmployees(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Employee>> GetEmployees(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Employees");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmployees(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Employee>>(response);
        }

        partial void OnCreateEmployee(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Employee> CreateEmployee(HrApp.Server.Models.HrDB.Employee employee = default(HrApp.Server.Models.HrDB.Employee))
        {
            var uri = new Uri(baseUri, $"Employees");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            OnCreateEmployee(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Employee>(response);
        }

        partial void OnDeleteEmployee(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEmployee(int employeeId = default(int))
        {
            var uri = new Uri(baseUri, $"Employees({employeeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEmployee(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEmployeeByEmployeeId(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Employee> GetEmployeeByEmployeeId(string expand = default(string), int employeeId = default(int))
        {
            var uri = new Uri(baseUri, $"Employees({employeeId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmployeeByEmployeeId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Employee>(response);
        }

        partial void OnUpdateEmployee(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEmployee(int employeeId = default(int), HrApp.Server.Models.HrDB.Employee employee = default(HrApp.Server.Models.HrDB.Employee))
        {
            var uri = new Uri(baseUri, $"Employees({employeeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", employee.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            OnUpdateEmployee(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportJobsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/jobs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/jobs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportJobsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/jobs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/jobs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetJobs(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Job>> GetJobs(Query query)
        {
            return await GetJobs(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Job>> GetJobs(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Jobs");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetJobs(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Job>>(response);
        }

        partial void OnCreateJob(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Job> CreateJob(HrApp.Server.Models.HrDB.Job job = default(HrApp.Server.Models.HrDB.Job))
        {
            var uri = new Uri(baseUri, $"Jobs");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(job), Encoding.UTF8, "application/json");

            OnCreateJob(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Job>(response);
        }

        partial void OnDeleteJob(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteJob(int jobId = default(int))
        {
            var uri = new Uri(baseUri, $"Jobs({jobId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteJob(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetJobByJobId(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Job> GetJobByJobId(string expand = default(string), int jobId = default(int))
        {
            var uri = new Uri(baseUri, $"Jobs({jobId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetJobByJobId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Job>(response);
        }

        partial void OnUpdateJob(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateJob(int jobId = default(int), HrApp.Server.Models.HrDB.Job job = default(HrApp.Server.Models.HrDB.Job))
        {
            var uri = new Uri(baseUri, $"Jobs({jobId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", job.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(job), Encoding.UTF8, "application/json");

            OnUpdateJob(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportLocationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLocationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetLocations(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Location>> GetLocations(Query query)
        {
            return await GetLocations(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Location>> GetLocations(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Locations");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLocations(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Location>>(response);
        }

        partial void OnCreateLocation(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Location> CreateLocation(HrApp.Server.Models.HrDB.Location location = default(HrApp.Server.Models.HrDB.Location))
        {
            var uri = new Uri(baseUri, $"Locations");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

            OnCreateLocation(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Location>(response);
        }

        partial void OnDeleteLocation(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteLocation(int locationId = default(int))
        {
            var uri = new Uri(baseUri, $"Locations({locationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLocation(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetLocationByLocationId(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Location> GetLocationByLocationId(string expand = default(string), int locationId = default(int))
        {
            var uri = new Uri(baseUri, $"Locations({locationId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLocationByLocationId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Location>(response);
        }

        partial void OnUpdateLocation(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateLocation(int locationId = default(int), HrApp.Server.Models.HrDB.Location location = default(HrApp.Server.Models.HrDB.Location))
        {
            var uri = new Uri(baseUri, $"Locations({locationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", location.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

            OnUpdateLocation(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Region>> GetRegions(Query query)
        {
            return await GetRegions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Region>> GetRegions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Regions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<HrApp.Server.Models.HrDB.Region>>(response);
        }

        partial void OnCreateRegion(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Region> CreateRegion(HrApp.Server.Models.HrDB.Region region = default(HrApp.Server.Models.HrDB.Region))
        {
            var uri = new Uri(baseUri, $"Regions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(region), Encoding.UTF8, "application/json");

            OnCreateRegion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Region>(response);
        }

        partial void OnDeleteRegion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegion(int regionId = default(int))
        {
            var uri = new Uri(baseUri, $"Regions({regionId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegionByRegionId(HttpRequestMessage requestMessage);

        public async Task<HrApp.Server.Models.HrDB.Region> GetRegionByRegionId(string expand = default(string), int regionId = default(int))
        {
            var uri = new Uri(baseUri, $"Regions({regionId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegionByRegionId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<HrApp.Server.Models.HrDB.Region>(response);
        }

        partial void OnUpdateRegion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegion(int regionId = default(int), HrApp.Server.Models.HrDB.Region region = default(HrApp.Server.Models.HrDB.Region))
        {
            var uri = new Uri(baseUri, $"Regions({regionId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", region.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(region), Encoding.UTF8, "application/json");

            OnUpdateRegion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}