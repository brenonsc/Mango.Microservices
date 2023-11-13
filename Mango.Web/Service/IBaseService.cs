using Mango.Web.Model;

namespace Mango.Web.Service;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto);
}