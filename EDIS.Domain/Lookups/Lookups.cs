using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace EDIS.Domain.Lookups
{
    public class Lookups : BaseEntity
    {
        [JsonProperty("tlkp_board_reference_type")]
        public List<BoardReferenceTypes> BoardReferenceTypes { get; set; }

        [JsonProperty("tlkp_conductor_size_type")]
        public List<ConductorSizeTypes> ConductorSizeTypes { get; set; }

        [JsonProperty("tlkp_circuit_opt")]
        public List<CircuitOpts> CircuitOpts { get; set; }

        [JsonProperty("tlkp_opt")]
        public List<Opts> Opts { get; set; }

        [JsonProperty("tlkp_phase")]
        public List<Phases> Phases { get; set; }

        [JsonProperty("tlkp_phase_sort_order")]
        public List<PhaseSortOrders> PhaseSortOrders { get; set; }

        [JsonProperty("tlkp_rating")]
        public List<Ratings> Ratings { get; set; }

        [JsonProperty("tlkp_cable_reference_method")]
        public List<CableReferenceMethods> CableReferenceMethods { get; set; }

        [JsonProperty("tlkp_certificate_inspection_question")]
        public List<CertificateInspectionQuestions> CertificateInspectionQuestions { get; set; }

        [JsonProperty("tlkp_bs_amendment_dates")]
        public List<BsAmendmentDates> BsAmendmentDates { get; set; }

        [JsonProperty("tlkp_rcd_type")]
        public List<RcdTypes> RcdTypes { get; set; }

        [JsonProperty("tlkp_rcd_op_current")]
        public List<RcdOpCurrents> RcdOpCurrents { get; set; }

        [JsonProperty("tlkp_csa_cpc")]
        public List<CsaCpc> CsaCpcs { get; set; }

        [JsonProperty("tlkp_csa_live")]
        public List<CsaLive> CsaLives { get; set; }

        [JsonProperty("tlkp_naming_convention")]
        public List<NamingConventions> NamingConventions { get; set; }

    }
}
