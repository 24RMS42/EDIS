using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ErrorResponse
    {
        [JsonProperty("errors")]
        public IEnumerable<Error> Errors { get; set; }
    }
}