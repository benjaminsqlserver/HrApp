using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HrApp.Server.Models.HrDB
{
    [Table("locations", Schema = "dbo")]
    public partial class Location
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
        public int location_id { get; set; }

        [ConcurrencyCheck]
        public string street_address { get; set; }

        [ConcurrencyCheck]
        public string postal_code { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string city { get; set; }

        [ConcurrencyCheck]
        public string state_province { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string country_id { get; set; }

        public ICollection<Department> Departments { get; set; }

        public Country Country { get; set; }

    }
}