using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Certificates
{
    public class CertificateRow : BaseEntity
    {
        
		[JsonProperty("cert_id")]
        [PrimaryKey]
        public string CertId { get; set; }

        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("cert_number")]
        public int CertNumber { get; set; }
        
		[JsonProperty("cert_type")]
        public string CertType { get; set; }
        
		[JsonProperty("cert_status")]
        public string CertStatus { get; set; }
        
		[JsonProperty("cert_description")]
        public string CertDescription { get; set; }
        
		[JsonProperty("cert_date_created")]
        public string CertDateCreated { get; set; }
        
		[JsonProperty("cert_date_signed_supervisor")]
        public string CertDateSignedSupervisor { get; set; }
        
		[JsonProperty("cert_inspection_job_reference")]
        public string CertInspectionJobReference { get; set; }
        
		[JsonProperty("con_user_id")]
        public string ConUserId { get; set; }
        
		[JsonProperty("contractor_name")]
        public string ContractorName { get; set; }
        
		[JsonProperty("es_user_id")]
        public string EsUserId { get; set; }
        
		[JsonProperty("supervisor_name")]
        public string SupervisorName { get; set; }

        [JsonIgnore]
        [Ignore]
        public string Contractor { get; set; }
    }
}