using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class CertificateInspectionQuestions : BaseEntity
    {
        [JsonProperty("ins_question_id")]
        [PrimaryKey]
        public int InsQuestionId { get; set; }

        [JsonProperty("ins_question_text")]
        public string InsQuestionText { get; set; }

        [JsonProperty("ins_question_precedence")]
        public int InsQuestionPrecedence { get; set; }

        [JsonProperty("ins_question_type")]
        public string InsQuestionType { get; set; }

        [JsonProperty("ins_question_set")]
        public int InsQuestionSet { get; set; }

        [JsonProperty("ins_question_heading_level")]
        public int InsQuestionHeadingLevel { get; set; }
    }
}

