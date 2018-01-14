using EDIS.Domain.Base;
using Newtonsoft.Json;

namespace EDIS.Domain.Buildings
{
    public class BuildingContactBase : BaseEntity
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("building_organization_responsible")]
        public string BuildingOrganizationResponsible { get; set; }

        [JsonProperty("building_occupier_name")]
        public string BuildingOccupierName { get; set; }

        [JsonProperty("building_occupier_address1")]
        public string BuildingOccupierAddress1 { get; set; }

        [JsonProperty("building_occupier_address2")]
        public string BuildingOccupierAddress2 { get; set; }

        [JsonProperty("building_occupier_address3")]
        public string BuildingOccupierAddress3 { get; set; }

        [JsonProperty("building_occupier_pcode")]
        public string BuildingOccupierPcode { get; set; }

        [JsonProperty("building_resp_person_name")]
        public string BuildingRespPersonName { get; set; }

        [JsonProperty("building_resp_person_address1")]
        public string BuildingRespPersonAddress1 { get; set; }

        [JsonProperty("building_resp_person_address2")]
        public string BuildingRespPersonAddress2 { get; set; }

        [JsonProperty("building_resp_person_address3")]
        public string BuildingRespPersonAddress3 { get; set; }

        [JsonProperty("building_resp_person_pcode")]
        public string BuildingRespPersonPcode { get; set; }

        [JsonProperty("building_resp_person_phone")]
        public string BuildingRespPersonPhone { get; set; }

        [JsonProperty("building_resp_person_email")]
        public string BuildingRespPersonEmail { get; set; }

        [JsonProperty("building_landlord_name")]
        public string BuildingLandlordName { get; set; }

        [JsonProperty("building_landlord_address1")]
        public string BuildingLandlordAddress1 { get; set; }

        [JsonProperty("building_landlord_address2")]
        public string BuildingLandlordAddress2 { get; set; }

        [JsonProperty("building_landlord_address3")]
        public string BuildingLandlordAddress3 { get; set; }

        [JsonProperty("building_landlord_pcode")]
        public string BuildingLandlordPcode { get; set; }

        [JsonProperty("building_landlord_phone")]
        public string BuildingLandlordPhone { get; set; }

        [JsonProperty("building_landlord_email")]
        public string BuildingLandlordEmail { get; set; }
    }
}