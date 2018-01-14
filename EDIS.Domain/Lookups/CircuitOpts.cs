using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace EDIS.Domain.Lookups
{
    public class CircuitOpts : BaseEntity
    {
        [JsonProperty("circuit_opt_id")]
        [PrimaryKey]
        public int CircuitOptId { get; set; }

        [JsonProperty("circuit_opt_bsen")]
        public string CircuitOptBsen { get; set; }

        [JsonProperty("circuit_opt_type")]
        public string CircuitOptType { get; set; }

        [JsonProperty("circuit_opt_max_permitted_disconnection_time")]
        public string CircuitOptMaxPermittedDisconnectionTime { get; set; }

        [JsonProperty("circuit_opt_rating")]
        public string CircuitOptRating { get; set; }

        [JsonProperty("circuit_opt_max_permitted_impedance")]
        public string CircuitOptMaxPermittedImpedance { get; set; }

        [JsonProperty("circuit_opt_voltage")]
        public string CircuitOptVoltage { get; set; }

        [JsonProperty("circuit_opt_regulation_date")]
        public DateTime? CircuitOptRegulationDate { get; set; }

        [JsonProperty("circuit_opt_regulation_table")]
        public string CircuitOptRegulationTable { get; set; }

        [JsonProperty("circuit_opt_inactive")]
        public string CircuitOptInactive { get; set; }
    }
}