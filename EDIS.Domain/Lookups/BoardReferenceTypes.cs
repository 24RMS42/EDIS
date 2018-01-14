using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class BoardReferenceTypes : BaseEntity
    {
        [JsonProperty("board_reference_type")]
        [PrimaryKey]
        public string BoardReferenceType { get; set; }
    }
}