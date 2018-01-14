using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Buildings
{
    public class BuildingContact : BuildingContactBase
    {
		[JsonProperty("building_contact_id")]
        [PrimaryKey]
        public string BuildingContactId { get; set; }
    }
}