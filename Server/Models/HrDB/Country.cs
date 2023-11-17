using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HrApp.Server.Models.HrDB
{
    [Table("countries", Schema = "dbo")]
    public partial class Country
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
        [Required]
        public string country_id { get; set; }

        [ConcurrencyCheck]
        public string country_name { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int region_id { get; set; }

        public Region Region { get; set; }

        public ICollection<Location> Locations { get; set; }

    }
}