using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testing.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(70)]
    public string? UserName { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(30)]
    public string? Password { get; set; }

    public int? RoleId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Result>? Results { get; } = new List<Result>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role? Role { get; set; }
}
