using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testing.Models;

[Table("Role")]
public partial class Role
{
    [Key]
    public int RoleId { get; set; }

    [StringLength(10)]
    public string? Name { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; } = new List<User>();
}
