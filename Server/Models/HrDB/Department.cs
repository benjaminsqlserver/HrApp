using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HrApp.Server.Models.HrDB
{
    [Table("departments", Schema = "dbo")]
    public partial class Department
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
        public int department_id { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string department_name { get; set; }

        [ConcurrencyCheck]
        public int? location_id { get; set; }

        public Location Location { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}