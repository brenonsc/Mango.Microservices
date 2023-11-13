using Mango.Web.Util;

namespace Mango.Web.Model;

public class RequestDto
{
    public SD.ApiType ApiType { get; set; } = SD.ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
}