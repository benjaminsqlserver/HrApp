using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HrApp.Server.Models.HrDB
{
    [Table("employees", Schema = "dbo")]
    public partial class Employee
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employee_id { get; set; }

        [ConcurrencyCheck]
        public string first_name { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string last_name { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string email { get; set; }

        [ConcurrencyCheck]
        public string phone_number { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime hire_date { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int job_id { get; set; }

        [Required]
        [ConcurrencyCheck]
        public decimal salary { get; set; }

        [ConcurrencyCheck]
        public int? manager_id { get; set; }

        [ConcurrencyCheck]
        public int? department_id { get; set; }

        public ICollection<Dependent> Dependents { get; set; }

        public Department Department { get; set; }

        public Job Job { get; set; }

        public Employee Employee1 { get; set; }

        public ICollection<Employee> Employees1 { get; set; }

    }
}