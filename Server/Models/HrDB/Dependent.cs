using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HrApp.Server.Models.HrDB
{
    [Table("dependents", Schema = "dbo")]
    public partial class Dependent
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
        public int dependent_id { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string first_name { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string last_name { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string relationship { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int employee_id { get; set; }

        public Employee Employee { get; set; }

    }
}