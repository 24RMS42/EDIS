using EDIS.Domain.Base;
using EDIS.Domain.Buildings;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace EDIS.Domain.Buildings
{
    public class BuildingUser : BaseEntity
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonIgnore]
        [Indexed(Name = "BuildingId", Unique = false)]
        [ForeignKey(typeof(Building))]
        public string BuildingId { get; set; }

        [JsonProperty("user_id")]
        [Indexed(Name = "UserId", Unique = false)]
        public string UserId { get; set; }
        
		[JsonProperty("user_name")]
        public string UserName { get; set; }
        
		[JsonProperty("user_fullname")]
        public string UserFullname { get; set; }
        
		[JsonProperty("user_organisation")]
        public string UserOrganisation { get; set; }
        
		[JsonProperty("user_branch_number")]
        public string UserBranchNumber { get; set; }
        
		[JsonProperty("contractor_accreditation_number")]
        public string ContractorAccreditationNumber { get; set; }
        
		[JsonProperty("user_address")]
        public string UserAddress { get; set; }
        
		[JsonProperty("user_privileges")]
        public string UserPrivileges { get; set; }
    }
}