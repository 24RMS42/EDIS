using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class PhaseSortOrders : BaseEntity
    {
        [JsonProperty("circuit_phase")]
        [PrimaryKey]
        public string CircuitPhase { get; set; }

        [JsonProperty("sort_order")]
        public int SortOrder { get; set; }
    }
}
