using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618

namespace ParcelLockTest.Api.Entities;

[Table("ParcelLock")]
public class ParcelLockDto {
    [Key]
    public string Number { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public bool IsActive { get; set; }
}
