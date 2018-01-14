using System;
using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Buildings
{
    public class Building : BuildingBase
    {
		[JsonProperty("building_id")]
        [PrimaryKey]
        public string BuildingId { get; set; }
    }
}