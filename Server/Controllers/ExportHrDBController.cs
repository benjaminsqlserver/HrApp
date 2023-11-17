using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using HrApp.Server.Data;

namespace HrApp.Server.Controllers
{
    public partial class ExportHrDBController : ExportController
    {
        private readonly HrDBContext context;
        private readonly HrDBService service;

        public ExportHrDBController(HrDBContext context, HrDBService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/HrDB/countries/csv")]
        [HttpGet("/export/HrDB/countries/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCountriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCountries(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/countries/excel")]
        [HttpGet("/export/HrDB/countries/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCountriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCountries(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/departments/csv")]
        [HttpGet("/export/HrDB/departments/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepartmentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepartments(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/departments/excel")]
        [HttpGet("/export/HrDB/departments/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepartmentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepartments(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/dependents/csv")]
        [HttpGet("/export/HrDB/dependents/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDependentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDependents(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/dependents/excel")]
        [HttpGet("/export/HrDB/dependents/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDependentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDependents(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/employees/csv")]
        [HttpGet("/export/HrDB/employees/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmployees(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/employees/excel")]
        [HttpGet("/export/HrDB/employees/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmployees(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/jobs/csv")]
        [HttpGet("/export/HrDB/jobs/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportJobsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetJobs(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/jobs/excel")]
        [HttpGet("/export/HrDB/jobs/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportJobsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetJobs(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/locations/csv")]
        [HttpGet("/export/HrDB/locations/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLocationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLocations(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/locations/excel")]
        [HttpGet("/export/HrDB/locations/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLocationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLocations(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/regions/csv")]
        [HttpGet("/export/HrDB/regions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegions(), Request.Query), fileName);
        }

        [HttpGet("/export/HrDB/regions/excel")]
        [HttpGet("/export/HrDB/regions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegions(), Request.Query), fileName);
        }
    }
}
