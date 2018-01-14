using System.Collections.Generic;
using EDIS.Domain.Buildings;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class BuildingResponse : BaseResponse
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("tbl_building")]
        public IEnumerable<Building> Buildings { get; set; }

        [JsonProperty("tbl_building_contact")]
        public IEnumerable<BuildingContact> BuildingContacts { get; set; }

        [JsonProperty("building_users")]
        public List<BuildingUser> BuildingUsers { get; set; }
    }
}