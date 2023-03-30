using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testing.Models;

[Table("Answer")]
public partial class Answer
{
    [Key]
    public int AnswerId { get; set; }

    [StringLength(100)]
    public string? Text { get; set; }

    public int? QuestionId { get; set; }

    public bool IsTrue { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("Answers")]
    public virtual Question? Question { get; set; }
}
