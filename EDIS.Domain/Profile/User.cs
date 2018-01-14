using System;
using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using EDIS.Core;

namespace EDIS.Domain.Profile
{
    public class User : BaseEntity
    {
        [JsonProperty("user_id")]
        [PrimaryKey]
        public string UserId { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("user_fullname")]
        public string UserFullname { get; set; }

        [JsonProperty("user_organisation")]
        public string UserOrganisation { get; set; }

        [JsonProperty("user_mobile")]
        public string UserMobile { get; set; }

        [JsonProperty("user_email")]
        public string UserEmail { get; set; }

        [JsonProperty("user_group_id")]
        public int UserGroupId { get; set; }

        [JsonProperty("user_parent_id")]
        public string UserParentId { get; set; }

        [JsonProperty("user_status")]
        public int UserStatus { get; set; }

        [JsonProperty("user_logo")]
        public string UserLogo { get; set; }

        [JsonProperty("user_logoUploadDate")]
        public string UserLogoUploadDate { get; set; }

        [JsonProperty("user_address")]
        public string UserAddress { get; set; }

        [JsonProperty("user_landline")]
        public string UserLandline { get; set; }

        [JsonProperty("user_website")]
        public string UserWebsite { get; set; }

        [JsonProperty("user_position")]
        public string UserPosition { get; set; }

        [JsonProperty("contractor_accredited_certification_body")]
        public string ContractorAccreditedCertificationBody { get; set; }

        [JsonProperty("contractor_accreditation_number")]
        public string ContractorAccreditationNumber { get; set; }

        [JsonProperty("user_branch_number")]
        public string UserBranchNumber { get; set; }

        [JsonProperty("cert_title_bkg_color_id")]
        public int CertTitleBkgColorId { get; set; }

        [JsonProperty("cert_title_text_color_id")]
        public int CertTitleTextColorId { get; set; }

        [JsonProperty("user_signature_image")]
        public string UserSignatureImage { get; set; }

        [JsonProperty("use_scanned_signature")]
        public int UseScannedSignature { get; set; }

        [JsonProperty("user_self_regd")]
        public int UserSelfRegd { get; set; }

        [JsonProperty("user_regd_application_date")]
        public DateTime? UserRegdApplicationDate { get; set; }

        [JsonProperty("user_regd_activation_date")]
        public DateTime? UserRegdActivationDate { get; set; }

        [JsonProperty("user_accepted_terms")]
        public string UserAcceptedTerms { get; set; }

        [JsonProperty("user_subscribed_rpts")]
        public int UserSubscribedRpts { get; set; }

        [JsonProperty("user_logo_content")]
        public string UserLogoContent { get; set; }

        [JsonProperty("user_phone")]
        public string UserPhone { get; set; }

        [JsonProperty("user_fax")]
        public string UserFax { get; set; }

        [JsonIgnore]
        public string UserLogoWithPath
        {
            get
            {
                var userLogoWithPath = "";
                if (!string.IsNullOrEmpty(UserLogo))
                {
                    userLogoWithPath = Settings.profileFolder + '/'+ UserLogo;
                }
                return userLogoWithPath;
            }
            set { }
        }
    }
}