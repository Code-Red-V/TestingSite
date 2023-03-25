using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testing.Models;

[Table("Category")]
public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Test> Tests { get; } = new List<Test>();
}
