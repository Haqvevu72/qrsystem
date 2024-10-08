using Microsoft.AspNetCore.Mvc;
using qrsystem.Models.Dtos.Qr;
using qrsystem.Models.Dtos.User;
using qrsystem.Services.QrServices;

namespace qrsystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IQrService service): ControllerBase
{
    private readonly IQrService _qrService = service;

    [HttpPost("addqr")]
    public async Task<IActionResult> AddQr([FromForm] QrAdd qrAdd, [FromForm]IFormFile Img)
    {
        try
        {
            var result = await _qrService.AddQr(qrAdd,Img);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("getallqrs")]
    public async Task<IActionResult> GetAllQrs([FromBody] UserRequest userRequest)
    {
        try
        {
            await Task.Delay(1000);
            var result = await _qrService.GetAllQrs(userRequest);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("removeqr")]
    public async Task<IActionResult> RemoveQr([FromBody] QrDeleteRequest qrDeleteRequest)
    {
        try
        {
            var result = await _qrService.DeleteQr(qrDeleteRequest);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}