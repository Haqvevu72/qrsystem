using Microsoft.EntityFrameworkCore;
using qrsystem.Models.Entities;

namespace qrsystem.Data;

public class QrSystemDB(DbContextOptions<QrSystemDB> options):DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Qr> Qrs { get; set; }
}