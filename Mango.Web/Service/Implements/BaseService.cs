using System.Net;
using System.Text;
using Mango.Web.Model;
using Mango.Web.Util;
using Newtonsoft.Json;

namespace Mango.Web.Service.Implements;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _clientFactory;
    
    public BaseService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    
    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        try
        {
            HttpClient client = _clientFactory.CreateClient("MangoAPI");
            HttpRequestMessage message = new();
        
            message.Headers.Add("Accept", "application/json");
            //token
        
            message.RequestUri = new Uri(requestDto.Url);
        
            if (requestDto.Data is not null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }
        
            HttpResponseMessage? apiResponse = null;

            switch (requestDto.ApiType)
            {
                case SD.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case SD.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case SD.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }
        
            apiResponse = await client.SendAsync(message);

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
        catch (Exception e)
        {
            var dto = new ResponseDto
            {
                Message = e.Message.ToString(),
                IsSuccess = false
            };
            return dto;
        }
    }
}