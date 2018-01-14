using System.Collections.Generic;
using EDIS.Domain.Boards;
using EDIS.Domain.Buildings;
using EDIS.Domain.Certificates;
using EDIS.Domain.Circuits;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class CertificateResponse : BaseResponse
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("cert_id")]
        public string CertId { get; set; }

        [JsonProperty("tbl_certificate")]
        public List<Certificate> Certificates { get; set; }

        [JsonProperty("tbl_certificate_inspection")]
        public List<CertificateInspection> CertificateInspections { get; set; }

        [JsonProperty("tbl_certificate_inspection_observations")]
        public List<CertificateInspectionObservations> CertificateInspectionsObservations { get; set; }

        [JsonProperty("tbl_building_test")]
        public List<BuildingTest> BuildingsTest { get; set; }

        [JsonProperty("tbl_building_contact_test")]
        public List<BuildingContactTest> BuildingsContactTest { get; set; }

        [JsonProperty("tbl_board_test")]
        public List<BoardTest> BoardsTest { get; set; }

        [JsonProperty("tbl_circuit_test")]
        public List<CircuitTest> CircuitsTest { get; set; }

        [JsonProperty("tbl_circuit_points_rcd_test")]
        public List<CircuitPointsRcdTest> CircuitsPointsRcdTest { get; set; }
    }
}