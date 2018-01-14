using System.Collections.Generic;
using EDIS.Domain.Buildings;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class BuildingsResponse : BaseResponse
    {
        [JsonProperty("estate_id")]
        public string EstateId { get; set; }

        [JsonProperty("estate_privileges")]
        public string EstatePrivileges { get; set; }

        [JsonProperty("buildings")]
        public IEnumerable<BuildingRow> Buildings { get; set; }
    }
}