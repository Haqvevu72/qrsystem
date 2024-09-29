using Microsoft.AspNetCore.Mvc;
using qrsystem.Models.Dtos.Qr;
using qrsystem.Services.QrServices;

namespace qrsystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QrController(IQrService qrService): ControllerBase
{
    private readonly IQrService _qrService = qrService;
    
    // Endpoint to redirect and track scans
    [HttpPost("incrementscancount")]
    public async Task<IActionResult> IncrementScanCountAsync(string qrCodeId)
    {
        var qrCode = await _qrService.IncrementScanCountAsync(qrCodeId);
        if (qrCode == null)
            return NotFound("QR Code not found");

        return Ok(qrCode.Value);
    }

    [HttpPost("updateqr")]
    public async Task<IActionResult> UpdateQr(QrUpdateRequest qrUpdateRequest)
    {
        try
        {
            var result = await _qrService.Update(qrUpdateRequest);
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