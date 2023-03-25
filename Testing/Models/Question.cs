using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testing.Models;

[Table("Question")]
public partial class Question
{
    [Key]
    public int QuestionId { get; set; }

    [StringLength(200)]
    public string? Text { get; set; }

    public int? TestId { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<Answer> Answers { get; } = new List<Answer>();

    [ForeignKey("TestId")]
    [InverseProperty("Questions")]
    public virtual Test? Test { get; set; }
}
