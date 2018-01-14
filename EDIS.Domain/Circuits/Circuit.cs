using System;
using System.Collections.Generic;
using EDIS.Domain.Base;
using EDIS.Domain.Boards;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Circuits
{
    public class Circuit : CircuitBase
    {
		[JsonProperty("circuit_id")]
        [PrimaryKey]
        public string CircuitId { get; set; }
        
		[JsonProperty("board_id")]
        [ForeignKey(typeof(Board))]
        public string BoardId { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CircuitPointsRcdTest> CircuitsPointsRcdTest { get; set; }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CircuitPointsRcd> CircuitsPointsRcd { get; set; }
    }
}