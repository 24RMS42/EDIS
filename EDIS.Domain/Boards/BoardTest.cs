using System;
using EDIS.Domain.Certificates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Boards
{
    public class BoardTest : BoardBase
    {
        [JsonProperty("board_test_id")]
        [PrimaryKey]
        public string BoardTestId { get; set; }

        [JsonProperty("cert_id")]
        [ForeignKey(typeof(Certificate))]
        public string CertId { get; set; }

        [JsonProperty("board_id")]
        public string BoardId { get; set; }

        [JsonProperty("test_instrument_earth_loop")]
        public string TestInstrumentEarthLoop { get; set; }

        [JsonProperty("test_instrument_insulation_resistance")]
        public string TestInstrumentInsulationResistance { get; set; }

        [JsonProperty("test_instrument_continuity")]
        public string TestInstrumentContinuity { get; set; }

        [JsonProperty("test_instrument_rcd")]
        public string TestInstrumentRcd { get; set; }

        [JsonProperty("test_instrument_other1")]
        public string TestInstrumentOther1 { get; set; }

        [JsonProperty("test_instrument_other2")]
        public string TestInstrumentOther2 { get; set; }

        [JsonProperty("board_tested_by_user_id")]
        public string BoardTestedByUserId { get; set; }

        [JsonProperty("board_tested_by_name")]
        public string BoardTestedByName { get; set; }

        [JsonProperty("board_tested_by_post")]
        public string BoardTestedByPost { get; set; }

        [JsonProperty("board_tested_date")]
        public DateTime? BoardTestedDate { get; set; }

        [JsonProperty("correct_supply_polarity_confirmed")]
        public int? CorrectSupplyPolarityConfirmed { get; set; }

        [JsonProperty("phase_sequence_confirmed")]
        public int? PhaseSequenceConfirmed { get; set; }

        [JsonProperty("circuits_equipment_vulnerable_details")]
        public string CircuitsEquipmentVulnerableDetails { get; set; }

        [JsonProperty("board_next_test_date")]
        public DateTime? BoardNextTestDate { get; set; }
    }
}