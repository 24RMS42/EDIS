using System;
using System.Collections.Generic;
using EDIS.Domain.Base;
using EDIS.Domain.Circuits;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Boards
{
    public class Board : BoardBase
    {
		[JsonProperty("board_id")]
        [PrimaryKey]
        public string BoardId { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Circuit> Circuits { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CircuitPointsRcd> CircuitsPointsRcd { get; set; }
    }
}