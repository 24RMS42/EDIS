using System.Collections;
using System.Collections.Generic;
using EDIS.Domain.Base;
using EDIS.Domain.Buildings;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Estates
{
    public class EstateRow : BaseEntity
    {   
		[JsonProperty("estate_id")]
        [PrimaryKey]
        public string EstateId { get; set; }
        
		[JsonProperty("estate_name")]
        public string EstateName { get; set; }
        
		[JsonProperty("estate_description")]
        public string EstateDescription { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BuildingRow> Buildings { get; set; }
    }
}