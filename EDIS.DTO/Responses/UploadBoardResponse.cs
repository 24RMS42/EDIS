using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class UploadBoardResponse : BaseResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}