using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedMango_API.data;
using RedMango_API.models;
using RedMango_API.models.dto;
using RedMango_API.services;
using RedMango_API.utility;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace RedMango_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IBlobService _blob;
        private ApiResponse _response;

        public MenuItemController(ApplicationDbContext db, IBlobService blob)
        {
            _db = db;
            _response = new ApiResponse();
            _blob = blob;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            _response.Result = _db.MenuItems;
            _response.StatudCode= HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name ="GetMenuItem")]
        public async Task<IActionResult> GetMenuItem(int id)
        {
            if(id == 0)
            {
                _response.StatudCode= HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            MenuItem menuItem = await _db.MenuItems.FirstOrDefaultAsync(mi => mi.Id == id);
            if(menuItem == null)
            {
                _response.StatudCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = menuItem;
            _response.StatudCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateMenuItem([FromForm] MenuItemCreateDto menuItem)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(menuItem.File == null || menuItem.File.Length == 0)
                    {
                        _response.IsSuccess = false;
                        return BadRequest();
                    }
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(menuItem.File.FileName)}";
                    MenuItem menuItemToCreate = new()
                    {
                        Name = menuItem.Name,
                        Price = menuItem.Price,
                        Category = menuItem.Category,
                        SpecialTag = menuItem.SpecialTag,
                        Description = menuItem.Description,
                        Image = await _blob.UploadBlob(fileName, SD.SD_Storage_Container, menuItem.File)
                    };
                    _db.MenuItems.Add(menuItemToCreate);
                    _db.SaveChanges();
                    _response.Result = menuItemToCreate;
                    _response.StatudCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new { id = menuItemToCreate.Id }, _response);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
               
            }
            return _response;

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateMenuItem(int id,[FromForm] MenuItemUpdateDto menuItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItem == null || id != menuItem.Id)
                    {
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    MenuItem menuItemDb = await _db.MenuItems.FindAsync(id);
                    if(menuItemDb == null)
                    {
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    menuItemDb.Name = menuItem.Name;
                    menuItemDb.Description = menuItem.Description;
                    menuItemDb.Price = menuItem.Price;
                    menuItemDb.SpecialTag = menuItem.SpecialTag;
                    menuItemDb.Category = menuItem.Category;

                    if(menuItem.File!= null && menuItem.File.Length > 0) 
                    {
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(menuItem.File.FileName)}";
                        await _blob.DeleteBlob(menuItemDb.Image.Split("/").Last(), SD.SD_Storage_Container);
                        menuItemDb.Image = await _blob.UploadBlob(fileName, SD.SD_Storage_Container, menuItem.File);
                    }

                    _db.MenuItems.Update(menuItemDb);
                    _db.SaveChanges();
                    _response.Result = menuItemDb;
                    _response.StatudCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id)
        {
            try
            {

                    if (id == 0)
                    {
                    _response.IsSuccess = false;
                    return BadRequest();
                    }

                    MenuItem menuItemDb = await _db.MenuItems.FindAsync(id);
                    if (menuItemDb == null)
                    {
                        _response.IsSuccess = false;
                         return BadRequest();
                    }

                        await _blob.DeleteBlob(menuItemDb.Image.Split("/").Last(), SD.SD_Storage_Container);
                      

                    _db.MenuItems.Remove(menuItemDb);
                    _db.SaveChanges();
                    _response.StatudCode = HttpStatusCode.NoContent;
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
