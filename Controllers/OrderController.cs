using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedMango_API.data;
using RedMango_API.models;
using System.Net;

namespace RedMango_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected ApiResponse _response;
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _response = new();
            _db = db;
        }

        [HttpGet("{id:int")]
        public async Task<ActionResult<ApiResponse>> GetOrders(int id)
        {
            try
            {
                if(id ==  0) 
                {
                    _response.StatusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var orderHeaders = _db.MangoOrderHeaders.Include(u => u.OrderDetails)
                    .ThenInclude(u => u.MenuItem).Where(u => u.OrderHeaderId == id);

                if(orderHeaders == null )
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                    _response.Result = orderHeaders;
                    _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
                

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetOrders(string? userId)
        {
            try
            {
                var orderHeaders = _db.MangoOrderHeaders.Include(u => u.OrderDetails)
                    .ThenInclude(u => u.MenuItem).OrderByDescending(u => u.OrderHeaderId);
                if (!string.IsNullOrEmpty(userId))
                {
                    _response.Result = orderHeaders.Where(u => u.UserId == userId);
                }
                else
                {
                    _response.Result = orderHeaders;
                }

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }
    }
}
