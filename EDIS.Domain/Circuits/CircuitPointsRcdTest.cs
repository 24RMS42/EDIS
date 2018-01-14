using System.Collections.Generic;
using EDIS.Domain.Base;
using EDIS.Domain.Certificates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Circuits
{
    public class CircuitPointsRcdTest : CircuitPointsRcdBase
    {
        [JsonProperty("cpr_test_id")]
        [PrimaryKey]
        public string CprTestId { get; set; }

        [JsonProperty("cert_id")]
        [ForeignKey(typeof(Certificate))]
        public string CertId { get; set; }

        [JsonProperty("board_id")]
        public string BoardId { get; set; }

        [JsonProperty("circuit_rcd_op_time_I_tested")]
        public string CircuitRcdOpTimeITested { get; set; }

        [JsonProperty("circuit_rcd_op_time_5I_tested")]
        public string CircuitRcdOpTime_5ITested { get; set; }

        [JsonProperty("circuit_point_measured_zs")]
        public string CircuitPointMeasuredZs { get; set; }

        [JsonProperty("circuit_point_polarity")]
        public int? CircuitPointPolarity { get; set; }

        [JsonProperty("circuit_rcd_test_button")]
        public int? CircuitRcdTestButton { get; set; }

        [JsonProperty("dbcct_observation")]
        public string DbcctObservation { get; set; }

        [JsonProperty("dbcct_classification_code")]
        public string DbcctClassificationCode { get; set; }

        [JsonProperty("include_in_observations_recommendations_list")]
        public int IncludeInObservationsRecommendationsList { get; set; }

        [JsonProperty("obs_from_id")]
        public string ObsFromId { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CertificateInspectionObservations> CertificatesInspectionObservations { get; set; }
    }
}