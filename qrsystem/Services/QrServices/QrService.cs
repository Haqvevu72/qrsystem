using Microsoft.EntityFrameworkCore;
using qrsystem.Data;
using qrsystem.Models.Dtos.Qr;
using qrsystem.Models.Dtos.User;
using qrsystem.Models.Entities;

namespace qrsystem.Services.QrServices;

public class QrService(QrSystemDB context): IQrService
{
    private readonly QrSystemDB _context = context;


    public async Task<string> AddQr(QrAdd qrAddRequest)
    {
        var isUserExist = await _context.Users.AnyAsync(u => u.Id == qrAddRequest.UserId);
        
        if (!isUserExist) throw new InvalidOperationException("Username is not exist.");

        var qr = new Qr()
        {
            Value = qrAddRequest.Value,
            BgColor = qrAddRequest.BgColor,
            FgColor = qrAddRequest.FgColor,
            ImgUrl = qrAddRequest.ImgUrl,
            Title = qrAddRequest.Title,
            UserId = qrAddRequest.UserId
        };

        await _context.Qrs.AddAsync(qr);
        await _context.SaveChangesAsync();

        return "Qr added successfully";
    }

    public async Task<string> Update(QrUpdateRequest qrUpdateRequest)
    {
        var qr = await _context.Qrs.FindAsync(qrUpdateRequest.Id);

        if (qr is null)
            throw new InvalidOperationException("Qr is not exist");

        qr.Value = qrUpdateRequest.Value;
        qr.Title = qrUpdateRequest.Title;
        qr.UpdatedTime = DateTime.Now;

        _context.Qrs.Update(qr);
        await _context.SaveChangesAsync();

        return "Qr is updated";
    }

    public async Task<List<QrGet>> GetAllQrs(UserRequest userRequest)
    {
        var qrs = await _context.Qrs
            .Where(q => q.UserId == userRequest.Id)
            .Select(q => new QrGet
            {
                Id = q.Id,
                Value = q.Value,
                Title = q.Title,
                BgColor = q.BgColor,
                FgColor = q.FgColor,
                ImgUrl = q.ImgUrl,
                Scans = q.Scans,
                UserId = q.UserId
            })
            .ToListAsync();

        return qrs;
    }

    public async Task<string> DeleteQr(QrDeleteRequest qrDeleteRequest)
    {
        var Qr = await _context.Qrs.FirstOrDefaultAsync(q => q.Id == qrDeleteRequest.Id);

        if (Qr is null) throw new InvalidOperationException("Qr is not exist");

        _context.Qrs.Remove(Qr);
        await _context.SaveChangesAsync();

        return ("Qr has deleted !");
    }

    public async Task<Qr> IncrementScanCountAsync(string qrId)
    {
        var qrCode = await _context.Qrs.FindAsync(qrId);

        if (qrCode is null)
            return null;

        qrCode.Scans++;
        _context.Qrs.Update(qrCode);
        await _context.SaveChangesAsync();

        return qrCode;
    }
}