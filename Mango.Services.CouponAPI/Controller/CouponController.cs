using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Model;
using Mango.Services.CouponAPI.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private ResponseDto _response;
    private IMapper _mapper;
    
    public CouponController(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _response = new ResponseDto();
    }

    [HttpGet]
    public ResponseDto GetAll()
    {
        try
        {
            IEnumerable<Coupon> couponList = _dbContext.Coupons.ToList();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(couponList);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpGet("{id}")]
    public ResponseDto GetById(int id)
    {
        try
        {
            Coupon coupon = _dbContext.Coupons.First(x => x.CouponId == id);
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpGet("GetByCode/{code}")]
    public ResponseDto GetByCode(string code)
    {
        try
        {
            Coupon coupon = _dbContext.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpPost]
    public ResponseDto Register([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _dbContext.Coupons.Add(coupon);
            _dbContext.SaveChanges();
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpPut]
    public ResponseDto Update([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _dbContext.Coupons.Update(coupon);
            _dbContext.SaveChanges();
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpDelete("{id}")]
    public ResponseDto Delete(int id)
    {
        try
        {
            Coupon coupon = _dbContext.Coupons.First(x => x.CouponId == id);
            _dbContext.Coupons.Remove(coupon);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
}