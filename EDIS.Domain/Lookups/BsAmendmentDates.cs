using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class BsAmendmentDates : BaseEntity
    {
        [JsonProperty("cert_date_amended_id")]
        [PrimaryKey]
        public int CertDateAmendedId { get; set; }

        [JsonProperty("cert_date_amended")]
        public string CertDateAmended { get; set; }
    }
}
