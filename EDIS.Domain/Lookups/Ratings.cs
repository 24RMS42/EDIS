using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class Ratings : BaseEntity
    {
        [JsonProperty("rating_value")]
        [PrimaryKey]
        public int RatingValue { get; set; }
        
    }
}

