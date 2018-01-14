using EDIS.Domain.Certificates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Buildings
{
    public class BuildingTest : BuildingBase
    {
        [JsonProperty("building_test_id")]
        [PrimaryKey]
        public string BuildingTestId { get; set; }

        [JsonProperty("cert_id")]
        [ForeignKey(typeof(Certificate))]
        public string CertId { get; set; }

        [JsonProperty("building_id")]
        public string BuildingId { get; set; }
    }
}