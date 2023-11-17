using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HrApp.Server.Models.HrDB
{
    [Table("jobs", Schema = "dbo")]
    public partial class Job
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
        public int job_id { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string job_title { get; set; }

        [ConcurrencyCheck]
        public decimal? min_salary { get; set; }

        [ConcurrencyCheck]
        public decimal? max_salary { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}