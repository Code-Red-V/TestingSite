using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testing.Models;

[Table("Test")]
public partial class Test
{
    [Key]
    public int TestId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreationDate { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Tests")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Test")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    [InverseProperty("Test")]
    public virtual ICollection<Result> Results { get; } = new List<Result>();
}
