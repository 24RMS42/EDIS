using EDIS.Domain.Certificates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Circuits
{
    public class CircuitTest : CircuitBase
    {
        [JsonProperty("circuit_test_id")]
        [PrimaryKey]
        public string CircuitTestId { get; set; }

        [JsonProperty("cert_id")]
        [ForeignKey(typeof(Certificate))]
        public string CertId { get; set; }

        [JsonProperty("board_id")]
        public string BoardId { get; set; }

        [JsonProperty("circuit_id")]
        public string CircuitId { get; set; }

        [JsonProperty("circuit_rcd_op_current_ms")]
        public int? CircuitRcdOpCurrentMs { get; set; }

        [JsonProperty("circuit_comments")]
        public string CircuitComments { get; set; }

        [JsonProperty("circuit_inspection_done")]
        public int CircuitInspectionDone { get; set; }

        [JsonProperty("circuit_impedance_ring_final_r1_phases")]
        public string CircuitImpedanceRingFinalR1Phases { get; set; }

        [JsonProperty("circuit_impedance_ring_final_rn_neutral")]
        public string CircuitImpedanceRingFinalRnNeutral { get; set; }

        [JsonProperty("circuit_impedance_ring_final_r2_cpc")]
        public string CircuitImpedanceRingFinalR2Cpc { get; set; }

        [JsonProperty("circuit_impedance_R1R2")]
        public string CircuitImpedanceR1R2 { get; set; }

        [JsonProperty("circuit_impedance_R2")]
        public string CircuitImpedanceR2 { get; set; }

        [JsonProperty("circuit_insulation_resistance_phase_phase")]
        public string CircuitInsulationResistancePhasePhase { get; set; }

        [JsonProperty("circuit_insulation_resistance_phase_neutral")]
        public string CircuitInsulationResistancePhaseNeutral { get; set; }

        [JsonProperty("circuit_insulation_resistance_phase_earth")]
        public string CircuitInsulationResistancePhaseEarth { get; set; }

        [JsonProperty("circuit_insulation_resistance_neutral_earth")]
        public string CircuitInsulationResistanceNeutralEarth { get; set; }

        [JsonProperty("circuit_earthing_adaquate")]
        public int CircuitEarthingAdaquate { get; set; }

        [JsonProperty("circuit_bonding_adaquate")]
        public int CircuitBondingAdaquate { get; set; }

        [JsonProperty("circuit_polarity_correct")]
        public int? CircuitPolarityCorrect { get; set; }

        [JsonProperty("circuit_max_earth_loop")]
        public string CircuitMaxEarthLoop { get; set; }

        [JsonProperty("circuit_protection_method")]
        public int? CircuitProtectionMethod { get; set; }

        [JsonProperty("circuit_rcd_op_time_I")]
        public string CircuitRcdOpTimeI { get; set; }

        [JsonProperty("circuit_rcd_op_time_5I")]
        public string CircuitRcdOpTime_5I { get; set; }

        [JsonProperty("circuit_mw_description")]
        public int? CircuitMwDescription { get; set; }

        [JsonProperty("circuit_mw_location")]
        public int? CircuitMwLocation { get; set; }

        [JsonProperty("mw_circuit_details_of_departures")]
        public int? MwCircuitDetailsOfDepartures { get; set; }

        [JsonProperty("circuit_rcdtest_button")]
        public int? CircuitRcdtestButton { get; set; }

        [JsonProperty("dbcct_observation")]
        public string DbcctObservation { get; set; }

        [JsonProperty("dbcct_classification_code")]
        public string DbcctClassificationCode { get; set; }

        [JsonProperty("include_in_observations_recommendations_list")]
        public int IncludeInObservationsRecommendationsList { get; set; }

        [JsonProperty("obs_from_id")]
        public string ObsFromId { get; set; }
    }
}