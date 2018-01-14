using System;
using EDIS.Domain.Base;
using Newtonsoft.Json;

namespace EDIS.Domain.Buildings
{
    public class BuildingBase : BaseEntity
    {
        [JsonProperty("estate_id")]
        public string EstateId { get; set; }

        [JsonProperty("building_name")]
        public string BuildingName { get; set; }

        [JsonProperty("building_reference")]
        public string BuildingReference { get; set; }

        [JsonProperty("building_addr1")]
        public string BuildingAddr1 { get; set; }

        [JsonProperty("building_addr2")]
        public string BuildingAddr2 { get; set; }

        [JsonProperty("building_addr3")]
        public string BuildingAddr3 { get; set; }

        [JsonProperty("building_pcode")]
        public string BuildingPcode { get; set; }

        [JsonProperty("building_premises")]
        public string BuildingPremises { get; set; }

        [JsonProperty("building_premises_other")]
        public string BuildingPremisesOther { get; set; }

        [JsonProperty("building_group_area")]
        public string BuildingGroupArea { get; set; }

        [JsonProperty("building_group_manager_responsible")]
        public string BuildingGroupManagerResponsible { get; set; }

        [JsonProperty("building_nof_floors")]
        public int BuildingNofFloors { get; set; }

        [JsonProperty("building_area")]
        public float BuildingArea { get; set; }

        [JsonProperty("building_age_of_installation")]
        public int BuildingAgeOfInstallation { get; set; }

        [JsonProperty("installation_records_location")]
        public string InstallationRecordsLocation { get; set; }

        [JsonProperty("records_held_by")]
        public string RecordsHeldBy { get; set; }

        [JsonProperty("building_status")]
        public int? BuildingStatus { get; set; }

        [JsonProperty("building_created_by")]
        public string BuildingCreatedBy { get; set; }

        [JsonProperty("building_created_date")]
        public string BuildingCreatedDate { get; set; }
    }
}