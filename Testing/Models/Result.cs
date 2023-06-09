﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testing.Models;

[Table("Result")]
public partial class Result
{
    [Key]
    public int ResultId { get; set; }

    public int? UserId { get; set; }

    public int? TestId { get; set; }

    public bool IsTestPassed{ get; set; }

    public int? RightAnswersPercent { get; set; }

    public DateTime CreationDate { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("Results")]
    public virtual Test? Test { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Results")]
    public virtual User? User { get; set; }

}
