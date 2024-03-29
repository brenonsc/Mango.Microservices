using Mango.Web.Model;
using Mango.Web.Service;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;
    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }
    
    public async Task<IActionResult> CouponIndex()
    {
        List<CouponDto>? list = new();
        ResponseDto response = await _couponService.GetAllCouponsAsync();
        
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
        }
        return View(list);
    }
}