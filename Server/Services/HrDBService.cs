using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using HrApp.Server.Data;

namespace HrApp.Server
{
    public partial class HrDBService
    {
        HrDBContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly HrDBContext context;
        private readonly NavigationManager navigationManager;

        public HrDBService(HrDBContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportCountriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCountriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCountriesRead(ref IQueryable<HrApp.Server.Models.HrDB.Country> items);

        public async Task<IQueryable<HrApp.Server.Models.HrDB.Country>> GetCountries(Query query = null)
        {
            var items = Context.Countries.AsQueryable();

            items = items.Include(i => i.Region);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCountriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCountryGet(HrApp.Server.Models.HrDB.Country item);
        partial void OnGetCountryByCountryId(ref IQueryable<HrApp.Server.Models.HrDB.Country> items);


        public async Task<HrApp.Server.Models.HrDB.Country> GetCountryByCountryId(string countryid)
        {
            var items = Context.Countries
                              .AsNoTracking()
                              .Where(i => i.country_id == countryid);

            items = items.Include(i => i.Region);
 
            OnGetCountryByCountryId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCountryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCountryCreated(HrApp.Server.Models.HrDB.Country item);
        partial void OnAfterCountryCreated(HrApp.Server.Models.HrDB.Country item);

        public async Task<HrApp.Server.Models.HrDB.Country> CreateCountry(HrApp.Server.Models.HrDB.Country country)
        {
            OnCountryCreated(country);

            var existingItem = Context.Countries
                              .Where(i => i.country_id == country.country_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Countries.Add(country);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(country).State = EntityState.Detached;
                throw;
            }

            OnAfterCountryCreated(country);

            return country;
        }

        public async Task<HrApp.Server.Models.HrDB.Country> CancelCountryChanges(HrApp.Server.Models.HrDB.Country item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCountryUpdated(HrApp.Server.Models.HrDB.Country item);
        partial void OnAfterCountryUpdated(HrApp.Server.Models.HrDB.Country item);

        public async Task<HrApp.Server.Models.HrDB.Country> UpdateCountry(string countryid, HrApp.Server.Models.HrDB.Country country)
        {
            OnCountryUpdated(country);

            var itemToUpdate = Context.Countries
                              .Where(i => i.country_id == country.country_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(country);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCountryUpdated(country);

            return country;
        }

        partial void OnCountryDeleted(HrApp.Server.Models.HrDB.Country item);
        partial void OnAfterCountryDeleted(HrApp.Server.Models.HrDB.Country item);

        public async Task<HrApp.Server.Models.HrDB.Country> DeleteCountry(string countryid)
        {
            var itemToDelete = Context.Countries
                              .Where(i => i.country_id == countryid)
                              .Include(i => i.Locations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCountryDeleted(itemToDelete);


            Context.Countries.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCountryDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDepartmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/departments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/departments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepartmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/departments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/departments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepartmentsRead(ref IQueryable<HrApp.Server.Models.HrDB.Department> items);

        public async Task<IQueryable<HrApp.Server.Models.HrDB.Department>> GetDepartments(Query query = null)
        {
            var items = Context.Departments.AsQueryable();

            items = items.Include(i => i.Location);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDepartmentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepartmentGet(HrApp.Server.Models.HrDB.Department item);
        partial void OnGetDepartmentByDepartmentId(ref IQueryable<HrApp.Server.Models.HrDB.Department> items);


        public async Task<HrApp.Server.Models.HrDB.Department> GetDepartmentByDepartmentId(int departmentid)
        {
            var items = Context.Departments
                              .AsNoTracking()
                              .Where(i => i.department_id == departmentid);

            items = items.Include(i => i.Location);
 
            OnGetDepartmentByDepartmentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepartmentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepartmentCreated(HrApp.Server.Models.HrDB.Department item);
        partial void OnAfterDepartmentCreated(HrApp.Server.Models.HrDB.Department item);

        public async Task<HrApp.Server.Models.HrDB.Department> CreateDepartment(HrApp.Server.Models.HrDB.Department department)
        {
            OnDepartmentCreated(department);

            var existingItem = Context.Departments
                              .Where(i => i.department_id == department.department_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Departments.Add(department);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(department).State = EntityState.Detached;
                throw;
            }

            OnAfterDepartmentCreated(department);

            return department;
        }

        public async Task<HrApp.Server.Models.HrDB.Department> CancelDepartmentChanges(HrApp.Server.Models.HrDB.Department item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepartmentUpdated(HrApp.Server.Models.HrDB.Department item);
        partial void OnAfterDepartmentUpdated(HrApp.Server.Models.HrDB.Department item);

        public async Task<HrApp.Server.Models.HrDB.Department> UpdateDepartment(int departmentid, HrApp.Server.Models.HrDB.Department department)
        {
            OnDepartmentUpdated(department);

            var itemToUpdate = Context.Departments
                              .Where(i => i.department_id == department.department_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(department);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepartmentUpdated(department);

            return department;
        }

        partial void OnDepartmentDeleted(HrApp.Server.Models.HrDB.Department item);
        partial void OnAfterDepartmentDeleted(HrApp.Server.Models.HrDB.Department item);

        public async Task<HrApp.Server.Models.HrDB.Department> DeleteDepartment(int departmentid)
        {
            var itemToDelete = Context.Departments
                              .Where(i => i.department_id == departmentid)
                              .Include(i => i.Employees)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepartmentDeleted(itemToDelete);


            Context.Departments.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepartmentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDependentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/dependents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/dependents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDependentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/dependents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/dependents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDependentsRead(ref IQueryable<HrApp.Server.Models.HrDB.Dependent> items);

        public async Task<IQueryable<HrApp.Server.Models.HrDB.Dependent>> GetDependents(Query query = null)
        {
            var items = Context.Dependents.AsQueryable();

            items = items.Include(i => i.Employee);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDependentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDependentGet(HrApp.Server.Models.HrDB.Dependent item);
        partial void OnGetDependentByDependentId(ref IQueryable<HrApp.Server.Models.HrDB.Dependent> items);


        public async Task<HrApp.Server.Models.HrDB.Dependent> GetDependentByDependentId(int dependentid)
        {
            var items = Context.Dependents
                              .AsNoTracking()
                              .Where(i => i.dependent_id == dependentid);

            items = items.Include(i => i.Employee);
 
            OnGetDependentByDependentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDependentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDependentCreated(HrApp.Server.Models.HrDB.Dependent item);
        partial void OnAfterDependentCreated(HrApp.Server.Models.HrDB.Dependent item);

        public async Task<HrApp.Server.Models.HrDB.Dependent> CreateDependent(HrApp.Server.Models.HrDB.Dependent dependent)
        {
            OnDependentCreated(dependent);

            var existingItem = Context.Dependents
                              .Where(i => i.dependent_id == dependent.dependent_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Dependents.Add(dependent);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(dependent).State = EntityState.Detached;
                throw;
            }

            OnAfterDependentCreated(dependent);

            return dependent;
        }

        public async Task<HrApp.Server.Models.HrDB.Dependent> CancelDependentChanges(HrApp.Server.Models.HrDB.Dependent item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDependentUpdated(HrApp.Server.Models.HrDB.Dependent item);
        partial void OnAfterDependentUpdated(HrApp.Server.Models.HrDB.Dependent item);

        public async Task<HrApp.Server.Models.HrDB.Dependent> UpdateDependent(int dependentid, HrApp.Server.Models.HrDB.Dependent dependent)
        {
            OnDependentUpdated(dependent);

            var itemToUpdate = Context.Dependents
                              .Where(i => i.dependent_id == dependent.dependent_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(dependent);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDependentUpdated(dependent);

            return dependent;
        }

        partial void OnDependentDeleted(HrApp.Server.Models.HrDB.Dependent item);
        partial void OnAfterDependentDeleted(HrApp.Server.Models.HrDB.Dependent item);

        public async Task<HrApp.Server.Models.HrDB.Dependent> DeleteDependent(int dependentid)
        {
            var itemToDelete = Context.Dependents
                              .Where(i => i.dependent_id == dependentid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDependentDeleted(itemToDelete);


            Context.Dependents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDependentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEmployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmployeesRead(ref IQueryable<HrApp.Server.Models.HrDB.Employee> items);

        public async Task<IQueryable<HrApp.Server.Models.HrDB.Employee>> GetEmployees(Query query = null)
        {
            var items = Context.Employees.AsQueryable();

            items = items.Include(i => i.Department);
            items = items.Include(i => i.Job);
            items = items.Include(i => i.Employee1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEmployeesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmployeeGet(HrApp.Server.Models.HrDB.Employee item);
        partial void OnGetEmployeeByEmployeeId(ref IQueryable<HrApp.Server.Models.HrDB.Employee> items);


        public async Task<HrApp.Server.Models.HrDB.Employee> GetEmployeeByEmployeeId(int employeeid)
        {
            var items = Context.Employees
                              .AsNoTracking()
                              .Where(i => i.employee_id == employeeid);

            items = items.Include(i => i.Department);
            items = items.Include(i => i.Job);
            items = items.Include(i => i.Employee1);
 
            OnGetEmployeeByEmployeeId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmployeeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmployeeCreated(HrApp.Server.Models.HrDB.Employee item);
        partial void OnAfterEmployeeCreated(HrApp.Server.Models.HrDB.Employee item);

        public async Task<HrApp.Server.Models.HrDB.Employee> CreateEmployee(HrApp.Server.Models.HrDB.Employee employee)
        {
            OnEmployeeCreated(employee);

            var existingItem = Context.Employees
                              .Where(i => i.employee_id == employee.employee_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Employees.Add(employee);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(employee).State = EntityState.Detached;
                throw;
            }

            OnAfterEmployeeCreated(employee);

            return employee;
        }

        public async Task<HrApp.Server.Models.HrDB.Employee> CancelEmployeeChanges(HrApp.Server.Models.HrDB.Employee item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmployeeUpdated(HrApp.Server.Models.HrDB.Employee item);
        partial void OnAfterEmployeeUpdated(HrApp.Server.Models.HrDB.Employee item);

        public async Task<HrApp.Server.Models.HrDB.Employee> UpdateEmployee(int employeeid, HrApp.Server.Models.HrDB.Employee employee)
        {
            OnEmployeeUpdated(employee);

            var itemToUpdate = Context.Employees
                              .Where(i => i.employee_id == employee.employee_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(employee);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmployeeUpdated(employee);

            return employee;
        }

        partial void OnEmployeeDeleted(HrApp.Server.Models.HrDB.Employee item);
        partial void OnAfterEmployeeDeleted(HrApp.Server.Models.HrDB.Employee item);

        public async Task<HrApp.Server.Models.HrDB.Employee> DeleteEmployee(int employeeid)
        {
            var itemToDelete = Context.Employees
                              .Where(i => i.employee_id == employeeid)
                              .Include(i => i.Dependents)
                              .Include(i => i.Employees1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmployeeDeleted(itemToDelete);


            Context.Employees.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmployeeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportJobsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/jobs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/jobs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportJobsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/jobs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/jobs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnJobsRead(ref IQueryable<HrApp.Server.Models.HrDB.Job> items);

        public async Task<IQueryable<HrApp.Server.Models.HrDB.Job>> GetJobs(Query query = null)
        {
            var items = Context.Jobs.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnJobsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnJobGet(HrApp.Server.Models.HrDB.Job item);
        partial void OnGetJobByJobId(ref IQueryable<HrApp.Server.Models.HrDB.Job> items);


        public async Task<HrApp.Server.Models.HrDB.Job> GetJobByJobId(int jobid)
        {
            var items = Context.Jobs
                              .AsNoTracking()
                              .Where(i => i.job_id == jobid);

 
            OnGetJobByJobId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnJobGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnJobCreated(HrApp.Server.Models.HrDB.Job item);
        partial void OnAfterJobCreated(HrApp.Server.Models.HrDB.Job item);

        public async Task<HrApp.Server.Models.HrDB.Job> CreateJob(HrApp.Server.Models.HrDB.Job job)
        {
            OnJobCreated(job);

            var existingItem = Context.Jobs
                              .Where(i => i.job_id == job.job_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Jobs.Add(job);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(job).State = EntityState.Detached;
                throw;
            }

            OnAfterJobCreated(job);

            return job;
        }

        public async Task<HrApp.Server.Models.HrDB.Job> CancelJobChanges(HrApp.Server.Models.HrDB.Job item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnJobUpdated(HrApp.Server.Models.HrDB.Job item);
        partial void OnAfterJobUpdated(HrApp.Server.Models.HrDB.Job item);

        public async Task<HrApp.Server.Models.HrDB.Job> UpdateJob(int jobid, HrApp.Server.Models.HrDB.Job job)
        {
            OnJobUpdated(job);

            var itemToUpdate = Context.Jobs
                              .Where(i => i.job_id == job.job_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(job);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterJobUpdated(job);

            return job;
        }

        partial void OnJobDeleted(HrApp.Server.Models.HrDB.Job item);
        partial void OnAfterJobDeleted(HrApp.Server.Models.HrDB.Job item);

        public async Task<HrApp.Server.Models.HrDB.Job> DeleteJob(int jobid)
        {
            var itemToDelete = Context.Jobs
                              .Where(i => i.job_id == jobid)
                              .Include(i => i.Employees)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnJobDeleted(itemToDelete);


            Context.Jobs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterJobDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportLocationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLocationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLocationsRead(ref IQueryable<HrApp.Server.Models.HrDB.Location> items);

        public async Task<IQueryable<HrApp.Server.Models.HrDB.Location>> GetLocations(Query query = null)
        {
            var items = Context.Locations.AsQueryable();

            items = items.Include(i => i.Country);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLocationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLocationGet(HrApp.Server.Models.HrDB.Location item);
        partial void OnGetLocationByLocationId(ref IQueryable<HrApp.Server.Models.HrDB.Location> items);


        public async Task<HrApp.Server.Models.HrDB.Location> GetLocationByLocationId(int locationid)
        {
            var items = Context.Locations
                              .AsNoTracking()
                              .Where(i => i.location_id == locationid);

            items = items.Include(i => i.Country);
 
            OnGetLocationByLocationId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLocationGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLocationCreated(HrApp.Server.Models.HrDB.Location item);
        partial void OnAfterLocationCreated(HrApp.Server.Models.HrDB.Location item);

        public async Task<HrApp.Server.Models.HrDB.Location> CreateLocation(HrApp.Server.Models.HrDB.Location location)
        {
            OnLocationCreated(location);

            var existingItem = Context.Locations
                              .Where(i => i.location_id == location.location_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Locations.Add(location);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(location).State = EntityState.Detached;
                throw;
            }

            OnAfterLocationCreated(location);

            return location;
        }

        public async Task<HrApp.Server.Models.HrDB.Location> CancelLocationChanges(HrApp.Server.Models.HrDB.Location item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLocationUpdated(HrApp.Server.Models.HrDB.Location item);
        partial void OnAfterLocationUpdated(HrApp.Server.Models.HrDB.Location item);

        public async Task<HrApp.Server.Models.HrDB.Location> UpdateLocation(int locationid, HrApp.Server.Models.HrDB.Location location)
        {
            OnLocationUpdated(location);

            var itemToUpdate = Context.Locations
                              .Where(i => i.location_id == location.location_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(location);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLocationUpdated(location);

            return location;
        }

        partial void OnLocationDeleted(HrApp.Server.Models.HrDB.Location item);
        partial void OnAfterLocationDeleted(HrApp.Server.Models.HrDB.Location item);

        public async Task<HrApp.Server.Models.HrDB.Location> DeleteLocation(int locationid)
        {
            var itemToDelete = Context.Locations
                              .Where(i => i.location_id == locationid)
                              .Include(i => i.Departments)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLocationDeleted(itemToDelete);


            Context.Locations.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLocationDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/hrdb/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/hrdb/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegionsRead(ref IQueryable<HrApp.Server.Models.HrDB.Region> items);

        public async Task<IQueryable<HrApp.Server.Models.HrDB.Region>> GetRegions(Query query = null)
        {
            var items = Context.Regions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnRegionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegionGet(HrApp.Server.Models.HrDB.Region item);
        partial void OnGetRegionByRegionId(ref IQueryable<HrApp.Server.Models.HrDB.Region> items);


        public async Task<HrApp.Server.Models.HrDB.Region> GetRegionByRegionId(int regionid)
        {
            var items = Context.Regions
                              .AsNoTracking()
                              .Where(i => i.region_id == regionid);

 
            OnGetRegionByRegionId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegionCreated(HrApp.Server.Models.HrDB.Region item);
        partial void OnAfterRegionCreated(HrApp.Server.Models.HrDB.Region item);

        public async Task<HrApp.Server.Models.HrDB.Region> CreateRegion(HrApp.Server.Models.HrDB.Region region)
        {
            OnRegionCreated(region);

            var existingItem = Context.Regions
                              .Where(i => i.region_id == region.region_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Regions.Add(region);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(region).State = EntityState.Detached;
                throw;
            }

            OnAfterRegionCreated(region);

            return region;
        }

        public async Task<HrApp.Server.Models.HrDB.Region> CancelRegionChanges(HrApp.Server.Models.HrDB.Region item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegionUpdated(HrApp.Server.Models.HrDB.Region item);
        partial void OnAfterRegionUpdated(HrApp.Server.Models.HrDB.Region item);

        public async Task<HrApp.Server.Models.HrDB.Region> UpdateRegion(int regionid, HrApp.Server.Models.HrDB.Region region)
        {
            OnRegionUpdated(region);

            var itemToUpdate = Context.Regions
                              .Where(i => i.region_id == region.region_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(region);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegionUpdated(region);

            return region;
        }

        partial void OnRegionDeleted(HrApp.Server.Models.HrDB.Region item);
        partial void OnAfterRegionDeleted(HrApp.Server.Models.HrDB.Region item);

        public async Task<HrApp.Server.Models.HrDB.Region> DeleteRegion(int regionid)
        {
            var itemToDelete = Context.Regions
                              .Where(i => i.region_id == regionid)
                              .Include(i => i.Countries)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegionDeleted(itemToDelete);


            Context.Regions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegionDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}