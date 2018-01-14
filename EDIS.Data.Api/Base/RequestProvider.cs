using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Core.Exceptions;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Shared.Helpers;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace EDIS.Data.Api.Base
{
    public class RequestProvider : IRequestProvider
    {
        private const string InvalidToken = "INVALID_TOKEN";
        private const string TokenExpired = "TOKEN_EXPIRED";
        private readonly JsonSerializerSettings _serializerSettings;

        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                //NullValueHandling = NullValueHandling.Ignore
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            HttpClient httpClient = CreateHttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response, uri);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public Task<TResult> PostAsync<TResult>(string uri, TResult data)
        {
            return PostAsync<TResult, TResult>(uri, data);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data)
        {
            try
            {
                var httpClient = CreateHttpClient();
                var serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
                HttpResponseMessage response =
                    await httpClient.PostAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

                await HandleResponse(response, uri);

                string responseData = await response.Content.ReadAsStringAsync();
                responseData = responseData.Replace("0000-00-00", "0001-01-01");

                httpClient.Dispose();

                try
                {
                    var converted = JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings);
                }
                catch (Exception e)
                {
                    throw;
                }

                return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));
            }
            catch (ServiceAuthenticationException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<OAuthResponse> TokenPostAsync(string uri, LoginRequest data)
        {
            var httpClient = new HttpClient(new NativeMessageHandler());

            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));

            HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/x-www-form-urlencoded"));

            await HandleResponse(response, uri);

            string responseData = await response.Content.ReadAsStringAsync();

            httpClient.Dispose();

            return await Task.Run(() => JsonConvert.DeserializeObject<OAuthResponse>(responseData, _serializerSettings));
        }

        public Task<TResult> PutAsync<TResult>(string uri, TResult data)
        {
            return PutAsync<TResult, TResult>(uri, data);
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data)
        {
            HttpClient httpClient = CreateHttpClient();
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
            HttpResponseMessage response = await httpClient.PutAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response, uri);

            string responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient(new NativeMessageHandler());

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response, string url)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ErrorResponse error;

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    error = await Task.Run(() => JsonConvert.DeserializeObject<ErrorResponse>(content, _serializerSettings));
                    if(error?.Errors.FirstOrDefault()?.Name == InvalidToken || error?.Errors.FirstOrDefault()?.Name == TokenExpired)
                        throw new ServiceAuthenticationException(error.Errors.FirstOrDefault()?.Message);
                }

                error = await Task.Run(() => JsonConvert.DeserializeObject<ErrorResponse>(content, _serializerSettings));
                if(error == null)
                    throw new HttpRequestException(content);

                throw new Exception("The call to "+ url +" has resulted in a error:\n- message: \""+error.Errors?.FirstOrDefault()?.Message+"\"\n- name: \""+error.Errors?.FirstOrDefault()?.Name + "\"\nEstate: \"" + CommonSettings.EstateName + "\"\nBuilding: \"" + CommonSettings.BuildingName + "\"\nUser privileges: \"" + CommonSettings.BuildingPrivileges + "\"");
            }
        }
    }

    public class ZerosIsoDateTimeConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    {
        /// <summary>
        /// The string representing a datetime value with zeros. E.g. "0000-00-00 00:00:00"
        /// </summary>
        private readonly string _zeroDateString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZerosIsoDateTimeConverter"/> class.
        /// </summary>
        /// <param name="dateTimeFormat">The date time format.</param>
        /// <param name="zeroDateString">The zero date string. 
        /// Please be aware that this string should match the date time format.</param>
        public ZerosIsoDateTimeConverter(string dateTimeFormat, string zeroDateString)
        {
            DateTimeFormat = dateTimeFormat;
            _zeroDateString = zeroDateString;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// If a DateTime value is DateTime.MinValue than the zeroDateString will be set as output value.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime && (DateTime)value == DateTime.MinValue)
            {
                value = _zeroDateString;
                serializer.Serialize(writer, value);
            }
            else
            {
                base.WriteJson(writer, value, serializer);
            }
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// If  an input value is same a zeroDateString than DateTime.MinValue will be set as return value
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return reader.Value.ToString() == _zeroDateString
                ? DateTime.MinValue
                : base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }

    class CustomDateContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);
            bool b = objectType == typeof(DateTime);
            if (b)
            {
                contract.Converter = new ZerosIsoDateTimeConverter("yyyy-MM-dd hh:mm:ss", "0000-00-00");
            }
            return contract;
        }
    }
}