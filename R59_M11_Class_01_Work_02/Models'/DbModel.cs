using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace R59_M11_Class_01_Work_02.Models_
{
    public enum DeviceType { Mobile = 1, Tab }
    public class Device
    {
        public int DeviceId { get; set; }
        [Required, StringLength(40)]
        public string DeviceName { get; set; } = default!;
        [Required, EnumDataType(typeof(DeviceType))]
        public DeviceType DeviceType { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime? RealeseDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [Required, StringLength(40)]
        public string Picture { get; set; } = default!;
        public bool InStock { get; set; }
        public virtual ICollection<Spec> Specs { get; set; } = new List<Spec>();
    }
    public class Spec
    {
        public int SpecId { get; set; }
        [Required, StringLength(40)]
        public string SpecName { get; set; } = default!;
        [Required, StringLength(40)]
        public string Value { get; set; } = default!;
        [Required, ForeignKey("Device")]
        public int DeviceId { get; set; }
        public virtual Device? Device { get; set; } = default!;
    }
    public class CommonSpec
    {
        public int CommonSpecId { get; set; }
        [Required, StringLength(40)]
        public string SpecName { get; set; } = default!;
    }
    public class DeviceDbContext(DbContextOptions<DeviceDbContext> options) : DbContext(options)
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Spec> Specs { get; set; }
        public DbSet<CommonSpec> CommonSpecs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonSpec>().HasData(
                new CommonSpec { CommonSpecId=1, SpecName="RAM"},
                new CommonSpec { CommonSpecId = 2, SpecName = "Display" },
                new CommonSpec { CommonSpecId = 3, SpecName = "Storage" }
                );
            modelBuilder.Entity<Device>().HasData(
                    new Device { DeviceId=1, DeviceName="Samsung M55", DeviceType= DeviceType.Mobile, RealeseDate=new DateTime(2023, 11, 1), Price=42000.00M, InStock=true, Picture="1.jpg"}
                );
            modelBuilder.Entity<Spec>().HasData(
                   
                new Spec { SpecId=1, DeviceId=1, SpecName="RAM", Value="6GB"},
                new Spec { SpecId = 2, DeviceId = 1, SpecName = "Display", Value = "5.8in" }
                );
        }
    }

}
