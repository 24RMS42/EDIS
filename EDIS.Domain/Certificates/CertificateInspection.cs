using System;
using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Certificates
{
    public class CertificateInspection : BaseEntity
    {
		[JsonProperty("cert_inspection_id")]
		[PrimaryKey]
        public string CertInspectionId { get; set; }
        
		[JsonProperty("cert_id")]
		[ForeignKey(typeof(Certificate))]
        public string CertId { get; set; }
        
		[JsonProperty("cert_inspection_extent_covered")]
        public string CertInspectionExtentCovered { get; set; }
        
		[JsonProperty("cert_inspection_remedy_required")]
        public string CertInspectionRemedyRequired { get; set; }
        
		[JsonProperty("cert_inspection_general_condition")]
        public string CertInspectionGeneralCondition { get; set; }
        
		[JsonProperty("cert_inspection_date")]
        public DateTime? CertInspectionDate { get; set; }
        
		[JsonProperty("cert_inspection_date_installed")]
        public DateTime? CertInspectionDateInstalled { get; set; }
        
		[JsonProperty("cert_inspection_assessment")]
        public string CertInspectionAssessment { get; set; }
        
		[JsonProperty("cert_inspection_date_next")]
        public string CertInspectionDateNext { get; set; }
        
		[JsonProperty("cert_inspection_building_evidence_of_alteration")]
        public string CertInspectionBuildingEvidenceOfAlteration { get; set; }
        
		[JsonProperty("cert_inspection_building_age_of_alteration")]
        public int? CertInspectionBuildingAgeOfAlteration { get; set; }
        
		[JsonProperty("cert_inspection_previous_inspection_date")]
        public DateTime? CertInspectionPreviousInspectionDate { get; set; }
        
		[JsonProperty("cert_inspection_previous_inspection_comments")]
        public string CertInspectionPreviousInspectionComments { get; set; }
        
		[JsonProperty("cert_inspection_previous_cert_number")]
        public string CertInspectionPreviousCertNumber { get; set; }
        
		[JsonProperty("cert_inspection_EI_type")]
        public int CertInspectionEiType { get; set; }
        
		[JsonProperty("cert_inspection_departure_details")]
        public string CertInspectionDepartureDetails { get; set; }
        
		[JsonProperty("cert_inspection_job_reference")]
        public string CertInspectionJobReference { get; set; }
        
		[JsonProperty("cert_inspection_seo_pro_method_confirm")]
        public string CertInspectionSeoProMethodConfirm { get; set; }
        
		[JsonProperty("cert_inspection_installation_comments")]
        public string CertInspectionInstallationComments { get; set; }
        
		[JsonProperty("cert_inspection_DCIT_by_contractor_only")]
        public int CertInspectionDcitByContractorOnly { get; set; }
        
		[JsonProperty("designer_1_user_id")]
        public string Designer1UserId { get; set; }
        
		[JsonProperty("designer_1_fullname")]
        public string Designer1Fullname { get; set; }
        
		[JsonProperty("designer_1_organisation")]
        public string Designer1Organisation { get; set; }
        
		[JsonProperty("designer_1_accreditation_number")]
        public string Designer1AccreditationNumber { get; set; }
        
		[JsonProperty("designer_1_address")]
        public string Designer1Address { get; set; }
        
		[JsonProperty("designer_1_branch_number")]
        public string Designer1BranchNumber { get; set; }
        
		[JsonProperty("designer_1_sign_date")]
        public DateTime? Designer1SignDate { get; set; }
        
		[JsonProperty("designer_2_applied_date")]
        public DateTime? Designer2AppliedDate { get; set; }
        
		[JsonProperty("designer_2_confirmation_code")]
        public int? Designer2ConfirmationCode { get; set; }
        
		[JsonProperty("designer_1_applied_date")]
        public DateTime? Designer1AppliedDate { get; set; }
        
		[JsonProperty("designer_1_confirmation_code")]
        public int? Designer1ConfirmationCode { get; set; }
        
		[JsonProperty("designer_2_user_id")]
        public string Designer2UserId { get; set; }
        
		[JsonProperty("designer_2_fullname")]
        public string Designer2Fullname { get; set; }
        
		[JsonProperty("designer_2_organisation")]
        public string Designer2Organisation { get; set; }
        
		[JsonProperty("designer_2_accreditation_number")]
        public string Designer2AccreditationNumber { get; set; }
        
		[JsonProperty("designer_2_address")]
        public string Designer2Address { get; set; }
        
		[JsonProperty("designer_2_branch_number")]
        public string Designer2BranchNumber { get; set; }
        
		[JsonProperty("designer_2_sign_date")]
        public string Designer2SignDate { get; set; }
        
		[JsonProperty("designer_date_amended")]
        public string DesignerDateAmended { get; set; }
        
		[JsonProperty("designer_inspection_departure_details")]
        public string DesignerInspectionDepartureDetails { get; set; }
        
		[JsonProperty("constructor_user_id")]
        public string ConstructorUserId { get; set; }
        
		[JsonProperty("constructor_fullname")]
        public string ConstructorFullname { get; set; }
        
		[JsonProperty("constructor_organisation")]
        public string ConstructorOrganisation { get; set; }
        
		[JsonProperty("constructor_accreditation_number")]
        public string ConstructorAccreditationNumber { get; set; }
        
		[JsonProperty("constructor_address")]
        public string ConstructorAddress { get; set; }
        
		[JsonProperty("constructor_branch_number")]
        public string ConstructorBranchNumber { get; set; }
        
		[JsonProperty("constructor_date_amended")]
        public string ConstructorDateAmended { get; set; }
        
		[JsonProperty("constructor_inspection_departure_details")]
        public string ConstructorInspectionDepartureDetails { get; set; }
        
		[JsonProperty("constructor_sign_date")]
        public DateTime? ConstructorSignDate { get; set; }
        
		[JsonProperty("constructor_applied_date")]
        public DateTime? ConstructorAppliedDate { get; set; }
        
		[JsonProperty("constructor_confirmation_code")]
        public int? ConstructorConfirmationCode { get; set; }
        
		[JsonProperty("cert_addendum_text")]
        public string CertAddendumText { get; set; }
        
		[JsonProperty("cert_addendum_date")]
        public DateTime? CertAddendumDate { get; set; }
        
		[JsonProperty("cert_addendum_user_id")]
        public string CertAddendumUserId { get; set; }
        
		[JsonProperty("show_inspection_schedule_pdf")]
        public int ShowInspectionSchedulePdf { get; set; }
        
		[JsonProperty("show_non_compliant_obs_pdf")]
        public int ShowNonCompliantObsPdf { get; set; }
        
		[JsonProperty("show_board_summary")]
        public int ShowBoardSummary { get; set; }
        
		[JsonProperty("ins_question_set")]
        public int InsQuestionSet { get; set; }
        
		[JsonProperty("additional_comments")]
        public string AdditionalComments { get; set; }
    }
}