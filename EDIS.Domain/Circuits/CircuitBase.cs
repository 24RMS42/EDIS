using System;
using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Circuits
{
    public class CircuitBase : BaseEntity
    {
        [JsonProperty("record_type")]
        public string RecordType { get; set; }

        [JsonProperty("circuit_date_from")]
        public DateTime? CircuitDateFrom { get; set; }

        [JsonProperty("circuit_date_to")]
        public DateTime? CircuitDateTo { get; set; }

        [JsonProperty("circuit_reference")]
        public int CircuitReference { get; set; }

        [JsonProperty("circuit_phase")]
        public string CircuitPhase { get; set; }

        [JsonProperty("circuit_equipment_connected")]
        public string CircuitEquipmentConnected { get; set; }

        [JsonProperty("circuit_is_sub_main")]
        public string CircuitIsSubMain { get; set; }

        [JsonProperty("circuit_is_3phase")]
        public string CircuitIs3Phase { get; set; }

        [JsonProperty("circuit_conductor_size_type")]
        public string CircuitConductorSizeType { get; set; }

        [JsonProperty("circuit_cable_other_text")]
        public string CircuitCableOtherText { get; set; }

        [JsonProperty("circuit_reference_method")]
        public string CircuitReferenceMethod { get; set; }

        [JsonProperty("circuit_reference_method_id")]
        public int CircuitReferenceMethodId { get; set; }

        [JsonProperty("circuit_number_of_points")]
        public int CircuitNumberOfPoints { get; set; }

        [JsonProperty("circuit_conductor_csa_live")]
        public string CircuitConductorCsaLive { get; set; }

        [JsonProperty("circuit_conductor_csa_cpc")]
        public string CircuitConductorCsaCpc { get; set; }

        [JsonProperty("circuit_load")]
        public int? CircuitLoad { get; set; }

        [JsonIgnore]
        public string CircuitIdentity => CircuitReference + CircuitPhase;

        [JsonProperty("circuit_load_test_date")]
        public string CircuitLoadTestDate { get; set; }

        [JsonProperty("circuit_last_test_date")]
        public string CircuitLastTestDate { get; set; }

        [JsonProperty("circuit_next_test_date")]
        public string CircuitNextTestDate { get; set; }

        [JsonProperty("circuit_planned_test_date")]
        public string CircuitPlannedTestDate { get; set; }

        [JsonProperty("circuit_scheduled_test_date")]
        public string CircuitScheduledTestDate { get; set; }

        [JsonProperty("circuit_max_permitted_disconnection_time")]
        public string CircuitMaxPermittedDisconnectionTime { get; set; }

        [JsonIgnore]
        public string CircuitRepresentation => CircuitIdentity + " " + CircuitEquipmentConnected;

        [JsonIgnore]
        public bool NotBelongsToThreePhase { get; set; } = true;

        [JsonIgnore]
        [Ignore]
        public string ThreePhase { get; set; }

        [JsonIgnore]
        public string ThreePhaseRepresentation => string.IsNullOrEmpty(ThreePhase) ? CircuitRepresentation : ThreePhase;

        [JsonIgnore]
        public string CircuitOcpdPattern {
            get
            {
                if (string.IsNullOrEmpty(CircuitOcpdType) ||  string.IsNullOrEmpty(CircuitOcpdRating))
                    return CircuitOcpd + " " + CircuitOcpdType + " " + CircuitOcpdRating;
                return CircuitOcpd + " " + CircuitOcpdType + ", " + CircuitOcpdRating;
            }
        }

        [JsonProperty("circuit_OCPD")]
        public string CircuitOcpd { get; set; }

        [JsonProperty("circuit_OCPD_Type")]
        public string CircuitOcpdType { get; set; }

        [JsonProperty("circuit_OCPD_rating")]
        public string CircuitOcpdRating { get; set; }

        [JsonProperty("circuit_OCPD_scc")]
        public string CircuitOcpdScc { get; set; }

        [JsonProperty("circuit_rcd_op_current")]
        public string CircuitRcdOpCurrent { get; set; }

        [JsonProperty("circuit_max_permitted_impedance")]
        public string CircuitMaxPermittedImpedance { get; set; }

        [JsonProperty("circuit_rcd_bsen")]
        public string CircuitRcdBsen { get; set; }

        [JsonProperty("circuit_rcd_poles")]
        public int CircuitRcdPoles { get; set; }

        [JsonProperty("linked_circuit_id")]
        public string LinkedCircuitId { get; set; }

        [JsonProperty("date_last_amended")]
        public DateTime? DateLastAmended { get; set; }

        [JsonProperty("edited_in_cert")]
        public int EditedInCert { get; set; }
    }
}