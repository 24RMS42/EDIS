using System.Collections.Generic;
using EDIS.Domain.Circuits;

namespace EDIS.Shared.Models
{
    public class BoardCircuits
    {
        public List<CircuitTest> CircuitsTest { get; set; }
        public List<Circuit> Circuits { get; set; }
    }
}