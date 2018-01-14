using System;
using System.Collections.Generic;
using System.Linq;
using EDIS.Domain.Base;
using EDIS.Domain.Boards;
using EDIS.Domain.Buildings;
using EDIS.Domain.Circuits;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Certificates
{
    public class Certificate : BaseEntity
    {
        [PrimaryKey]
		[JsonProperty("cert_id")]
        public string CertId { get; set; }
        
		[JsonProperty("building_id")]
        public string BuildingId { get; set; }
        
		[JsonProperty("cert_number")]
        public int CertNumber { get; set; }
        
		[JsonProperty("cert_type")]
        public string CertType { get; set; }
        
		[JsonProperty("cert_created_by")]
        public string CertCreatedBy { get; set; }
        
		[JsonProperty("cert_status")]
        public string CertStatus { get; set; }
        
		[JsonProperty("cert_creation_status")]
        public int CertCreationStatus { get; set; }
        
		[JsonProperty("cert_description")]
        public string CertDescription { get; set; }
        
		[JsonProperty("cert_location_minorworks")]
        public int? CertLocationMinorworks { get; set; }
        
		[JsonProperty("cert_date_created")]
        public DateTime? CertDateCreated { get; set; }
        
		[JsonProperty("cert_date_amended")]
        public string CertDateAmended { get; set; }

        [JsonProperty("cert_date_signed_contractor")]
        public DateTime? CertDateSignedContractor { get; set; }

        [JsonProperty("cert_date_applied_contractor")]
        public DateTime? CertDateAppliedContractor { get; set; }

        [JsonProperty("confirmation_code_contractor")]
        public string ConfirmationCodeContractor { get; set; }

        [JsonProperty("cert_date_signed_supervisor")]
        public DateTime? CertDateSignedSupervisor { get; set; }

        [JsonProperty("cert_date_applied_supervisor")]
        public DateTime? CertDateAppliedSupervisor { get; set; }

        [JsonProperty("confirmation_code_supervisor")]
        public string ConfirmationCodeSupervisor { get; set; }

        [JsonProperty("cert_agreed_limitations")]
        public string CertAgreedLimitations { get; set; }
        
		[JsonProperty("cert_agreed_with")]
        public string CertAgreedWith { get; set; }
        
		[JsonProperty("cert_operational_limitations")]
        public string CertOperationalLimitations { get; set; }
        
		[JsonProperty("seo_id")]
        public string SeoId { get; set; }
        
		[JsonProperty("ES_user_id")]
        public string EsUserId { get; set; }
        
		[JsonProperty("con_user_id")]
        public string ConUserId { get; set; }
        
		[JsonProperty("con_person_name")]
        public string ConPersonName { get; set; } //It is used for Electricial value
        
		[JsonProperty("con_person_post")]
        public string ConPersonPost { get; set; }

        [JsonProperty("cert_being_generated")]
        public DateTime? CertBeingGenerated { get; set; }

        [JsonProperty("cert_being_authorized")]
        public bool? CertBeingAuthorized { get; set; }

        [JsonProperty("cert_date_signed_declaration")]
        public DateTime? CertDateSignedDeclaration { get; set; }

        [JsonProperty("cert_signed_declaration_upload")]
        public bool? CertSignedDeclarationUpload { get; set; }

        [JsonProperty("cert_signed_declaration_upload2")]
        public bool? CertSignedDeclarationUpload2 { get; set; }

        [JsonProperty("cert_comments_installation")]
        public string CertCommentsInstallation { get; set; }

        [JsonProperty("cert_last_generated")]
        public string CertLastGenerated { get; set; }

        [JsonProperty("cert_pdf_file_name")]
        public string CertPdfFileName { get; set; }

        [JsonProperty("cert_show_job_ref")]
        public string CertShowJobRef { get; set; }
        
		[JsonProperty("cert_show_edis_ref")]
        public string CertShowEdisRef { get; set; }
        
		[JsonProperty("cert_edition")]
        public string CertEdition { get; set; }
        
		[JsonProperty("cert_ammendment_version")]
        public int CertAmmendmentVersion { get; set; }
        
		[JsonProperty("rec_updation_type")]
        public int RecUpdationType { get; set; }
        
		[JsonProperty("external_certificate")]
        public int ExternalCertificate { get; set; }
        
		[JsonProperty("show_test_circuits_on_pdf")]
        public int ShowTestCircuitsOnPdf { get; set; }

        [JsonIgnore]
        [Ignore]
        public string Contractor { get; set; }
        
        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CertificateInspection> CertificateInspections { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BuildingTest> BuildingsTest { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CertificateInspectionObservations> CertificatesInspectionObservations { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BuildingContactTest> BuildingsContactTest { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BoardTest> BoardsTest { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CircuitTest> CircuitsTest { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CircuitPointsRcdTest> CircuitsPointsRcdTest { get; set; }

        [JsonIgnore]
        public string CertInspectionJobReference => CertificateInspections?.FirstOrDefault()?.CertInspectionJobReference;
    }
}