using qrsystem.Models.Dtos.Qr;
using qrsystem.Models.Dtos.User;
using qrsystem.Models.Entities;

namespace qrsystem.Services.QrServices;

public interface IQrService
{
    Task<string> AddQr(QrAdd qrAddRequest, IFormFile Img);
    Task<string> Update(QrUpdateRequest qrUpdateRequest);
    Task<List<QrGet>> GetAllQrs(UserRequest userRequest);

    Task<string> DeleteQr(QrDeleteRequest qrDeleteRequest);
    
    Task<Qr> IncrementScanCountAsync(string qrId);
}