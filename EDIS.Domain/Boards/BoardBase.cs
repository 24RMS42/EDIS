using System;
using EDIS.Domain.Base;
using Newtonsoft.Json;

namespace EDIS.Domain.Boards
{
    public class BoardBase : BaseEntity
    {
        [JsonProperty("copied_from_board_id")]
        public string CopiedFromBoardId { get; set; }

        [JsonProperty("board_reference")]
        public string BoardReference { get; set; }

        [JsonProperty("board_reference_type")]
        public string BoardReferenceType { get; set; }

        [JsonIgnore]
        public string BoardIdentity => BoardReference + " " + BoardReferenceType;

        [JsonIgnore]
        public string BoardRepresentation => BoardIdentity + ", " + BoardLocationFloor + ", " + BoardLocationBlock;

        [JsonProperty("board_nominal_voltage")]
        public int? BoardNominalVoltage { get; set; }

        [JsonProperty("board_phase")]
        public string BoardPhase { get; set; }

        [JsonProperty("board_circuit_phase_naming")]
        public string BoardCircuitPhaseNaming { get; set; }

        [JsonProperty("Is_Mains_Distribution")]
        public string IsMainsDistribution { get; set; }

        [JsonProperty("board_manufacturer")]
        public string BoardManufacturer { get; set; }

        [JsonProperty("board_function")]
        public string BoardFunction { get; set; }

        [JsonProperty("board_ways")]
        public int BoardWays { get; set; }

        [JsonProperty("board_type")]
        public string BoardType { get; set; }

        [JsonProperty("board_rating")]
        public string BoardRating { get; set; }

        [JsonProperty("board_asset_number")]
        public string BoardAssetNumber { get; set; }

        [JsonProperty("board_earth_loop")]
        public string BoardEarthLoop { get; set; }

        [JsonProperty("board_PSSC")]
        public string BoardPssc { get; set; }

        [JsonProperty("board_supply_conductor_size")]
        public string BoardSupplyConductorSize { get; set; }

        [JsonProperty("board_supply_circuit_reference")]
        public int BoardSupplyCircuitReference { get; set; }

        [JsonProperty("board_supply_circuit_phase")]
        public string BoardSupplyCircuitPhase { get; set; }

        [JsonProperty("board_supply_circuit_conductor_size_type")]
        public string BoardSupplyCircuitConductorSizeType { get; set; }

        [JsonProperty("board_supply_circuit_conductor_size_type_other")]
        public string BoardSupplyCircuitConductorSizeTypeOther { get; set; }

        [JsonProperty("board_OPT")]
        public string BoardOpt { get; set; }

        [JsonProperty("board_OPT_type")]
        public string BoardOptType { get; set; }

        [JsonProperty("board_supply_conductor_rating")]
        public string BoardSupplyConductorRating { get; set; }

        [JsonProperty("board_location_block")]
        public string BoardLocationBlock { get; set; }

        [JsonProperty("board_location_floor")]
        public string BoardLocationFloor { get; set; }

        [JsonProperty("board_location")]
        public string BoardLocation { get; set; }

        [JsonProperty("board_picture_id")]
        public string BoardPictureId { get; set; }

        [JsonProperty("board_supply_source_reference")]
        public string BoardSupplySourceReference { get; set; }

        [JsonProperty("board_supply_source_reference_type")]
        public string BoardSupplySourceReferenceType { get; set; }

        [JsonProperty("board_rcd")]
        public string BoardRcd { get; set; }

        [JsonProperty("board_rcd_poles")]
        public string BoardRcdPoles { get; set; }

        [JsonProperty("board_rcd_rating")]
        public string BoardRcdRating { get; set; }

        [JsonProperty("current_mod_number")]
        public int CurrentModNumber { get; set; }

        [JsonProperty("issue_number")]
        public int IssueNumber { get; set; }

        [JsonProperty("date_last_amended")]
        public string DateLastAmended { get; set; }

        [JsonProperty("spreadsheet_filename")]
        public int? SpreadsheetFilename { get; set; }

        [JsonProperty("DateObsolete")]
        public DateTime? DateObsolete { get; set; }

        [JsonProperty("board_comments")]
        public string BoardComments { get; set; }

        [JsonProperty("board_rcd_op_time_I")]
        public string BoardRcdOpTimeI { get; set; }

        [JsonProperty("board_rcd_op_time_5I")]
        public string BoardRcdOpTime_5I { get; set; }

        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("amend_status")]
        public int? AmmendStatus { get; set; }

        [JsonProperty("board_last_thermo_date")]
        public DateTime? BoardLastThermoDate { get; set; }

        [JsonProperty("board_next_thermo_date")]
        public DateTime? BoardNextThermoDate { get; set; }

        [JsonProperty("board_supply_source_reference_id")]
        public string BoardSupplySourceReferenceId { get; set; }

        [JsonProperty("is_migrated")]
        public int IsMigrated { get; set; }
    }
}