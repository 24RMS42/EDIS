using System.Collections;
using System.Collections.Generic;
using EDIS.Domain.Profile;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class ProfileResponse : BaseResponse
    {
        [JsonProperty("tbl_user")]
        public IEnumerable<User> Users { get; set; }

        [JsonProperty("tbl_instruments")]
        public IEnumerable<Instrument> Instruments { get; set; }
    }
}