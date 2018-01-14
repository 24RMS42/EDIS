using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Circuits
{
    public class CircuitPointsRcdBase : BaseEntity
    {
        [JsonProperty("circuit_id")]
        [ForeignKey(typeof(Circuit))]
        public string CircuitId { get; set; }

        [JsonProperty("circuit_reference")]
        public int CircuitReference { get; set; }

        [JsonProperty("circuit_phase")]
        public string CircuitPhase { get; set; }

        [JsonProperty("circuit_end_point")]
        public string CircuitEndPoint { get; set; }

        [JsonProperty("circuit_point_position")]
        public string CircuitPointPosition { get; set; }

        [JsonProperty("point_last_test")]
        public string PointLastTest { get; set; }

        [JsonProperty("point_next_test")]
        public string PointNextTest { get; set; }

        [JsonProperty("circuit_rcd_bsen_num")]
        public string CircuitRcdBsenNum { get; set; }

        [JsonProperty("circuit_rcd_current")]
        public string CircuitRcdCurrent { get; set; }

        [JsonProperty("circuit_rcd_op_time_I_rated")]
        public string CircuitRcdOpTimeIRated { get; set; }

        [JsonProperty("circuit_rcd_op_time_5I_rated")]
        public string CircuitRcdOpTime_5IRated { get; set; }
    }
}