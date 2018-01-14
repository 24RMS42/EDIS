using EDIS.Domain.Base;
using EDIS.Domain.Circuits;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Certificates
{
    public class CertificateInspectionObservations : BaseEntity
    {
        [JsonProperty("cert_inspection_obs_id")]
        [PrimaryKey]
        public string CertInspectionObsId { get; set; }

        [JsonProperty("cert_id")]
        [ForeignKey(typeof(Certificate))]
        public string CertId { get; set; }

        [JsonProperty("cpr_test_id")]
        [ForeignKey(typeof(CircuitPointsRcdTest))]
        public string CprTestId { get; set; }

        [JsonProperty("cert_inspection_obs_item_no")]
        public int CertInspectionObsItemNo { get; set; }

        [JsonProperty("cert_inspection_obs_item_observation")]
        public string CertInspectionObsItemObservation { get; set; }

        [JsonProperty("cert_inspection_obs_item_status")]
        public string CertInspectionObsItemStatus { get; set; }

        [JsonProperty("contractor_id")]
        public string ContractorId { get; set; }

        [JsonProperty("item_status")]
        public int ItemStatus { get; set; }

        [JsonProperty("final_comments")]
        public string FinalComments { get; set; }

        [JsonProperty("item_date")]
        public string ItemDate { get; set; }

        [JsonProperty("item_closed")]
        public string ItemClosed { get; set; }

        [JsonProperty("board_id")]
        public string BoardId { get; set; }

        [JsonProperty("circuit_test_id")]
        public string CircuitTestId { get; set; }

        [JsonProperty("observation_type")]
        public int ObservationType { get; set; }

        [JsonProperty("ins_question_id")]
        public int InsQuestionId { get; set; }

        [JsonProperty("obs_group_id")]
        public string ObsGroupId { get; set; }

        [JsonProperty("obs_from_id")]
        public string ObsFromId { get; set; }
    }
}