using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Profile
{
    public class Instrument : BaseEntity
    {
        [JsonProperty("user_id")]
        [PrimaryKey]
        public string UserId { get; set; }

        [JsonProperty("instrument_efli_sn")]
        public string InstrumentEfliSn { get; set; }

        [JsonProperty("instrument_ir_sn")]
        public string InstrumentIrSn { get; set; }

        [JsonProperty("instrument_c_sn")]
        public string InstrumentCSn { get; set; }

        [JsonProperty("instrument_rcd_sn")]
        public string InstrumentRcdSn { get; set; }

        [JsonProperty("instrument_o_sn1")]
        public string InstrumentOSn1 { get; set; }

        [JsonProperty("instrument_o_sn2")]
        public string InstrumentOSn2 { get; set; }
    }
}