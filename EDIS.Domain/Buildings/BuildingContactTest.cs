using EDIS.Domain.Certificates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Buildings
{
    public class BuildingContactTest : BuildingContactBase
    {
        [JsonProperty("building_contact_test_id")]
        [PrimaryKey]
        public string BuildingContactTestId { get; set; }

        [JsonProperty("cert_id")]
        [ForeignKey(typeof(Certificate))]
        public string CertId { get; set; }
    }
}